using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace CallSharp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MemberDatabase memberDatabase = new MemberDatabase();
    private static object[] noArgs = { };

    public ObservableHashSet<string> Candidates
    {
      get { return (ObservableHashSet<string>)GetValue(CandidatesProperty); }
      set { SetValue(CandidatesProperty, value); }
    }

    public static readonly DependencyProperty CandidatesProperty =
        DependencyProperty.Register("Candidates", typeof(ObservableHashSet<string>), typeof(MainWindow),
          new PropertyMetadata(new ObservableHashSet<string>()));

    public string InputText
    {
      get { return (string) GetValue(InputTextProperty); }
      set { SetValue(InputTextProperty, value); }
    }

    public static readonly DependencyProperty InputTextProperty =
        DependencyProperty.Register("InputText", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty, InputChanged));

    public Type InputType
    {
      get { return (Type)GetValue(InputTypeProperty); }
      set { SetValue(InputTypeProperty, value); }
    }

    public static readonly DependencyProperty InputTypeProperty =
        DependencyProperty.Register("InputType", typeof(Type), typeof(MainWindow), new PropertyMetadata(typeof(string)));

    public Type OutputType
    {
      get { return (Type)GetValue(OutputTypeProperty); }
      set { SetValue(OutputTypeProperty, value); }
    }

    public static readonly DependencyProperty OutputTypeProperty =
        DependencyProperty.Register("OutputType", typeof(Type), typeof(MainWindow), new PropertyMetadata(typeof(string)));

    private object parsedInputValue, parsedOutputValue;

    private static void InputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var self = (MainWindow) d;

      var parsedValues = ((string) e.NewValue).InferTypes();
      if (parsedValues.Any())
      {
        self.parsedInputValue = parsedValues[0];
        self.InputType = self.parsedInputValue.GetType();

        self.AlternateInputValues.Inlines.Clear();
        foreach (var i in parsedValues)
        {
          if (!Equals(self.InputType, i.GetType()))
          {
            Hyperlink h = new Hyperlink();
            h.Inlines.Add(i.GetType().GetFriendlyName());
            h.Tag = i;
            h.Click += (sender, args) =>
            {
              var me = (Hyperlink) sender;
              // cache the current type
              Type currentType = self.InputType;
              // set the new type
              self.InputType = me.Tag.GetType();
              self.parsedInputValue = me.Tag;
              // restore my type and name
              me.Inlines.Clear();
              me.Inlines.Add(currentType.GetFriendlyName());
              me.Tag = currentType;
            };
            Span s = new Span(h);
            s.Inlines.Add(" ");
            self.AlternateInputValues.Inlines.Add(s);
          }
        }
      }
    }

    public string OutputText
    {
      get { return (string)GetValue(OutputTextProperty); }
      set { SetValue(OutputTextProperty, value); }
    }

    public static readonly DependencyProperty OutputTextProperty =
        DependencyProperty.Register("OutputText", 
          typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty, OutputChanged));

    private static void OutputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var self = (MainWindow) d;
      var parsedValues = ((string)e.NewValue).InferTypes();
      if (parsedValues.Any())
      {
        self.parsedOutputValue = parsedValues[0];
        self.OutputType = self.parsedOutputValue.GetType();
      }
    }

    public MainWindow()
    {
      InitializeComponent();
    }

    private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
    {
      Candidates.Clear();

      // if input and output are identical, be sure to add it
      if (InputText == OutputText)
        Candidates.Add("input");

      var input = InputText.InferTypes().FirstOrDefault() ?? InputText;
      var output = OutputText.InferTypes().FirstOrDefault() ?? OutputText;

      foreach (
        var m in memberDatabase.FindOneToOneNonStatic(
          input.GetType(), output.GetType()))
      {
        object actualOutput = m.InvokeWithNoArgument(input);
        if (output.Equals(actualOutput))
          Candidates.Add("input." + m.Name + "()");
      }

      foreach (
        var m in memberDatabase.FindOneToOneStatic(
          input.GetType(), output.GetType()))
      {
        var actualOutput = m.InvokeStaticWithSingleArgument(input);
        if (output.Equals(actualOutput))
          Candidates.Add($"{m.DeclaringType?.Name}.{m.Name}(input)");
      }

      foreach (
        var p in
        memberDatabase.FindOneToOnePropertyGet(input.GetType(),
          output.GetType()))
      {
        var actualOutput = p.GetMethod.Invoke(input, noArgs);
        if (output.Equals(actualOutput))
          Candidates.Add("input." + p.Name);
      }
    }

    private void BtnCopy_OnClick(object sender, RoutedEventArgs e)
    {
      if (LbCandidates.SelectedIndex >= 0)
      {
        Clipboard.SetText(LbCandidates.SelectedItem.ToString());
        TbInfo.Text = "Text copied to clipboard.";
      }
    }
  }
}