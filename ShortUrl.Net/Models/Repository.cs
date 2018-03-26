using System;
using System.Linq;
using System.Collections.Generic;
using LiteDB;

namespace ShortUrlNet.Models
{
    public class Repository : IRepository
    {
        private string _db = "./AppData/data.db";
        private string _urls = "urls";
        private string _users = "users";
        private string _keys = "apikey";

        public Repository()
        {

            using (var db = new LiteDatabase(_db))
            {
                var urls = db.GetCollection<ShortUrl>(_urls);
                urls.EnsureIndex("UserKey");
                var users = db.GetCollection<User>(_users);
                users.EnsureIndex("Login");
                var keys = db.GetCollection<ApiKey>(_keys);
                keys.EnsureIndex("UserKey");
            }
            
        }

        
        private T SaveItem<T>(object item) where T : CollectionItem
        {
            var name = string.Empty;
            switch (item)
            {
                case ShortUrl s:
                    name = _urls;
                    break;
                case User s:
                    name = _users;
                    break;
                case ApiKey s:
                    name = _keys;
                    break;
            }

            using (var db = new LiteDatabase(_db))
            {
                var collection = db.GetCollection<T>(name);
                var dbItem = item as T;
                collection.Upsert(dbItem);
                return collection.FindOne(x => x.Key == dbItem.Key);
            }
        }

        public ShortUrl AddUrl(ShortUrl url)
        {
            return SaveItem<ShortUrl>(url);
        }

        public ShortUrl GetUrl(string key)
        {
            var k = new ObjectId(key);
            using (var db = new LiteDatabase(_db))
            {
                var c = db.GetCollection<ShortUrl>(_urls);
                return c.FindOne(x => x.Key == k);
            }
        }

        public void ViewUrl(ShortUrl url)
        {
            url.ViewCount++;
            SaveItem<ShortUrl>(url);
        }


        public User AddUser(User user)
        {
            return SaveItem<User>(user);
        }

        public User GetUser(string login)
        {
            using (var db = new LiteDatabase(_db))
            {
                var c = db.GetCollection<User>(_users);
                return c.FindOne(x => x.Login == login);
            }
        }

        public User GetUser(ObjectId key)
        {
            using (var db = new LiteDatabase(_db))
            {
                var c = db.GetCollection<User>(_users);
                return c.FindOne(x => x.Key == key);
            }
        }

        public ApiKey AddApiKey(ApiKey key)
        {
            return SaveItem<ApiKey>(key);
        }

        public ApiKey GetApiKey(string key)
        {
            using (var db = new LiteDatabase(_db))
            {
                var k = new ObjectId(key);
                var c = db.GetCollection<ApiKey>(_keys);
                return c.FindOne(x => x.Key == k);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var db = new LiteDatabase(_db))
            {
                var c = db.GetCollection<User>(_users);
                return c.FindAll();
            }
        }

        public int GetUsersCout()
        {
            using (var db = new LiteDatabase(_db))
            {
                var c = db.GetCollection<User>(_users);
                return c.Count();
            }
        }
    }
}
