using DemoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            mainViewModel.Navigation = Navigation;
            BindingContext = mainViewModel;

            //resetting the selected to null so that it doesn't remain in that state.
            ItemsListView.ItemSelected += (s, e) =>ItemsListView.SelectedItem = null;
        }
    }
}
