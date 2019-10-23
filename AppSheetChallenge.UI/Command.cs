using System;
using System.Windows.Input;

namespace AppSheetChallenge.UI
{
	/// <summary>
	/// A generic implementation of <see cref="ICommand"/> that can be used to execute commands in
	/// a view model.
	/// </summary>
	public class Command : ICommand
	{
		#region Events
		/// <summary>
		/// Triggered when the result of <see cref="CanExecute(object)"/> will change.
		/// </summary>
		public event EventHandler CanExecuteChanged;
		#endregion

		#region Properties
		private Action<object> ExecuteAction { get; }

		private Func<object, bool> CanExecuteFunction { get; }
		#endregion

		#region Constructor
		/// <summary>
		/// Instantiates a new <see cref="Command"/> object.
		/// </summary>
		/// <param name="execute">The action to execute when the command is triggered.</param>
		/// <param name="canExecute">A function that returns true when the command can be executed
		/// and false otherwise.</param>
		public Command(Action<object> execute, Func<object, bool> canExecute = null)
		{
			this.ExecuteAction = execute;
			this.CanExecuteFunction = canExecute ?? new Func<object, bool>(p => true);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Returns true when <see cref="Execute(object)"/> can be called and false otherwise.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public bool CanExecute(object parameter)
		{
			return this.CanExecuteFunction(parameter);
		}

		/// <summary>
		/// Executed when the command is triggered.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void Execute(object parameter)
		{
			this.ExecuteAction(parameter);
		}

		/// <summary>
		/// Triggers the <see cref="CanExecuteChanged"/> event.
		/// </summary>
		public void OnCanExecuteChanged()
		{
			this.CanExecuteChanged?.Invoke(this, new EventArgs());
		}
		#endregion
	}
}
