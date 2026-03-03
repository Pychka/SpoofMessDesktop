using System.Windows;
using System.Windows.Controls;

namespace SpoofMess.ViewElements;

public partial class FunctionButton : UserControl
{
    private static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(FunctionButton));
    private static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(string), typeof(FunctionButton));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    public FunctionButton()
    {
        InitializeComponent();
    }
}
