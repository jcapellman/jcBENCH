namespace jcBENCH.lib.Common
{
    public static class Constants
    {
        public const string APP_NAME = "jcBENCH";

        public const string APP_VERSION = "2018.2.0";

#if DEBUG
        public const string WEB_SERVICE_URL = "http://localhost:55343/api/";
#else
        public const string WEB_SERVICE_URL = "http://www.jcbench.com/api/";
#endif
    }
}