
 // mscorlib
       // IsNullOrEmpty // IsNullOrWhiteSpace // Microsoft.VisualStudio.Platform.AppDomainManager
       // System
       // System.Windows.Forms
       // Microsoft.VisualStudio.TextTemplating.14.0
       // Microsoft.VisualStudio.TextTemplating.Interfaces.11.0
       // Microsoft.VisualStudio.TextTemplating.Interfaces.10.0
       // System.Core
       // WindowsBase
       // Microsoft.VisualStudio.TextTemplating.VSHost.14.0
       // Microsoft.CodeAnalysis
       // System.Runtime
       // System.Collections.Immutable
       // System.Threading.Tasks
       // System.IO
       // System.Reflection.Metadata
       // System.Collections
       // System.Runtime.Extensions
       // System.Linq
       // Microsoft.CodeAnalysis.CSharp
       // System.Text.Encoding
       // System.Threading
       // System.Globalization
       // System.Dynamic.Runtime
       // System.Reflection
       // System.Threading.Tasks.Parallel
       // System.Reflection.Primitives
       // System.Runtime.InteropServices
       // System.IO.FileSystem.Primitives
       // System.IO.FileSystem
       // System.Collections.Concurrent
       // System.Text.Encoding.Extensions
       // System.Reflection.Extensions
       // System.Resources.ResourceManager
       // System.Xml.XDocument
       // System.Xml.Linq
       // System.Xml
       // System.Xml.ReaderWriter
       // TemporaryT4Assembly
       // Microsoft.CSharp
       // Anonymously Hosted DynamicMethods Assembly
       // cannot get exported types of Anonymously Hosted DynamicMethods Assembly  // System.Dynamic
       // System.Configuration
       // Accessibility
       // System.Drawing
       // System.ComponentModel.Composition
       // System.Xaml
       // Microsoft.VisualStudio.Shell.14.0
       // Microsoft.VisualStudio.Shell.Interop.8.0
       // Microsoft.VisualStudio.Shell.Immutable.10.0
       // Microsoft.VisualStudio.Shell.Interop
       // Microsoft.VisualStudio.OLE.Interop
       // TemporaryT4Assembly
       // Microsoft.VisualStudio.Utilities
       // PresentationCore
       // Microsoft.VisualStudio.Shell.Immutable.11.0
       // Microsoft.VisualStudio.Shell.Interop.9.0
       // System.Design
       // Microsoft.VisualStudio.Shell.Immutable.14.0
       // Microsoft.VisualStudio.ProjectAggregator
       // PresentationFramework
       // UIAutomationProvider
       // UIAutomationTypes
       // Microsoft.VisualStudio.Imaging
       // Microsoft.VisualStudio.Shell.Immutable.12.0
       // Microsoft.VisualStudio.TextManager.Interop
       // TemporaryT4Assembly
      
namespace CallSharp
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;

  public class StaticMemberDatabase : IMemberDatabase
  {
    public IEnumerable<MethodInfo> FindAnyToOneInstance(Type inputType, Type ignoreThisOutputType)
    {
      throw new NotImplementedException();
          }

    public IEnumerable<MethodInfo> FindAnyToOneStatic(Type inputType, Type ignoreThisOutputType)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<ConstructorInfo> FindConstructorFor(Type inputType, Type outputType)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<MethodInfo> FindOneToOneInstance(Type inputType, Type outputType)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<MethodInfo> FindOneToTwoInstance(Type inputType, Type outputType)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<MethodInfo> FindOneToOneStatic(Type inputType, Type outputType)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<MethodInfo> FindOneToThreeInstance(Type inputType, Type outputType)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<string> FindCandidates(object input, object output, int depth,
      string callChain = "input")
    {
      throw new NotImplementedException();
    }
  }
}