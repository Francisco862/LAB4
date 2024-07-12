using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using lab4.models;
using lab4.repositories;

namespace lab4
{
    /// <summary>
    /// Interaction logic for AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page {
        private List<RegisteredEvent> registredEvents = RegisteredEventRepository.GetAll();

        public AdministratorPage() {
            InitializeComponent();
        }

        private void OnAddUserButton(object sender, RoutedEventArgs e) {
            string firstName = addUserFirstNameText.Text;
            string lastName = addUserLastNameText.Text;
            string login = addUserLoginText.Text;
            string password = addUserPasswordText.Text;
            string email = addUserEmailText.Text;
            string type = addUserType.Text;
            DateTime date = DateTime.Now;
            string dateToString = date.ToString("dd-MM-yyyy");
            User newUser = new User(firstName, lastName, login, password, email, type, dateToString);
            UserRepository.Create(newUser);
            addUserFirstNameText.Text = "";
            addUserLastNameText.Text = "";
            addUserLoginText.Text = "";
            addUserPasswordText.Text = "";
            addUserEmailText.Text = "";
        }

        private void OnDeleteUserButton(object sender, RoutedEventArgs e) {
            string login = deleteUserLoginText.Text;
            User user = UserRepository.ReadWithLogin(login);
            if(user != null) {
                UserRepository.Delete(user);
                deleteUserLoginText.Text = "";
            }
        }

        private void OnResetUserPasswordButton(object sender, RoutedEventArgs e) {
            string login = restUserPasswordText.Text;
            string newPwd = newUserPasswordText.Text;
            User user = UserRepository.ReadWithLogin(login);
            if(user != null) {
                UserRepository.ResetPassword(user, newPwd);
                restUserPasswordText.Text = "";
                newUserPasswordText.Text = "";
            }
        }

        private void OnAddEventButton(object sender, RoutedEventArgs e) {
            string eventName = addEventText.Text;
            string eventAgenda = addEventAgendaText.Text;
            Event newEvent = new Event(eventName, eventAgenda);
            EventRepository.Create(newEvent);
            addEventText.Text = "";
            addEventAgendaText.Text = "";
        }

        private void OnDeleteEventButton(object sender, RoutedEventArgs e) {
            string eventName = eventToDeleteNameText.Text;
            Event eventToDelete = EventRepository.ReadWithName(eventName);
            if(eventToDelete != null) {
                EventRepository.Delete(eventToDelete);
                eventToDeleteNameText.Text = "";
            }
        }

        private void OnModifyEventButton(object sender, RoutedEventArgs e) {
            string eventName = eventToModifyNameText.Text;
            string newEventName = newEventNameText.Text;
            string newAgenda = newEventAgendaText.Text;
            Event searchedEvent = EventRepository.ReadWithName(eventName);
            if(searchedEvent != null) {
                EventRepository.Modify(searchedEvent, newEventName, newAgenda);
                eventToModifyNameText.Text = "";
                newEventNameText.Text = "";
                newEventAgendaText.Text = "";
            }
        }

        private void LogoutButton(object sender, RoutedEventArgs e) {
            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            mainFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }
    }

}
