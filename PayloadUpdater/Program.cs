using Avalonia;
using Avalonia.Logging;
using System;


#nullable enable
namespace PayloadUpdater
{
  internal class Program
  {
    [STAThread]
    public static void Main(string[] args) => Program.BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>().UsePlatformDetect().WithInterFont().LogToTrace(LogEventLevel.Warning);
  }
}
