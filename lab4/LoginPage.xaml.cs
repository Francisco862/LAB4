using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using lab4.models;
using lab4.repositories;

namespace lab4
{
    /// <summary>
    /// Logika interakcji dla klasy LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private User user;
        private int attempts = 0;

        public LoginPage()
        {
            InitializeComponent();
        }
        
        private void OnLoginBtn(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            Frame frame = ((MainWindow)window).Main;
            if(attempts < 3) {
                string userLogin = loginText.Text;
                string userPassword = passwordText.Password.ToString();
                user = UserRepository.ReadWithLogin(userLogin);
                if (user != null && user.Password == userPassword) {
                    if (user.Type == "user") {
                        EventRegistrationPage eventRegistrationPage = new EventRegistrationPage(user);
                        frame.Navigate(eventRegistrationPage);
                    } else if (user.Type == "admin") {
                        frame.Navigate(new Uri("AdministratorPage.xaml", UriKind.Relative));
                    }
                } else {
                    attempts++;
                    messageLabel.Content = "Nieprawidłowe dane";
                }
            } else {
                messageLabel.Content = "Przekroczono maksymalną ilość prób";
            }
        }

        private void ToRegisterPage(object sender, MouseButtonEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            Frame frame = ((MainWindow)window).Main;
            frame.Navigate(new Uri("RegistrationPage.xaml", UriKind.Relative));
        }

        private void ShowPassword(object sender, RoutedEventArgs e)
        {
            string password = passwordText.Password;
            passwordText.Visibility = Visibility.Collapsed;
            passwordVisibleBox.Visibility = Visibility.Visible;
            passwordVisibleBox.Text = password;
        }

        private void UnShowPassword(object sender, RoutedEventArgs e)
        {
            string password = passwordVisibleBox.Text;
            passwordText.Visibility = Visibility.Visible;
            passwordVisibleBox.Visibility = Visibility.Collapsed;
            passwordText.Password = password;
        }
    }
}
