using System.Windows;
using System.Windows.Controls;

namespace SpoofMess.ViewElements;

public partial class SideMenu : UserControl
{
    private readonly static DependencyProperty IsOpenProperty =
        DependencyProperty.Register("IsOpen", typeof(Visibility), typeof(SideMenu));

    public Visibility IsOpen
    {
        get => (Visibility)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public SideMenu()
    {
        InitializeComponent();
    }

    public void ChangeMenuVisibility()
    {
        SideMenuView.Visibility = SideMenuView.Visibility is Visibility.Visible
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    private void Rectangle_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SideMenuView.Visibility = Visibility.Collapsed;
    }
}
