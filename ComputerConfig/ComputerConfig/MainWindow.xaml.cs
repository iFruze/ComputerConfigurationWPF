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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerConfig
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConfigBox.ItemsSource = ConfigDB.GetContext().Types.Select(t => t.name).ToList();
        }

        private void Enter_Click(object sender, RoutedEventArgs e) => new EnterWindow().ShowDialog();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ConfigBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип компьютера для подборки нужных компонентов.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int id = ConfigDB.GetContext().Types.FirstOrDefault(t => t.name.Trim() == ConfigBox.SelectedItem.ToString().Trim()).id;
                new UserPanel(id).ShowDialog();
            }
        }
    }
}
