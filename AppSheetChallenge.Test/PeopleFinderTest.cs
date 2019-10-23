using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppSheetChallenge.Library;

namespace AppSheetChallenge.Test
{
	[TestClass]
	public class PeopleFinderTest
	{
		#region Tests
		[TestMethod]
		public void FindAllPeopleSortedByAge()
		{
			var finder = new PeopleFinder("https://appsheettest1.azurewebsites.net/sample");
			var task = finder.FindPeopleSortedByAge();
			task.Wait();
			var actual = task.Result;
			Assert.AreEqual(26, actual.Length);
			var previousAge = -1;
			foreach (var person in actual)
			{
				Assert.IsTrue(person.age > previousAge);
				previousAge = person.age;
			}
		}

		[TestMethod]
		public void FindUnitedStatesPeopleSortedByAge()
		{
			var finder = new PeopleFinder("https://appsheettest1.azurewebsites.net/sample");
			var task = finder.FindPeopleSortedByAge(true);
			task.Wait();
			var actual = task.Result;
			Assert.AreEqual(20, actual.Length);
			var previousAge = -1;
			foreach (var person in actual)
			{
				Assert.IsTrue(person.HasUnitedStatesNumber());
				Assert.IsTrue(person.age > previousAge);
				previousAge = person.age;
			}
		}
		#endregion
	}
}
