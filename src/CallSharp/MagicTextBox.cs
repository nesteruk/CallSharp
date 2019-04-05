using System.Windows;
using System.Windows.Controls;

namespace CallSharp
{
  /// <summary>
  /// This is a text box similar to an ordinary text box except it
  /// displays space as ⎵.
  /// </summary>
  public class MagicTextBox : TextBox
  {
    public const char SpaceChar = '⎵';

    static MagicTextBox()
    {
      TextProperty.OverrideMetadata(typeof(MagicTextBox),
        new FrameworkPropertyMetadata("", TextProperty.DefaultMetadata.PropertyChangedCallback,
        textValueChanged));
    }

    private static object textValueChanged(DependencyObject d, object basevalue)
    {
      return basevalue.ToString().Replace(' ', SpaceChar);
    }

    public string ActualText => Text.Replace(SpaceChar, ' ');
  }
}