using Microsoft.UI.Xaml;
using WindowsPrivacyAutomator.ViewModels;


namespace WindowsPrivacyAutomator
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; } = new();

        public MainWindow()
        {
            InitializeComponent();

            this.AppWindow.Resize(new Windows.Graphics.SizeInt32(500, 700));
        }
    }
}
