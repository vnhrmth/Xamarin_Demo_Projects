using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DemoApp.Models;
using DemoApp.Repositories;
using DemoApp.Views;
using Xamarin.Forms;

namespace DemoApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TodoItemRepository repository;

        // we are creating a itemviewmodel instead of directly using a model object.
        /*
         * It is good practice to wrap plain objects in ViewModel; 
         * this makes it simpler to add actions or extra properties to it. 
         * ItemStatusChanged is a stub that is called when we change the status of the to-do list 
         * item from active to completed and vice versa.
         */
        public ObservableCollection<TodoItemViewModel> Items { get; set; }

        public TodoItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await NavigateToItem(value);
                    RaisePropertyChanged(nameof(SelectedItem));
                });
            }
        }

        private async Task NavigateToItem(TodoItemViewModel item)
        {
            if(item == null)
            {
                return;
            }

            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.TodoItem = item.Item;

            await Navigation.PushAsync(itemView);
        }
        public string FilterText => ShowAll ? "All" : "Active";

        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });

        public MainViewModel(TodoItemRepository repository)
        {
            // On item added and updated we need event for these.
            repository.OnItemAdded += (sender, item) => Items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this.repository = repository;
            Task.Run(async() =>
            {
                await LoadData();
            });
        }

        private async Task LoadData()
        {
            var items = await repository.GetItems();
            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }

            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));
            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem item) 
        {
            var todoItemViewModel = new TodoItemViewModel(item);
            todoItemViewModel.ItemStatusChanged += ItemStatusChanged;
            return todoItemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is TodoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }

                Task.Run(async () => await
                repository.UpdateItem(item.Item));
            }
        }

        public bool ShowAll { get; set; }

        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<DemoApp.Views.ItemView>();
            await Navigation.PushAsync(itemView);
        }); 


    }
}
