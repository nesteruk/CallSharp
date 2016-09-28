using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CallSharp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {


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
      string input = TbIn.Text;
      string output = TbOut.Text;

      // get all types corresponding to the input
      var types = input.InferTypes();

      // search single string-to-string chains
      foreach (var method in typeof(string).GetMethods())
      {
        // ensure this takes a string and returns a string
        var pars = method.GetParameters();

        var isSingleParams = (pars.Length == 1 && pars[0].IsParams());
        if (!method.IsStatic &&
            method.ReturnType == typeof(string) &&
            (pars.Length == 0 
             || pars.AllAreOptional() 
             || isSingleParams))
        {
          // try calling it and getting the result
          string result;

          if (isSingleParams)
            result = (string) method.Invoke((object) input,
              new[]
              {
                Activator.CreateInstance(
                  pars[0].ParameterType.UnderlyingSystemType, 0)
              });
          else
            result = (string) method.Invoke(input, new object[] {});
          
          if (result == output)
          {
            Candidates.Add("input." + method.Name + "(output)");
          }
        }
      }
    }
  }
}
