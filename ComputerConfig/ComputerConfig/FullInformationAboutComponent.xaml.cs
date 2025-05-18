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
    /// Логика взаимодействия для FullInformationAboutComponent.xaml
    /// </summary>
    public partial class FullInformationAboutComponent : Window
    {
        public FullInformationAboutComponent(ComponentsView component)
        {
            InitializeComponent();
            NameBox.Text = component.name;
            DescBox.Text = component.description;
            AvailBox.Text = component.availability;
            CostText.Text = component.cost.ToString();
            CatComponentBox.Text = component.cat;
            TypeComponentBox.Text = component.type;
            ComponentImage.Source = component.imgSource;
        }
    }
}
