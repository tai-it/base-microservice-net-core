namespace Simple.Core.Models.Common
{
    public static class CommonConstants
    {
        public class Config
        {
            public const int DEFAULT_SKIP = 1;
            public const int DEFAULT_TAKE = 30;
        }

        public class Roles
        {
            public const string SUPER_ADMIN = "Super Admin";
            public const string ADMIN = "Admin";
            public const string MEMBER = "Member";
        }

        public class Messages
        {
            public const string FILE_REQUIRED = "File is required";
        }
    }
}
