using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using lab4.models;
using lab4.repositories;

namespace lab4
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page {
        User user;

        public RegistrationPage() {
            InitializeComponent();
        }   

        private void OnLoginButton(object sender, RoutedEventArgs e) {
            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            string firstName = firstNameText.Text;
            string lastName = lastNameText.Text;
            string login = loginText.Text;
            string email = emailText.Text;
            string password = passwordText.Password.ToString();
            string repeatPassword = repeatPasswordText.Password.ToString();
            string type = "user";
            DateTime date = DateTime.Now;
            string dateToString = date.ToString("dd-MM-yyyy");

            bool isDataOk = true;
            if (firstName == "" || lastName == "" || login == "" || email == "" || password == "") {
                isDataOk = false;
                messageLabel.Content += "Niekompletne dane\n";
            }
            if (!EmailValidation(email)) {
                isDataOk = false;
                messageLabel.Content += "Niepoprawny email\n";
            }
            if (!PasswordValidation(password)) {
                isDataOk = false;
                messageLabel.Content += "Niepoprawne hasło\n";
            }
            if (password != repeatPassword) {
                isDataOk = false;
                messageLabel.Content += "Hasła nie są takie same\n";
            }

            if (isDataOk) {
                user = new User(firstName, lastName, login, password, email, type, dateToString);
                UserRepository.Create(user);
                EventRegistrationPage eventRegistrationPage = new EventRegistrationPage(user);
                mainFrame.Navigate(eventRegistrationPage);
            }
        }

        private void ToLoginPage(object sender, MouseButtonEventArgs e) {
            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            mainFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }

        private bool EmailValidation(string email) {
            string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegex);
        }

        private bool PasswordValidation(string password) {
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
            return Regex.IsMatch(password, passwordRegex);
        }
    }
}
