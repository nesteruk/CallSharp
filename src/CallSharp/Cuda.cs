using System.Runtime.InteropServices;

namespace CallSharp
{
  /// <summary>
  /// This file is used to proxy certain calls into CUDA kernels that speed up certain calculations.
  /// </summary>
  public abstract class Cuda
  {
    [DllImport("CallSharp.Cuda.dll")]
    public static extern void foo();
  }
}