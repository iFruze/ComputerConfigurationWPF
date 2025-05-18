using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ComputerConfig
{
    public class ComponentsView
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double cost { get; set; }
        public string availability { get; set; }
        public string cat { get; set; }
        public string type { get; set; }
        public byte[] image { get; set; }
        public ImageSource imgSource { get; set; }
        public ComponentsView(Components component)
        {
            this.id = component.id;
            this.name = component.name;
            this.description = component.description;
            this.cost = component.cost;
            this.availability = component.availability ? "В наличии" : "Не в наличии";
            this.cat = ConfigDB.GetContext().Categories.FirstOrDefault(c => c.id == component.cat_id).name;
            this.type = ConfigDB.GetContext().Types.FirstOrDefault(c => c.id == component.type_id).name;
            this.image = component.image;
            imgSource = ByteArrayToImageSource(image);
        }
        private ImageSource ByteArrayToImageSource(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return null;
            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }
            return image;
        }

    }
}
