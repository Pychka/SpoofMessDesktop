using System.Windows;

namespace SpoofMess.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
    }

    private void Menu_Click(object sender, RoutedEventArgs e)
    {
        SideMenu.ChangeMenuVisibility();
    }
}
