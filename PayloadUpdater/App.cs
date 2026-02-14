using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace PayloadUpdater
{
  public class App : Application
  {
    public override void Initialize() => App.\u0021XamlIlPopulateTrampoline(this);

    public override void OnFrameworkInitializationCompleted()
    {
      if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime applicationLifetime)
        applicationLifetime.MainWindow = (Window) new MainWindow();
      base.OnFrameworkInitializationCompleted();
    }
  }
}
