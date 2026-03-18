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

    private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        AdditionalContainer.Visibility = Visibility.Collapsed;
    }
    private void Menu_Item_Click(object sender, RoutedEventArgs e)
    {
        AdditionalContainer.Visibility = Visibility.Visible;
        SideMenu.ChangeMenuVisibility();
        e.Handled = true;
    }

    private void ContentPresenter_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        e.Handled = true;
    }
}
