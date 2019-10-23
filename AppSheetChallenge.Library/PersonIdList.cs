namespace AppSheetChallenge.Library
{
	/// <summary>
	/// Represents a list of person ID's returned by a web service.
	/// </summary>
	internal class PersonIdList
	{
		#region Properties
		/// <summary>
		/// Gets or sets the list of person ID's.
		/// </summary>
		public int[] Result { get; set; }

		/// <summary>
		/// Gets or sets the token to get the next ID's.
		/// </summary>
		public string Token { get; set; }
		#endregion
	}
}
