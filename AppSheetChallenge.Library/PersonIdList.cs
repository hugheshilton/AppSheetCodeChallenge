using System;
using System.Runtime.Serialization;

namespace AppSheetChallenge.Library
{
	/// <summary>
	/// Represents a list of person ID's returned by a web service.
	/// </summary>
	public class PersonIdList : IHasException
	{
		#region Properties
		/// <summary>
		/// Gets or sets the list of person ID's.
		/// </summary>
		[DataMember(Name = "result")]
		public int[] Result { get; set; }

		/// <summary>
		/// Gets or sets the token to get the next ID's.
		/// </summary>
		[DataMember(Name = "token")]
		public string Token { get; set; }

		/// <summary>
		/// Gets or sets any exception that occurs retrieving the object from the web service.
		/// </summary>
		public Exception Exception { get; set; }
		#endregion
	}
}
