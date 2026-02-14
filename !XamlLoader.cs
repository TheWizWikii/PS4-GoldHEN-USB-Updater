using PayloadUpdater;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CompiledAvaloniaXaml
{
  [EditorBrowsable]
  [CompilerGenerated]
  public class \u0021XamlLoader
  {
    public static object TryLoad([In] IServiceProvider obj0, [In] string obj1)
    {
      if (string.Equals(obj1, "avares://PS4 goldHEN USB UPDATER/App.axaml", (StringComparison) 5))
        return (object) new App();
      return string.Equals(obj1, "avares://PS4 goldHEN USB UPDATER/MainWindow.axaml", (StringComparison) 5) ? (object) new MainWindow() : (object) null;
    }

    public static object TryLoad([In] string obj0) => \u0021XamlLoader.TryLoad((IServiceProvider) null, obj0);
  }
}
