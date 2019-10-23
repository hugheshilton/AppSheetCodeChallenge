using System;

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
		public int[] result { get; set; }

		/// <summary>
		/// Gets or sets the token to get the next ID's.
		/// </summary>
		public string token { get; set; }

		/// <summary>
		/// Gets or sets any exception that occurs retrieving the object from the web service.
		/// </summary>
		public Exception exception { get; set; }
		#endregion
	}
}
