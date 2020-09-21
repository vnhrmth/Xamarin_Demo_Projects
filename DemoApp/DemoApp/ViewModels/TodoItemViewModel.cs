using System;
using System.Windows.Input;
using DemoApp.Models;
using Xamarin.Forms;

namespace DemoApp.ViewModels
{
    public class TodoItemViewModel : BaseViewModel
    {
        public event EventHandler ItemStatusChanged;

        public TodoItem Item { get; private set; }
        public TodoItemViewModel(TodoItem item) => Item = item;

        public string StatusText => Item.Completed ? "Reactivate" : "Completed";

        public ICommand ToggleCompleted => new Command((arg) =>
         {
             Item.Completed = !Item.Completed;
             ItemStatusChanged?.Invoke(this, new EventArgs());
         });
    }
}