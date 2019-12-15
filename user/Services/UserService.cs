using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using user.Models;

namespace user.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IUserDatabaseSettings settings)
        {
            var client = new MongoClient("");
            var database = client.GetDatabase("test");

            _user = database.GetCollection<User>("Users");
        }

        public List<User> Get() =>
            _user.Find(user => true).ToList();

        public User Get(string id) =>
            _user.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void Update(string id, User user) =>
            _user.ReplaceOne(user => user.Id == id, user);

        public void Remove(User user) =>
            _user.DeleteOne(user => user.Id == user.Id);

        public void Remove(string id) =>
            _user.DeleteOne(user => user.Id == id);
    }
}
