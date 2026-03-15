using System.Windows;
using System.Windows.Controls;

namespace SpoofMess.ViewElements;

public partial class ShowView : UserControl
{
    private static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(string), typeof(ShowView), null);
    private static readonly DependencyProperty HeaderVisibilityProperty =
        DependencyProperty.Register(nameof(HeaderVisibility), typeof(Visibility), typeof(ShowView), null);
    private static readonly DependencyProperty ViewProperty =
        DependencyProperty.Register(nameof(View), typeof(ContentControl), typeof(ShowView), null);


    public string Header
    {
        get => (string)GetValue(HeaderProperty); 
        set => SetValue(HeaderProperty, value);
    }

    public Visibility HeaderVisibility
    {
        get => (Visibility)GetValue(HeaderVisibilityProperty);
        set => SetValue(HeaderVisibilityProperty, value);
    }

    public ContentControl View
    {
        get => (ContentControl)GetValue(ViewProperty);
        set => SetValue(ViewProperty, value);
    }

    public ShowView()
    {
        InitializeComponent();
    }
}
