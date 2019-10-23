using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utf8Json;

namespace AppSheetChallenge.Library
{
	/// <summary>
	/// Retrieves <see cref="Person"/> objects from a web service.
	/// </summary>
	public class PeopleFinder
	{
		#region Properties
		private string ServiceUrl { get; }
		#endregion

		#region Constructors
		/// <summary>
		/// Instantiates a new <see cref="PeopleFinder"/> object with the specified base <paramref name="serviceUrl"/>.
		/// </summary>
		/// <param name="serviceUrl">The base url to the web service from which to retrieve people.</param>
		public PeopleFinder(string serviceUrl)
		{
			this.ServiceUrl = serviceUrl;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Returns all <see cref="Person"/> objects from the web service sorted youngest to oldest by age.
		/// If an exception occurs while retrieving a person, that person is excluded from the results.
		/// </summary>
		public async Task<Person[]> FindAllPeopleSortedByAge()
		{
			const string listPath = "list";
			const string detailPath = "detail";
			var people = new SortedSet<Person>();
			var result = await this.QueryService<PersonIdList>(listPath);
			foreach (var person in await Task.WhenAll(result.result.Select(i => this.QueryService<Person>($"{detailPath}/{i}"))))
			{
				if (person.exception == null)
					people.Add(person);
			}
			while (!string.IsNullOrEmpty(result.token))
			{
				result = await this.QueryService<PersonIdList>(listPath, $"token={result.token}");
				foreach (var person in await Task.WhenAll(result.result.Select(i => this.QueryService<Person>($"{detailPath}/{i}"))))
				{
					if (person.exception == null)
						people.Add(person);
				}
			}
			return people.ToArray();
		}
		#endregion

		#region Private Methods
		private async Task<T> QueryService<T>(string path, string query = "") where T : IHasException, new()
		{
			try
			{
				var builder = new UriBuilder(this.ServiceUrl);
				if (!string.IsNullOrEmpty(builder.Path))
					builder.Path = $"{builder.Path}/{path}";
				else
					builder.Path = path;
				if (!string.IsNullOrEmpty(query))
					builder.Query = query;
				var request = WebRequest.Create(builder.ToString());
				using (var response = await request.GetResponseAsync())
				{
					using (var stream = response.GetResponseStream())
					{
						return await JsonSerializer.DeserializeAsync<T>(stream);
					}
				}
			}
			catch (Exception ex)
			{
				var result = new T();
				((IHasException)result).exception = ex;
				return result;
			}
		}
		#endregion
	}
}
