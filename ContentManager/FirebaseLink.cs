using ContentManager.Models;
using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ContentManager
{
    public class FirebaseLink
    {
        string basePath = "https://contentmanager-45789-default-rtdb.firebaseio.com/";

        //Q Initialize Firebase
        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "Y0v7LwjGT8UVtuXIbUv24M60EHd7plsxYHBUeDyP",
            BasePath = "https://contentmanager-45789-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        //Q The Method below will Generate a UserId
        private string CreateId()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;

            //Q Reference this Correctly
            //https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
        }

        //Q The Method below will create a User
        public void AddUser(User user)
        {
            string firebaseUserId = CreateId();

            client = new FireSharp.FirebaseClient(config);

            //Q Assign Values to User
            user.id = firebaseUserId;
            user.admin = false;

            var result = new FirebaseClient(basePath).Child("Users").Child(user.id).PutAsync(user);

            //Q Reference this Correctly
            //https://www.youtube.com/watch?v=GH_mHALFUTU
        }

        //Q The Method below will create an Admin
        public void AddAdmin(User user)
        {
            string firebaseUserId = CreateId();

            client = new FireSharp.FirebaseClient(config);

            //Q Assign Values to User
            user.id = firebaseUserId;
            user.admin = true;

            var result = new FirebaseClient(basePath).Child("Users").Child(user.id).PutAsync(user);
        }

        //Q The Method below will create a Category
        public void AddCategory(Category category)
        {
            string firebaseUserId = CreateId();

            client = new FireSharp.FirebaseClient(config);

            //Q Assign Values to Category
            category.categoryId = firebaseUserId;

            var result = new FirebaseClient(basePath).Child("Categories/").Child(category.categoryId).PutAsync(category);
        }

        public void DeleteAdmin(string id)
        {
            var result = new FirebaseClient(basePath).Child("Users/").Child(id).DeleteAsync();
        }

        public void DeleteCategory(string id)
        {
            var result = new FirebaseClient(basePath).Child("Categories/").Child(id).DeleteAsync();
        }

        public List<Category> PullCategoryList()
        {
            List<Category> categoryList = new List<Category>();

            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse response = client.Get("Categories");

            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            foreach (var item in data)
            {
                categoryList.Add(JsonConvert.DeserializeObject<Category>(((JProperty)item).Value.ToString()));
            }

            return categoryList;
        }

        public List<User> PullUserList()
        {
            List<User> userList = new List<User>();

            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse response = client.Get("Users");

            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            foreach (var item in data)
            {
                userList.Add(JsonConvert.DeserializeObject<User>(((JProperty)item).Value.ToString()));
            }

            return userList;
        }
    }
}
