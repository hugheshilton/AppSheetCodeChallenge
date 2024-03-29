﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppSheetChallenge.Library;

namespace AppSheetChallenge.Test
{
	[TestClass]
	public class PersonTest
	{
		#region Tests
		[TestMethod]
		public void HasUnitedStatesNumber()
		{
			var person = new Person();
			// Some example of false cases.
			Assert.IsFalse(person.HasUnitedStatesNumber());
			// Area code is required.
			person.Number = "123-1234";
			Assert.IsFalse(person.HasUnitedStatesNumber());
			// Letters are not allowed.
			person.Number = "ABC 123 1234";
			Assert.IsFalse(person.HasUnitedStatesNumber());
			// Too few digits.
			person.Number = "123 123 123";
			Assert.IsFalse(person.HasUnitedStatesNumber());
			// Too many digits.
			person.Number = "123 123 12345";
			Assert.IsFalse(person.HasUnitedStatesNumber());
			// The only allowed separators are ( ) / and .
			person.Number = "123&123&1234";
			Assert.IsFalse(person.HasUnitedStatesNumber());
			// True cases.
			person.Number = "1231231234";
			Assert.IsTrue(person.HasUnitedStatesNumber());
			person.Number = "123-123-1234";
			Assert.IsTrue(person.HasUnitedStatesNumber());
			person.Number = "123.123.1234";
			Assert.IsTrue(person.HasUnitedStatesNumber());
			person.Number = "(123) 123-1234";
			Assert.IsTrue(person.HasUnitedStatesNumber());
			person.Number = "  123    456    7890    ";
			Assert.IsTrue(person.HasUnitedStatesNumber());
			person.Number = "  (  123  )  123   -   1234  ";
			Assert.IsTrue(person.HasUnitedStatesNumber());
		}
		#endregion
	}
}
