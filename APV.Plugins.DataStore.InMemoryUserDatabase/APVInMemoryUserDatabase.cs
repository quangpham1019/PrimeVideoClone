using APV.CoreBusiness;
using APV.UseCases.PluginInterfaces;

namespace APV.Plugins.DataStore.InMemoryUserDatabase
{
    // All the code in this file is included in all platforms.
    public class APVInMemoryUserDatabase : IUserDatabase
    {
        private Dictionary<string, UserInfo> userInfoDict;
        public APVInMemoryUserDatabase()
        {
            userInfoDict = InitializeUserInfos();

        }
        public Task<UserInfo> GetUserInfo(string username, string password)
        {
            // check if username exists in db, return null if not
            // check if provided password matches password in UserInfo from db, if not return null
            // return the UserInfo associated with username

            UserInfo userInfo = new UserInfo();
            if (!userInfoDict.TryGetValue(username, out UserInfo userInfoFromDb)) return Task.FromResult(userInfo);

            if (userInfoFromDb.Password != password) return Task.FromResult(userInfo);
            
            return Task.FromResult(userInfoFromDb);
        }

        Dictionary<string, UserInfo> InitializeUserInfos()
        {
            Dictionary<string, UserInfo> userInfos = new Dictionary<string, UserInfo>();
            string[] names = new string[] { "admin", "john", "tracy", "david" };
            
            foreach (string name in names)
            {
                userInfos.Add(name, new UserInfo { Username = $"{name}", Email = $"{name}@gmail.com", Password = $"{name}123" });
            }

            return userInfos;
        }
    }
}
