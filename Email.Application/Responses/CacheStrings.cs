namespace Email.Application.Responses
{
    public static class CacheStrings
    {
        public const string KeyCacheIncorrect = "Key is null or empty.";
        public const string SaveFailed = "Unfortunately, but failed to save data to the cache.";

        public static string SavePositive(string key)
        {
            return $"Data were saved. Key - {key}";
        }
    }
}
