using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace CallSharp
{
  using System;
  using System.Linq;
  using System.Reflection;
  using System.Threading.Tasks;
  using System.Windows;
  using System.Windows.Documents;

  public partial class MainWindow : Window
  {
    private readonly IMemberDatabase dynamicMemberDatabase = new StaticMemberDatabase();
    private static object[] noArgs = { };

    private const string defaultPrimaryRunText = "Search!";
    private const string defaultSecondaryRunText = "may take a long time";

    public string PrimaryRunLabel
    {
      get { return (string)GetValue(PrimaryRunLabelProperty); }
      set { SetValue(PrimaryRunLabelProperty, value); }
    }
    public static readonly DependencyProperty PrimaryRunLabelProperty =
        DependencyProperty.Register("PrimaryRunLabel", typeof(string), typeof(MainWindow), new PropertyMetadata(defaultPrimaryRunText));

    private bool busy = false;
    private CancellationTokenSource cts;

    public string SecondaryRunLabel
    {
      get { return (string)GetValue(SecondaryRunLabelProperty); }
      set { SetValue(SecondaryRunLabelProperty, value); }
    }

    public static readonly DependencyProperty SecondaryRunLabelProperty =
        DependencyProperty.Register("SecondaryRunLabel", typeof(string), typeof(MainWindow), new PropertyMetadata(defaultSecondaryRunText));



    public ObservableHashSet<string> Candidates
    {
      get => (ObservableHashSet<string>)GetValue(CandidatesProperty);
      set => SetValue(CandidatesProperty, value);
    }

    public static readonly DependencyProperty CandidatesProperty =
        DependencyProperty.Register("Candidates", typeof(ObservableHashSet<string>), typeof(MainWindow),
          new PropertyMetadata(new ObservableHashSet<string>()));



    public ObservableCollection<string> Log
    {
      get { return (ObservableCollection<string>)GetValue(LogProperty); }
      set { SetValue(LogProperty, value); }
    }

    public static readonly DependencyProperty LogProperty =
        DependencyProperty.Register("Log", typeof(ObservableCollection<string>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<string>()));




    public string InputText
    {
      get => (string) GetValue(InputTextProperty);
      set => SetValue(InputTextProperty, value);
    }

    public static readonly DependencyProperty InputTextProperty =
        DependencyProperty.Register("InputText", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty, InputChanged));

    public Type InputType
    {
      get => (Type)GetValue(InputTypeProperty);
      set => SetValue(InputTypeProperty, value);
    }

    public static readonly DependencyProperty InputTypeProperty =
        DependencyProperty.Register("InputType", typeof(Type), typeof(MainWindow), new PropertyMetadata(typeof(string)));

    public Type OutputType
    {
      get => (Type)GetValue(OutputTypeProperty);
      set => SetValue(OutputTypeProperty, value);
    }

    public static readonly DependencyProperty OutputTypeProperty =
        DependencyProperty.Register("OutputType", typeof(Type), typeof(MainWindow), new PropertyMetadata(typeof(string)));



    public bool? ScaleWindow
    {
      get => (bool?)GetValue(ScaleWindowProperty);
      set => SetValue(ScaleWindowProperty, value);
    }

    // Using a DependencyProperty as the backing store for ScaleWindow.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ScaleWindowProperty =
        DependencyProperty.Register("ScaleWindow", typeof(bool?), typeof(MainWindow), new PropertyMetadata(false));

    private object parsedInputValue = string.Empty, parsedOutputValue = string.Empty;

    private static void InputChanged(DependencyObject d, DependencyPropertyChangedEventArgs _)
    {
      InputChanged(d);
    }

    private static void InputChanged(DependencyObject d)
    {
      var self = (MainWindow) d;

      var parsedValues = self.InputText.RemoveMarkers().InferTypes();
      parsedValues.Add(self.InputText.RemoveMarkers());
      if (parsedValues.Any())
      {
        self.parsedInputValue = parsedValues[0];
        self.InputType = self.parsedInputValue.GetType();

        self.AlternateInputValues.Inlines.Clear();
        foreach (var i in parsedValues)
        {
          if (self.InputType != i.GetType())
          {
            var h = new Hyperlink();
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
              var friendlyName = currentType.GetFriendlyName();
              if (friendlyName == "RuntimeType")
                Debugger.Break();
              me.Inlines.Add(friendlyName);
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
      get => (string)GetValue(OutputTextProperty);
      set => SetValue(OutputTextProperty, value);
    }

    public static readonly DependencyProperty OutputTextProperty =
        DependencyProperty.Register("OutputText", 
          typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty, OutputChanged));



    public string Separator
    {
      get { return (string)GetValue(SeparatorProperty); }
      set { SetValue(SeparatorProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Separator.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SeparatorProperty =
        DependencyProperty.Register("Separator", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty, SeparatorChanged));

    private static void SeparatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      // when the separator is changed, we need to reevaluate both input and output
      var window = d as MainWindow;
      InputChanged(d);
      OutputChanged(d, window.InputText);
    }

    private static void OutputChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      OutputChanged(d, (string)e.NewValue);
    }


    private static void OutputChanged(DependencyObject d, string parsed)
    {
      var self = (MainWindow) d;

      var parsedValues = parsed.InferTypes(); // scalar types
      parsedValues.Add(parsed);
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

      Title += " v" + Assembly.GetEntryAssembly().GetName().Version.ToString(3);
    }

    private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
    {
      if (busy)
      {
        LogInfo("Cancellation requested. Should not have anything here.");
        cts.Cancel();
        busy = false;
      }
      else
      {
        busy = true;
        // do the search
        Candidates.Clear();
        Log.Clear();
        cts = new CancellationTokenSource();

        PrimaryRunLabel = "Cancel";
        SecondaryRunLabel = "search in progress";

        Task.Factory.StartNew(() =>
          dynamicMemberDatabase.FindCandidates(
                x => Dispatcher.Invoke(() =>
                {
                  LogInfo("Found a candidate: " + x);
                  Candidates.Add(x);
                }),
                parsedInputValue, parsedInputValue, parsedOutputValue, 0, cts.Token),
            cts.Token)
          .ContinueWith(t =>
          {
            PrimaryRunLabel = defaultPrimaryRunText;
            SecondaryRunLabel = "search " + (t.IsCanceled ? "canceled" : "completed");
          }, TaskScheduler.FromCurrentSynchronizationContext());
      }
    }

    private void LogInfo(string msg)
    {
      Log.Add($"{DateTime.Now.ToLongTimeString()}: {msg}");
    }

    private void BtnCopy_OnClick(object sender, RoutedEventArgs e)
    {
      if (LbCandidates.SelectedIndex >= 0)
      {
        Clipboard.SetText(LbCandidates.SelectedItem.ToString());
        TbInfo.Text = "Text copied to clipboard.";
      }
    }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
      ScaleWindow = CbScaleWindow.IsChecked;
    }

    private void BtnCopyAllCandidates_OnClick(object sender, RoutedEventArgs e)
    {
      var sb = new StringBuilder();
      foreach (var c in LbCandidates.Items)
      {
        sb.AppendLine(c.ToString());
      }
      Clipboard.SetText(sb.ToString());
    }
  }
}