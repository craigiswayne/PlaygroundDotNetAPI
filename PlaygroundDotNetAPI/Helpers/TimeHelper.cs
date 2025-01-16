namespace PlaygroundDotNetAPI.Helpers;

public static class TimeHelper
{
    public const int OneMonthInSeconds = OneDayInSeconds * 30;
    public const int OneWeekInSeconds = OneDayInSeconds * 7;
    public const int OneDayInSeconds = OneHourInSeconds * 24;
    public const int OneHourInSeconds = OneMinuteInSeconds * 60;
    public const int OneMinuteInSeconds = 60;
}