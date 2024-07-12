using lab4.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.repositories {
    
    public class RegisteredEventRepository {
        private static DatabaseContext database;

        public RegisteredEventRepository() {
            SQLitePCL.Batteries.Init();
            database = new DatabaseContext();
        }

        public static void Create(RegisteredEvent registeredEvent) {
            database.Add(registeredEvent);
            database.SaveChanges();
        }

        public static List<RegisteredEvent> GetAll() {
            return database.Set<RegisteredEvent>().ToList();
        }

        public static void Delete(RegisteredEvent registeredEvent) {
            database.RegisteredEvents.Remove(registeredEvent);
            database.SaveChanges();
        }

        public static RegisteredEvent ReadWithId(int id) {
            return database.RegisteredEvents.FirstOrDefault(re => re.Id == id);
        }

        public static void Modify(RegisteredEvent registeredEvent, bool isAccepted) {
            registeredEvent.IsAccepted = isAccepted;
            database.SaveChanges();
        }
    }
}
