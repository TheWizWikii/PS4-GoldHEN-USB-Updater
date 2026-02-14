#nullable enable
namespace PayloadUpdater
{
  public class UsbDriveItem
  {
    public string Path { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public virtual string ToString() => this.DisplayName;
  }
}
