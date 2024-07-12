using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab4.models;

namespace lab4.repositories
{

    public class UserRepository
    {
        private static DatabaseContext database;

        public UserRepository()
        {
            SQLitePCL.Batteries.Init();
            database = new DatabaseContext();
        }

        public static void Create(User user)
        {
            database.Add(user);
            database.SaveChanges();
        }

        public static List<User> GetAll()
        {
            return database.Set<User>().ToList();
        }

        public static User ReadWithLogin(string login)
        {
            return database.Users.FirstOrDefault(u => u.Login == login);
        }

        public static User ReadWithId(int id)
        {
            return database.Users.FirstOrDefault(u => u.Id == id);
        }

        public static void Delete(User user)
        {
            database.Users.Remove(user);
            database.SaveChanges();
        }

        public static void ResetPassword(User user, string password)
        {
            user.Password = password;
            database.SaveChanges();
        }
    }
}
