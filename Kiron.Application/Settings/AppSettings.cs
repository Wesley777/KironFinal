namespace Kiron.Application.Settings;
public class AppSettings
{
    public string Token { get; set; }
    public int NavigationCacheDurationMin { get; set; }
    public int TokenDurationHours { get; set; }
    public int ThirdPartyCacheDurationMin { get; set; }
}
