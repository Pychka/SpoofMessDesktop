using System.Windows;
using System.Windows.Controls;

namespace SpoofMess.ViewElements;

public partial class InputPasswordBox : UserControl
{
    private readonly static DependencyProperty HolderProperty =
        DependencyProperty.Register("Holder", typeof(string), typeof(InputTextBox));

    private readonly static DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(InputTextBox));


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
    public InputPasswordBox()
    {
        InitializeComponent();
    }
    private string _password;

    private void InputTextChanged(object sender, TextChangedEventArgs e)
    {
        HolderText.Visibility = string.IsNullOrEmpty(Input.Text)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }
}
