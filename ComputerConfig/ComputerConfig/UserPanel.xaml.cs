using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        int typeId;
        BindingList<ComponentsView> components;
        static Configuration configuration = new Configuration();
        GetConfiguration getConig = new GetConfiguration(GetConfig);
        public static void GetConfig(Configuration config)
        {
            configuration = config;
        }
        public UserPanel(int typeId)
        {
            InitializeComponent();
            this.typeId = typeId;
            components = new BindingList<ComponentsView>();
            foreach (var component in ConfigDB.GetContext().Components.Where(t => t.type_id == typeId).ToList())
            {
                components.Add(new ComponentsView(component));
            }
            CompView.ItemsSource = components;
        }

        private void CatBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            CompView.ItemsSource = null;
            components = new BindingList<ComponentsView>();
            foreach (var component in ConfigDB.GetContext().Components.Where(t => t.type_id == typeId).ToList())
            {
                components.Add(new ComponentsView(component));
            }
            BindingList<ComponentsView> filters = new BindingList<ComponentsView>();
            if (AvailBox.SelectedItem != null)
            {
                if (AvailBox.SelectedIndex == 0)
                {
                    var temp = components.Where(c => c.availability == "В наличии").ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
                else
                {
                    var temp = components.Where(c => c.availability == "Не в наличии").ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
            }
            if (CatBox.SelectedItem != null)
            {
                filters.Clear();
                foreach (var c in components)
                {
                    if (c.cat.Trim() == CatBox.SelectedItem.ToString().Trim())
                    {
                        filters.Add(c);
                    }
                }
                components.Clear();
                foreach (var c in filters)
                {
                    components.Add(c);
                }
                filters.Clear();
            }
            if (CostBox.SelectedItem != null)
            {
                if (CostBox.SelectedIndex == 0)
                {
                    var temp = components.OrderBy(c => c.cost).ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
                else
                {
                    var temp = components.OrderByDescending(c => c.cost).ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
            }
            CompView.ItemsSource = components;
        }

        private void CostBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            CompView.ItemsSource = null;
            components = new BindingList<ComponentsView>();
            foreach (var component in ConfigDB.GetContext().Components.Where(t => t.type_id == typeId).ToList())
            {
                components.Add(new ComponentsView(component));
            }
            BindingList<ComponentsView> filters = new BindingList<ComponentsView>();
            if (AvailBox.SelectedItem != null)
            {
                if (AvailBox.SelectedIndex == 0)
                {
                    var temp = components.Where(c => c.availability == "В наличии").ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
                else
                {
                    var temp = components.Where(c => c.availability == "Не в наличии").ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
            }
            if (CatBox.SelectedItem != null)
            {
                filters.Clear();
                foreach (var c in components)
                {
                    if (c.cat.Trim() == CatBox.SelectedItem.ToString().Trim())
                    {
                        filters.Add(c);
                    }
                }
                components.Clear();
                foreach (var c in filters)
                {
                    components.Add(c);
                }
                filters.Clear();
            }
            if (CostBox.SelectedItem != null)
            {
                if (CostBox.SelectedIndex == 0)
                {
                    var temp = components.OrderBy(c => c.cost).ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
                else
                {
                    var temp = components.OrderByDescending(c => c.cost).ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
            }
            CompView.ItemsSource = components;
        }

        private void AddToConfig_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы точно хотите добавить данный компонент в вашу конфигурацию?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var compView = button?.DataContext as ComponentsView;
                int id = ConfigDB.GetContext().Categories.FirstOrDefault(c => c.name == compView.cat).id;
                switch (id)
                {
                    case 1:
                        if(configuration.processor != null)
                        {
                            if (MessageBox.Show("Компонент такого типа уже указан в вашей конфигурации. Хотите заменить его?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                configuration.processor = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                                MessageBox.Show("Конфигурация была успешно изменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            configuration.processor = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                            MessageBox.Show("Компонент был успешно добавлен в вашу конфигурацию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 2:
                        if (configuration.plata != null)
                        {
                            if (MessageBox.Show("Компонент такого типа уже указан в вашей конфигурации. Хотите заменить его?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                configuration.plata = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                                MessageBox.Show("Конфигурация была успешно изменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            configuration.plata = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                            MessageBox.Show("Компонент был успешно добавлен в вашу конфигурацию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 3:
                        if (configuration.videoKarta != null)
                        {
                            if (MessageBox.Show("Компонент такого типа уже указан в вашей конфигурации. Хотите заменить его?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                configuration.videoKarta = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                                MessageBox.Show("Конфигурация была успешно изменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            configuration.videoKarta = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                            MessageBox.Show("Компонент был успешно добавлен в вашу конфигурацию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 4:
                        if (configuration.nokopitel != null)
                        {
                            if (MessageBox.Show("Компонент такого типа уже указан в вашей конфигурации. Хотите заменить его?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                configuration.nokopitel = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                                MessageBox.Show("Конфигурация была успешно изменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            configuration.nokopitel = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                            MessageBox.Show("Компонент был успешно добавлен в вашу конфигурацию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 5:
                        if (configuration.blockPitaniua != null)
                        {
                            if (MessageBox.Show("Компонент такого типа уже указан в вашей конфигурации. Хотите заменить его?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                configuration.blockPitaniua = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                                MessageBox.Show("Конфигурация была успешно изменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            configuration.blockPitaniua = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                            MessageBox.Show("Компонент был успешно добавлен в вашу конфигурацию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 6:
                        if (configuration.cold != null)
                        {
                            if (MessageBox.Show("Компонент такого типа уже указан в вашей конфигурации. Хотите заменить его?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                configuration.cold = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                                MessageBox.Show("Конфигурация была успешно изменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            configuration.cold = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                            MessageBox.Show("Компонент был успешно добавлен в вашу конфигурацию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 7:
                        if (configuration.korpus != null)
                        {
                            if (MessageBox.Show("Компонент такого типа уже указан в вашей конфигурации. Хотите заменить его?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                configuration.korpus = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                                MessageBox.Show("Конфигурация была успешно изменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            configuration.korpus = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                            MessageBox.Show("Компонент был успешно добавлен в вашу конфигурацию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 8:
                        if (configuration.adapter != null)
                        {
                            if (MessageBox.Show("Компонент такого типа уже указан в вашей конфигурации. Хотите заменить его?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                configuration.adapter = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                                MessageBox.Show("Конфигурация была успешно изменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            configuration.adapter = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                            MessageBox.Show("Компонент был успешно добавлен в вашу конфигурацию.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    default:
                        MessageBox.Show("Произошла неизвестная ошибка.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
        }

        private void CompInfo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var compView = button.DataContext as ComponentsView;
            new FullInformationAboutComponent(compView).ShowDialog();
        }

        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            AvailBox.SelectedItem = null;
            CatBox.SelectedItem = null;
            CostBox.SelectedItem = null;
            components = new BindingList<ComponentsView>();
            foreach (var component in ConfigDB.GetContext().Components.Where(t => t.type_id == typeId).ToList())
            {
                components.Add(new ComponentsView(component));
            }
            CompView.ItemsSource = components;
        }

        private void AvailBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            CompView.ItemsSource = null;
            components = new BindingList<ComponentsView>();
            foreach (var component in ConfigDB.GetContext().Components.Where(t => t.type_id == typeId).ToList())
            {
                components.Add(new ComponentsView(component));
            }
            BindingList<ComponentsView> filters = new BindingList<ComponentsView>();
            if (AvailBox.SelectedItem != null)
            {
                if(AvailBox.SelectedIndex == 0)
                {
                    var temp = components.Where(c => c.availability == "В наличии").ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
                else
                {
                    var temp = components.Where(c => c.availability == "Не в наличии").ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
            }
            if (CatBox.SelectedItem != null)
            {
                filters.Clear();
                foreach (var c in components)
                {
                    if (c.cat.Trim() == CatBox.SelectedItem.ToString().Trim())
                    {
                        filters.Add(c);
                    }
                }
                components.Clear();
                foreach (var c in filters)
                {
                    components.Add(c);
                }
                filters.Clear();
            }
            if (CostBox.SelectedItem != null)
            {
                if (CostBox.SelectedIndex == 0)
                {
                    var temp = components.OrderBy(c => c.cost).ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
                else
                {
                    var temp = components.OrderByDescending(c => c.cost).ToList();
                    components.Clear();
                    foreach (var t in temp)
                    {
                        components.Add(t);
                    }
                }
            }
            CompView.ItemsSource = components;
        }

        private void ConfigInfo_Click(object sender, RoutedEventArgs e)
        {
            new ConfigurationWindow(getConig, configuration).ShowDialog();
        }
    }
    public delegate void GetConfiguration(Configuration config);
}
