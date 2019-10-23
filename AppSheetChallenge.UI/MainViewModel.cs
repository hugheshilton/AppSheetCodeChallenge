using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AppSheetChallenge.Library;

namespace AppSheetChallenge.UI
{
	/// <summary>
	/// The main window view model.
	/// </summary>
	public class MainViewModel : INotifyPropertyChanged
	{
		#region Class Variables
		private ObservableCollection<Person> myPeople;
		private Command myRetrieveCommand;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the collection of people being displayed.
		/// </summary>
		public ObservableCollection<Person> People =>
			myPeople ?? (myPeople = new ObservableCollection<Person>());

		/// <summary>
		/// Gets a command to retrieve all people.
		/// </summary>
		public Command RetrieveCommand =>
			myRetrieveCommand ?? (myRetrieveCommand = new Command(this.Retrieve, p => this.ButtonsEnabled));

		private bool ButtonsEnabled { get; set; } = true;
		#endregion

		#region Private Methods
		private async void Retrieve(object parameter)
		{
			this.ButtonsEnabled = false;
			this.RetrieveCommand.OnCanExecuteChanged();
			this.People.Clear();
			var finder = new PeopleFinder("https://appsheettest1.azurewebsites.net/sample");
			var type = (RetrievalType)parameter;
			var usOnly = type == RetrievalType.USOnly || type == RetrievalType.Top5USOnly;
			var top = type == RetrievalType.Top5USOnly ? 5 : 0;
			var people = await finder.FindPeopleSortedByAge(usOnly, top);
			if (type == RetrievalType.Top5USOnly)
				people = people.OrderBy(p => p.name).ToArray();
			foreach (var person in people)
			{
				this.People.Add(person);
			}
			this.ButtonsEnabled = true;
			this.RetrieveCommand.OnCanExecuteChanged();
		}
		#endregion

		#region INotifyPropertyChanged Members
		/// <summary>
		/// Event that is triggered when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Triggers the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">The property name or the calling member name when unspecified.</param>
		protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}

	/// <summary>
	/// The type of people retrieval operation to perform.
	/// </summary>
	public enum RetrievalType
	{
		/// <summary>
		/// Retrieve all people (sorted by age).
		/// </summary>
		All,
		/// <summary>
		/// Retrieve all people with United States phone numbers (sorted by age).
		/// </summary>
		USOnly,
		/// <summary>
		/// Retrieve the top 5 US people sorted by name (sorted by name).
		/// </summary>
		Top5USOnly
	}
}
