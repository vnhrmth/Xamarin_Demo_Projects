using DemoApp.ViewModels;
using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class ItemView : ContentPage
    {
        public ItemView(ItemViewModel itemViewModel)
        {
            InitializeComponent();
            itemViewModel.Navigation = Navigation;
            BindingContext = itemViewModel;
        }
    }
}
