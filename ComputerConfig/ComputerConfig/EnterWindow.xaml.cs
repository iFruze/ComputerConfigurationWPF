using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerConfig
{
    /// <summary>
    /// Логика взаимодействия для EnterWindow.xaml
    /// </summary>
    public partial class EnterWindow : Window
    {
        public EnterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //new AdminPanel().ShowDialog();
            if (string.IsNullOrEmpty(LoginBox.Text) || string.IsNullOrEmpty(PassBox.Password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string login = LoginBox.Text;
                string password = PassBox.Password;

                if (login == "Orlov" && password == "Admin2025")
                {
                    if(new AdminPanel().ShowDialog() == true)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
/*
Логин: Orlov
Пароль: Admin2025
*/
