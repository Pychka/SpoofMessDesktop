using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpoofMess.ViewElements;
public partial class QuestionTextButton : UserControl
{
    private static readonly DependencyProperty PrefixProperty =
        DependencyProperty.Register(nameof(Prefix), typeof(string), typeof(QuestionTextButton), null);

    private static readonly DependencyProperty CommandTextProperty =
        DependencyProperty.Register(nameof(CommandText), typeof(string), typeof(QuestionTextButton), null);

    private static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(QuestionTextButton), null);

    public string Prefix
    {
        get => (string)GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }

    public string CommandText
    {
        get => (string)GetValue(CommandTextProperty);
        set => SetValue(CommandTextProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    public QuestionTextButton()
    {
        InitializeComponent();
    }
}
