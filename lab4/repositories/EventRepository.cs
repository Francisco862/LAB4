using lab4.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.repositories {
    
    public class EventRepository {
        private static DatabaseContext database;

        public EventRepository() {
            SQLitePCL.Batteries.Init();
            database = new DatabaseContext();
        }

        public static void Create(Event newEvent) {
            database.Add(newEvent);
            database.SaveChanges();
        }

        public static List<Event> GetAll() {
            return database.Set<Event>().ToList();
        }

        public static void Delete(Event newEvent) {
            database.Events.Remove(newEvent);
            database.SaveChanges();
        }

        public static Event ReadWithName(string name) {
            return database.Events.FirstOrDefault(e => e.EventName == name);
        }

        public static void Modify(Event eventToModify, string name, string agenda) {
            eventToModify.EventName = name;
            eventToModify.Agenda = agenda;
            database.SaveChanges();
        }
    }
}
