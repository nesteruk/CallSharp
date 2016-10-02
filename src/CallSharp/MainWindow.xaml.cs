using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace CallSharp
{
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



    public bool? ScaleWindow
    {
      get { return (bool?)GetValue(ScaleWindowProperty); }
      set { SetValue(ScaleWindowProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ScaleWindow.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ScaleWindowProperty =
        DependencyProperty.Register("ScaleWindow", typeof(bool?), typeof(MainWindow), new PropertyMetadata(false));



    private object parsedInputValue = string.Empty, parsedOutputValue = string.Empty;

    private static void InputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var self = (MainWindow) d;

      var parsedValues = ((string) e.NewValue).InferTypes();
      parsedValues.Add(e.NewValue);
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
      parsedValues.Add(e.NewValue);
      if (parsedValues.Any())
      {
        self.parsedOutputValue = parsedValues[0];
        self.OutputType = self.parsedOutputValue.GetType();

        self.AlternateOutputValues.Inlines.Clear();
        foreach (var i in parsedValues)
        {
          if (self.OutputType != i.GetType())
          {
            Hyperlink h = new Hyperlink();
            h.Inlines.Add(i.GetType().GetFriendlyName());
            h.Tag = i;
            h.Click += (sender, args) =>
            {
              var me = (Hyperlink)sender;
              // cache the current type
              Type currentType = self.InputType;
              // set the new type
              self.OutputType = me.Tag.GetType();
              self.parsedOutputValue = me.Tag;
              // restore my type and name
              me.Inlines.Clear();
              me.Inlines.Add(currentType.GetFriendlyName());
              me.Tag = currentType;
            };
            Span s = new Span(h);
            s.Inlines.Add(" ");
            self.AlternateOutputValues.Inlines.Add(s);
          }
        }
      }
    }

    public MainWindow()
    {
      InitializeComponent();
    }

    private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
    {
      Candidates.Clear();
      FindCandidates(parsedInputValue, parsedOutputValue, 0);
    }

    private void FindCandidates(object input, object output, int depth, string callChain = "")
    {
      // if input and output are identical, be sure to add it
      if (InputText == OutputText)
      {
        Candidates.Add("input");
        return; // let's not chain-call this (unless you want to obfuscate)
      }

      // contains all calls that didn't yield the right result
      var failCookies = new List<MethodCallCookie>();

      SetProgress("Looking for 1-to-1 member calls.");

      foreach (var m in memberDatabase.FindOneToOneNonStatic(
          input.GetType(), output.GetType()))
      {
        var cookie = m.InvokeWithNoArgument(input);
        if (output.Equals(cookie?.ReturnValue))
          Candidates.Add("input" + callChain + cookie);
        else
        {
          failCookies.Add(cookie);
        }
      }

      SetProgress("Looking for 1-to-1 static calls.");

      foreach (var m in memberDatabase.FindOneToOneStatic(
          input.GetType(), output.GetType()))
      {
        var cookie = m.InvokeStaticWithSingleArgument(input);
        if (output.Equals(cookie?.ReturnValue))
          Candidates.Add($"{m.DeclaringType?.Name}{callChain}{cookie}");
        else
        {
          failCookies.Add(cookie);
        }
      }

      if (!Candidates.Any() && depth < 2)
      {
        // if we found nothing of worth, try a chain
        foreach (
          var m in memberDatabase.FindAnyToOneNonStatic(input.GetType(), output.GetType())
        )
        {
          // get the cookie for this invocation
          var cookie = m.InvokeWithNoArgument(input);
          
          // pass it on
          FindCandidates(cookie.ReturnValue, output, depth+1, cookie.ToString());
        }
      }

      SetProgress(Candidates.Any()
        ? $"Found {Candidates.Count} call chain{(Candidates.Count % 10 == 1 ? string.Empty : "s")}"
        : "Could not find any call chains for given input/output");
    }

    private void BtnCopy_OnClick(object sender, RoutedEventArgs e)
    {
      if (LbCandidates.SelectedIndex >= 0)
      {
        Clipboard.SetText(LbCandidates.SelectedItem.ToString());
        TbInfo.Text = "Text copied to clipboard.";
      }
    }

    private void SetProgress(string s)
    {
      TbInfo.Text = s;
    }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
      ScaleWindow = CbScaleWindow.IsChecked;
    }
  }
}