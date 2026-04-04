using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Windows.UI.Text;

namespace WindowsPrivacyAutomator.Models;


public enum LogType { Header, Setting, Error }

public record LogEntry(string Message, LogType Type)
{
    public int Column => Type == LogType.Header ? 0 : 1;

    public int ColumnSpan => Type == LogType.Header ? 2 : 1;
    public double FontSize => Type == LogType.Header ? 16 : 14;

    public FontWeight FontWeight => Type == LogType.Header ? FontWeights.Bold : FontWeights.Normal;

    public Visibility SettingIconVisibility => Type == LogType.Setting ? Visibility.Visible : Visibility.Collapsed;
    public Visibility ErrorIconVisibility => Type == LogType.Error ? Visibility.Visible : Visibility.Collapsed;

    public Microsoft.UI.Xaml.Thickness Margin => Type == LogType.Header ? new Microsoft.UI.Xaml.Thickness(0, 12, 0, 4) : new Microsoft.UI.Xaml.Thickness(0, 4, 0, 4);
}