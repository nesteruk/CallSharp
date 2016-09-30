using System.Linq;
using System.Windows;

namespace CallSharp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MemberDatabase memberDatabase = new MemberDatabase();
    private static object[] noArgs = {};

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

      // if input and output are identical, be sure to add it
      if (TbIn.Text == TbOut.Text)
        Candidates.Add("input");

      var input = TbIn.Text.InferTypes().FirstOrDefault() ?? TbIn.Text;
      var output = TbOut.Text.InferTypes().FirstOrDefault() ?? TbOut.Text;
     
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
        var actualOutput = p.GetMethod.Invoke(TbIn.Text, noArgs);
        if (output.Equals(actualOutput))
          Candidates.Add("input." + p.Name);
      }
    }
  }
}