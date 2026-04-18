using Laboratory_ProductManager.AppUI.ViewModels;

namespace Laboratory_ProductManager.AppUI
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
