using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
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
using System.IO;
using System.ComponentModel;

namespace ComputerConfig
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        ImageSource componentImg = null;
        string imagePath = "";
        public AdminPanel()
        {
            InitializeComponent();
            var cats = ConfigDB.GetContext().Categories.Select(c => c.name).ToList();
            var types = ConfigDB.GetContext().Types.Select(c => c.name).ToList();
            CatComponentBox.ItemsSource = cats;
            TypeComponentBox.ItemsSource = types;
            CatBox.ItemsSource = cats;
            TypeBox.ItemsSource = types;
            components = new BindingList<ComponentsView>();
            foreach ( var c in ConfigDB.GetContext().Components.ToList() )
            {
                components.Add(new ComponentsView(c));
            }
            CompView.ItemsSource = components;
        }
        static BindingList<ComponentsView> components;
        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            TypeBox.SelectedItem = null;
            CatBox.SelectedItem = null;
            CostBox.SelectedItem = null;
            components = new BindingList<ComponentsView>();
            foreach (var c in ConfigDB.GetContext().Components.ToList())
            {
                components.Add(new ComponentsView(c));
            }
            CompView.ItemsSource = components;
        }

        private void AddImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if(openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                componentImg = new BitmapImage(new Uri(imagePath));
                ComponentImage.Source = componentImg;
            }
        }

        private void AddComponent_Click(object sender, RoutedEventArgs e)
        {
            double costOfComponent = 0;
            List<string> errors = new List<string>();
            if(componentImg == null)
            {
                errors.Add("Выберите картинку для компонента.");
            }
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                errors.Add($"Введите корректное название для компонента.");
            }
            if (string.IsNullOrEmpty(CostText.Text) || !double.TryParse(CostText.Text.Replace('.', ','), out costOfComponent) || costOfComponent <= 0)
            {
                errors.Add($"Введите корректную цену для компонента.");
            }
            if (string.IsNullOrEmpty(DescBox.Text))
            {
                errors.Add($"Введите корректное описание для компонента.");
            }
            if (AvailBox.SelectedItem == null)
            {
                errors.Add($"Укажите начилие компонента.");
            }
            if (CatComponentBox.SelectedItem == null)
            {
                errors.Add($"Укажите категорию компонента.");
            }
            if (TypeComponentBox.SelectedItem == null)
            {
                errors.Add($"Укажите тип компьютера для компонента.");
            }
            if (errors.Any())
            {
                StringBuilder sb = new StringBuilder();
                foreach(string error in errors)
                {
                    sb.AppendLine(error);
                }
                MessageBox.Show(sb.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Components component = new Components();
                component.name = NameBox.Text;
                component.description = DescBox.Text;
                component.cost = costOfComponent;
                component.cat_id = ConfigDB.GetContext().Categories.FirstOrDefault(c => c.name == CatComponentBox.SelectedItem.ToString()).id;
                component.type_id = ConfigDB.GetContext().Types.FirstOrDefault(c => c.name == TypeComponentBox.SelectedItem.ToString()).id;
                component.image = File.ReadAllBytes(imagePath);
                component.availability = AvailBox.SelectedIndex == 0 ? true : false;
                ConfigDB.GetContext().Components.Add(component);
                ConfigDB.GetContext().SaveChanges();
                components = new BindingList<ComponentsView>();
                foreach (var c in ConfigDB.GetContext().Components.ToList())
                {
                    components.Add(new ComponentsView(c));
                }
                CompView.ItemsSource = null;
                CompView.ItemsSource = components;
                MessageBox.Show("Компонент успешно добавлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                

            }
        }

        private void TypeBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            components = new BindingList<ComponentsView>();
            CompView.ItemsSource = null;
            foreach (var c in ConfigDB.GetContext().Components.ToList())
            {
                components.Add(new ComponentsView(c));
            }
            BindingList<ComponentsView> filters = new BindingList<ComponentsView>();
            if (TypeBox.SelectedItem != null)
            {
                filters.Clear();
                foreach (var c in components)
                {
                    if(c.type.Trim() == TypeBox.SelectedItem.ToString().Trim())
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
            if(CatBox.SelectedItem != null)
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
            if(CostBox.SelectedItem != null)
            {
                filters.Clear();
                if (CostBox.SelectedIndex == 0)
                {
                    var temp = components.OrderBy(c => c.cost).ToList();
                    components.Clear();
                    foreach(var t in temp)
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

        private void CatBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            components = new BindingList<ComponentsView>();
            CompView.ItemsSource = null;
            foreach (var c in ConfigDB.GetContext().Components.ToList())
            {
                components.Add(new ComponentsView(c));
            }
            BindingList<ComponentsView> filters = new BindingList<ComponentsView>();
            if (TypeBox.SelectedItem != null)
            {
                filters.Clear();
                foreach (var c in components)
                {
                    if (c.type.Trim() == TypeBox.SelectedItem.ToString().Trim())
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
                filters.Clear();
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
            components.Clear();
            CompView.ItemsSource = null;
            foreach (var c in ConfigDB.GetContext().Components.ToList())
            {
                components.Add(new ComponentsView(c));
            }
            BindingList<ComponentsView> filters = new BindingList<ComponentsView>();
            if (TypeBox.SelectedItem != null)
            {
                filters.Clear();
                foreach (var c in components)
                {
                    if (c.type.Trim() == TypeBox.SelectedItem.ToString().Trim())
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
                filters.Clear();
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var compView = button.DataContext as ComponentsView;
            if(MessageBox.Show("Вы действительно хотите удалить компонент?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var component = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == compView.id);
                if (component != null)
                {
                    ConfigDB.GetContext().Components.Remove(component);
                    ConfigDB.GetContext().SaveChanges();
                    components = new BindingList<ComponentsView>();
                    foreach (var c in ConfigDB.GetContext().Components.ToList())
                    {
                        components.Add(new ComponentsView(c));
                    }
                    CompView.ItemsSource = null;
                    CompView.ItemsSource = components;
                    MessageBox.Show("Компонент был успешно удалён.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var compView = button.DataContext as ComponentsView;
            new FullInformationAboutComponent(compView).ShowDialog();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var compView = button.DataContext as ComponentsView;
            if(new ChangeComponent(compView).ShowDialog() == true)
            {
                components = new BindingList<ComponentsView>();
                foreach (var c in ConfigDB.GetContext().Components.ToList())
                {
                    components.Add(new ComponentsView(c));
                }
                CompView.ItemsSource = null;
                CompView.ItemsSource = components;
            }
        }

       

        private void Window_Closed(object sender, CancelEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
