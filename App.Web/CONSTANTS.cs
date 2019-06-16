

namespace App.Web
{
    public static class CONSTANTS
    {
        public struct SessionKeys
        {
            public const string ACTIVE_APPLICATION = "ActiveApplication";
            public const string HOUSE_MEMBERS = "HouseMembers";
        }
        public struct ApiUrls
        {
            public const string BASE_ADDRESS = "http://localhost:53290/api/";
            public const string APPLICATION_SAVE = "Application/Save";
            public const string APPLICATION_GET = "Application/Get/{0}";
            public const string RELATIONSHIP_SAVE = "HoseMember/SaveRelationships";
        }
    }
}