using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpoofMess.ViewElements;

public partial class FunctionButton : UserControl
{
    private static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(FunctionButton));
    private static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(string), typeof(FunctionButton));
    private static readonly DependencyProperty AlignmentProperty =
        DependencyProperty.Register("Alignment", typeof(HorizontalAlignment), typeof(FunctionButton));
    private static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register("Command", typeof(ICommand), typeof(FunctionButton));

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
    public HorizontalAlignment Alignment
    {
        get => (HorizontalAlignment)GetValue(AlignmentProperty);
        set => SetValue(AlignmentProperty, value);
    }
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    public FunctionButton()
    {
        InitializeComponent();
    }
}
