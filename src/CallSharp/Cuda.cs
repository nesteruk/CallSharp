using System.Runtime.InteropServices;

namespace CallSharp
{
  public abstract class Cuda
  {
    [DllImport("CallSharp.Cuda.dll")]
    public static extern void foo();
  }
}