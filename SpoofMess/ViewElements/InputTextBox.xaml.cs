using System.Windows;
using System.Windows.Controls;

namespace SpoofMess.ViewElements;

public partial class InputTextBox : UserControl
{
    private readonly static DependencyProperty HolderProperty =
        DependencyProperty.Register("Holder", typeof(string), typeof(InputTextBox), new FrameworkPropertyMetadata(
            default(string),
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    private readonly static DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(InputTextBox), new FrameworkPropertyMetadata(
            default(string),
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    public string Holder
    {
        get => 
            (string)GetValue(HolderProperty);
        set =>
            SetValue(HolderProperty, value);
    }

    public string Text
    {
        get =>
            (string)GetValue(TextProperty);
        set =>
            SetValue(TextProperty, value);
    }

    public InputTextBox()
    {
        InitializeComponent();
    }

    private void InputTextChanged(object sender, TextChangedEventArgs e)
    {
        HolderText.Visibility = string.IsNullOrEmpty(Input.Text) 
            ? Visibility.Visible 
            : Visibility.Collapsed;
    }
}
