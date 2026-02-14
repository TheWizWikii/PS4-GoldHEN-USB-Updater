using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;


#nullable enable
namespace PayloadUpdater
{
  public class MainWindow : Window
  {
    private const string GithubTxtUrl = "https://raw.githubusercontent.com/TheWizWikii/TheWizWikii.github.io/refs/heads/master/goldhen_link.txt";
    private DispatcherTimer _usbTimer;
    [GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.11.0")]
    internal 
    #nullable disable
    ComboBox ComboUsb;
    [GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.11.0")]
    internal TextBlock TxtStatus;

    public MainWindow()
    {
      this.InitializeComponent();
      this._usbTimer = new DispatcherTimer()
      {
        Interval = TimeSpan.FromSeconds(2.0)
      };
      this._usbTimer.Tick += (EventHandler) ((s, e) => this.RefreshUsbList());
      this._usbTimer.Start();
    }

    private void RefreshUsbList()
    {
      List<UsbDriveItem> list = Enumerable.ToList<UsbDriveItem>(Enumerable.Select<DriveInfo, UsbDriveItem>(Enumerable.Where<DriveInfo>((IEnumerable<DriveInfo>) DriveInfo.GetDrives(), (Func<DriveInfo, bool>) (d => d.DriveType == 2 && d.IsReady)), (Func<DriveInfo, UsbDriveItem>) (d =>
      {
        UsbDriveItem usbDriveItem = new UsbDriveItem();
        usbDriveItem.Path = ((FileSystemInfo) d.RootDirectory).FullName;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 3);
        interpolatedStringHandler.AppendFormatted(d.VolumeLabel);
        interpolatedStringHandler.AppendLiteral(" (");
        interpolatedStringHandler.AppendFormatted(d.Name);
        interpolatedStringHandler.AppendLiteral(") [");
        interpolatedStringHandler.AppendFormatted(d.DriveFormat);
        interpolatedStringHandler.AppendLiteral("]");
        usbDriveItem.DisplayName = interpolatedStringHandler.ToStringAndClear();
        return usbDriveItem;
      })));
      if (this.ComboUsb.ItemsSource != null && ((List<UsbDriveItem>) this.ComboUsb.ItemsSource).Count == list.Count)
        return;
      this.ComboUsb.ItemsSource = (IEnumerable) list;
      if (list.Count <= 0 || this.ComboUsb.SelectedIndex != -1)
        return;
      this.ComboUsb.SelectedIndex = 0;
    }

    private async 
    #nullable enable
    Task DownloadPayload(string key)
    {
      if (!(this.ComboUsb.SelectedItem is UsbDriveItem selectedUsb))
      {
        this.TxtStatus.Text = "❌ Error: Selecciona un USB de la lista.";
        selectedUsb = (UsbDriveItem) null;
      }
      else
      {
        try
        {
          this.TxtStatus.Text = "\uD83C\uDF10 Conectando para bajar " + key + "...";
          using (HttpClient client = new HttpClient())
          {
            string str1 = Enumerable.FirstOrDefault<string>((IEnumerable<string>) (await client.GetStringAsync("https://raw.githubusercontent.com/TheWizWikii/TheWizWikii.github.io/refs/heads/master/goldhen_link.txt")).Split('\n', (StringSplitOptions) 0), (Func<string, bool>) (l => l.Trim().StartsWith(key + "=")));
            if (string.IsNullOrEmpty(str1))
            {
              this.TxtStatus.Text = "❌ Error: No se encontró " + key + " en el .txt";
              selectedUsb = (UsbDriveItem) null;
              return;
            }
            string str2 = str1.Split('=', (StringSplitOptions) 0)[1].Trim();
            this.TxtStatus.Text = "\uD83D\uDCE5 Descargando a " + selectedUsb.DisplayName + "...";
            byte[] byteArrayAsync = await client.GetByteArrayAsync(str2);
            await File.WriteAllBytesAsync(Path.Combine(selectedUsb.Path, "payload.bin"), byteArrayAsync, new CancellationToken());
            this.TxtStatus.Text = "✅ ¡Éxito! payload.bin creado en " + selectedUsb.Path;
          }
          selectedUsb = (UsbDriveItem) null;
        }
        catch (Exception ex)
        {
          this.TxtStatus.Text = "❌ Error: " + ex.Message;
          selectedUsb = (UsbDriveItem) null;
        }
      }
    }

    public async void DownloadGoldHen_Click(object? sender, RoutedEventArgs e) => await this.DownloadPayload("GOLDHEN");

    public async void DownloadHen_Click(object? sender, RoutedEventArgs e) => await this.DownloadPayload("HEN");

    [GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.11.0")]
    [ExcludeFromCodeCoverage]
    public void InitializeComponent(bool loadXaml = true)
    {
      if (loadXaml)
      {
        // ISSUE: reference to a compiler-generated method
        MainWindow.\u0021XamlIlPopulateTrampoline(this);
      }
      INameScope nameScope = this.FindNameScope();
      this.ComboUsb = nameScope != null ? nameScope.Find<ComboBox>("ComboUsb") : (ComboBox) null;
      this.TxtStatus = nameScope != null ? nameScope.Find<TextBlock>("TxtStatus") : (TextBlock) null;
    }
  }
}
