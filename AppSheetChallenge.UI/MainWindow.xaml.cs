using System.Windows;

namespace AppSheetChallenge.UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Constructors
		/// <summary>
		/// Instantiates a new <see cref="MainWindow"/> object.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new MainViewModel();
		}
		#endregion
	}
}
