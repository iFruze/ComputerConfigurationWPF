using Microsoft.Win32;
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
using System.IO;
using System.Windows.Shapes;

namespace ComputerConfig
{
    /// <summary>
    /// Логика взаимодействия для ChangeComponent.xaml
    /// </summary>
    public partial class ChangeComponent : Window
    {
        ComponentsView componentView;
        ImageSource componentImg = null;
        string imagePath = "";
        public ChangeComponent()
        {
            InitializeComponent();
        }
        public ChangeComponent(ComponentsView c)
        {
            InitializeComponent();
            var cats = ConfigDB.GetContext().Categories.Select(cc => cc.name).ToList();
            var types = ConfigDB.GetContext().Types.Select(cc => cc.name).ToList();
            CatComponentBox.ItemsSource = cats;
            TypeComponentBox.ItemsSource = types;
            componentView = c;
            componentImg = c.imgSource;
            ComponentImage.Source = componentImg;
            NameBox.Text = c.name;
            DescBox.Text = c.description;
            CostText.Text = c.cost.ToString();
            foreach(var cat in CatComponentBox.Items)
            {
                if(cat.ToString() == c.cat)
                {
                    CatComponentBox.SelectedItem = cat;
                    break;
                }
            }
            foreach (var type in TypeComponentBox.Items)
            {
                if (type.ToString() == c.type)
                {
                    TypeComponentBox.SelectedItem = type;
                    break;
                }
            }
            AvailBox.SelectedIndex = c.availability.Trim() == "В наличии".Trim() ? 0 : 1;
        }

        private void AddComponent_Click(object sender, RoutedEventArgs e)
        {
            double costOfComponent = 0;
            List<string> errors = new List<string>();
            if (componentImg == null)
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
                foreach (string error in errors)
                {
                    sb.AppendLine(error);
                }
                MessageBox.Show(sb.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Components component = ConfigDB.GetContext().Components.FirstOrDefault(c => c.id == componentView.id);
                component.name = NameBox.Text;
                component.description = DescBox.Text;
                component.cost = costOfComponent;
                component.cat_id = ConfigDB.GetContext().Categories.FirstOrDefault(c => c.name == CatComponentBox.SelectedItem.ToString()).id;
                component.type_id = ConfigDB.GetContext().Types.FirstOrDefault(c => c.name == TypeComponentBox.SelectedItem.ToString()).id;
                if(!string.IsNullOrEmpty(imagePath))
                {
                    component.image = File.ReadAllBytes(imagePath);
                }
                component.availability = AvailBox.SelectedIndex == 0 ? true : false;
                ConfigDB.GetContext().SaveChanges();
                MessageBox.Show("Компонент успешно изменён!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
        }

        private void AddImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                componentImg = new BitmapImage(new Uri(imagePath));
                ComponentImage.Source = componentImg;
            }
        }
    }
}
