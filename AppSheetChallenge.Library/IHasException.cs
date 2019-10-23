using System;

namespace AppSheetChallenge.Library
{
	/// <summary>
	/// Represents an object that has an Exception property.
	/// </summary>
	interface IHasException
	{
		#region Properties
		/// <summary>
		/// Gets or sets an exception that occured.
		/// </summary>
		Exception Exception { get; set; }
		#endregion
	}
}
