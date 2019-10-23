using System.Collections.ObjectModel;
using System.ComponentModel;
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
		private Command myRetrieveAllCommand;
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
		public Command RetrieveAllCommand =>
			myRetrieveAllCommand ?? (myRetrieveAllCommand = new Command(this.RetrieveAll, p => this.ButtonsEnabled));

		private bool ButtonsEnabled { get; set; } = true;
		#endregion

		#region Private Methods
		private async void RetrieveAll(object parameter)
		{
			this.ButtonsEnabled = false;
			this.RetrieveAllCommand.OnCanExecuteChanged();
			this.People.Clear();
			var finder = new PeopleFinder("https://appsheettest1.azurewebsites.net/sample");
			foreach (var person in await finder.FindAllPeopleSortedByAge())
			{
				this.People.Add(person);
			}
			this.ButtonsEnabled = true;
			this.RetrieveAllCommand.OnCanExecuteChanged();
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
}
