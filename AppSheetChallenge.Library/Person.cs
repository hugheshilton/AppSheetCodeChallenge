using System;
using System.Text.RegularExpressions;

namespace AppSheetChallenge.Library
{
	/// <summary>
	/// Represents a person.
	/// </summary>
	public class Person : IComparable<Person>, IHasException
	{
		#region Properties
		/// <summary>
		/// Gets or sets the person's ID.
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Gets or sets the person's name.
		/// </summary>
		public string name { get; set; }

		/// <summary>
		/// Gets or sets the person's age.
		/// </summary>
		public int age { get; set; }

		/// <summary>
		/// Gets or sets the person's phone number.
		/// </summary>
		public string number { get; set; }

		/// <summary>
		/// Gets or sets a url to an image of the person.
		/// </summary>
		public string photo { get; set; }

		/// <summary>
		/// Gets or sets the person's biographical description.
		/// </summary>
		public string bio { get; set; }

		/// <summary>
		/// Gets or sets any exception that occurs retrieving the object from the web service.
		/// </summary>
		public Exception exception { get; set; }
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets a value indicating whether this person has a phone number in the united states. Area code is required.
		/// Examples of allowed formats:
		/// 123.456.7890
		/// 123-456-7890
		/// (123) 456-7890
		/// </summary>
		/// <returns>True if the phone number is a valid United States number; otherwise, false.</returns>
		public bool HasUnitedStatesNumber()
		{
			const string regexPattern = @"^\s*\(?\s*([0-9]{3})\s*\)?\s*[-.]?\s*([0-9]{3})\s*[-.]?\s*([0-9]{4})\s*$";
			return string.IsNullOrEmpty(this.number) ? false : Regex.IsMatch(this.number, regexPattern);
		}
		#endregion

		#region IComparable Members
		/// <summary>
		/// Compares this person to the specified <paramref name="other"/> based on age.
		/// </summary>
		/// <param name="other">The other person to which to compare.</param>
		/// <returns>Less than zero if <paramref name="other"/> is older than this person, 0 if the ages are equal, and greater than zero
		/// if <paramref name="other"/> is younger than this person.</returns>
		public int CompareTo(Person other)
		{
			return this.age.CompareTo(other.age);
		}
		#endregion
	}
}
