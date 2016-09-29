using System.Linq;
using System.Windows;

namespace CallSharp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MethodDatabase methodDatabase = new MethodDatabase();

    public ObservableHashSet<string> Candidates
    {
      get { return (ObservableHashSet<string>)GetValue(CandidatesProperty); }
      set { SetValue(CandidatesProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Candidates.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CandidatesProperty =
        DependencyProperty.Register("Candidates", typeof(ObservableHashSet<string>), typeof(MainWindow), 
          new PropertyMetadata(new ObservableHashSet<string>()));

    public MainWindow()
    {
      InitializeComponent();
    }

    private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
    {
      Candidates.Clear();
      var input = TbIn.Text;
      var output = TbOut.Text;

      // if input and output are identical, be sure to add it
      if (input == output)
        Candidates.Add("input");

      var interpretedInput = input.InferTypes().FirstOrDefault() ?? input;
      var interpretedOutput = output.InferTypes().FirstOrDefault() ?? output;
     
      foreach (
        var m in methodDatabase.FindOneToOneNonStatic(
          interpretedInput.GetType(), interpretedOutput.GetType()))
      {
        object actualOutput = m.InvokeWithNoArgument(interpretedInput);
        if (output.Equals(actualOutput))
          Candidates.Add("input." + m.Name + "()");
      }

      foreach (
        var m in methodDatabase.FindOneToOneStatic(
          interpretedInput.GetType(), interpretedOutput.GetType()))
      {
        var actualOutput = m.InvokeStaticWithSingleArgument(interpretedInput);
        if (output.Equals(actualOutput))
          Candidates.Add($"{m.DeclaringType?.Name}.{m.Name}(input)");
      }
    }
  }
}
