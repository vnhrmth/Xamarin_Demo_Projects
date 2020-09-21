using System;
using System.Windows.Input;
using DemoApp.Models;
using DemoApp.Repositories;
using Xamarin.Forms;

namespace DemoApp.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        public TodoItem TodoItem { get; set; }
        private readonly TodoItemRepository repository;

        public ItemViewModel(TodoItemRepository repository)
        {
            this.repository = repository;
            TodoItem = new TodoItem { Due = DateTime.Now.AddDays(1) };
        }

        public ICommand SaveItem => new Command(async () =>
        {
             await this.repository.AddOrUpdateItem(TodoItem);
             await Navigation.PopAsync();
        });
    }
}
