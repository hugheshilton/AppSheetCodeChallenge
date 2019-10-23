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
		/// Returns all <see cref="Person"/> objects from the web service sorted youngest to oldest.
		/// If an exception occurs while retrieving a person, that person is excluded from the results.
		/// </summary>
		/// <param name="unitedStatesNumbersOnly">True to return only people with phone numbers in the United States.</param>
		/// <param name="top">If set to a positive integer, only that many people (or less) are returned.</param>
		public async Task<Person[]> FindPeopleSortedByAge(bool unitedStatesNumbersOnly = false, int top = 0)
		{
			const string listPath = "list";
			const string detailPath = "detail";
			var people = new SortedSet<Person>();
			PersonIdList result = null;
			do
			{
				var query = result != null ? $"token={result.token}" : string.Empty;
				result = await this.QueryService<PersonIdList>(listPath, query);
				foreach (var person in await Task.WhenAll(result.result.Select(i => this.QueryService<Person>($"{detailPath}/{i}"))))
				{
					if (person.exception == null && (!unitedStatesNumbersOnly || person.HasUnitedStatesNumber()))
						people.Add(person);
				}
			} while (!string.IsNullOrEmpty(result.token));
			if (top > 0)
				return people.Take(top).ToArray();
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
