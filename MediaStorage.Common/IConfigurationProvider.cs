namespace MediaStorage.Common
{
    public interface IConfigurationProvider
    {
        string GetAppSetting(string settingName);
        string GetDBConnectionString(string settingName);
    }
}