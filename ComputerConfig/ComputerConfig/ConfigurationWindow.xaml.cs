﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Win32;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;

namespace ComputerConfig
{
    /// <summary>
    /// Логика взаимодействия для ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : System.Windows.Window
    {
        static Configuration configuration;
        GetConfiguration getConfig;
        double sum = 0;

        public ConfigurationWindow(GetConfiguration getConfig, Configuration config)
        {
            InitializeComponent();
            this.getConfig = getConfig;
            configuration = config;

            ProcBox.Text = configuration.processor == null ? "" : configuration.processor.name;
            MatBox.Text = configuration.plata == null ? "" :configuration.plata.name;
            NakopBox.Text = configuration.nokopitel == null ? "" : configuration.nokopitel.name;
            ColdBox.Text = configuration.cold == null ? "" : configuration.cold.name;
            BlockBox.Text = configuration.blockPitaniua == null ? "" : configuration.blockPitaniua.name;
            AdapterBox.Text = configuration.adapter == null ? "" : configuration.adapter.name;
            KorpBox.Text = configuration.korpus == null ? "" : configuration.korpus.name;
            VideoBox.Text = configuration.videoKarta == null ? "" : configuration.videoKarta.name;

            ProcCost.Text = configuration.processor == null ? "" : configuration.processor.cost.ToString();
            MatCost.Text = configuration.plata == null ? "" : configuration.plata.cost.ToString();
            NakopCost.Text = configuration.nokopitel == null ? "" : configuration.nokopitel.cost.ToString();
            ColdCost.Text = configuration.cold == null ? "" : configuration.cold.cost.ToString();
            BlockCost.Text = configuration.blockPitaniua == null ? "" : configuration.blockPitaniua.cost.ToString();
            AdapterCost.Text = configuration.adapter == null ? "" : configuration.adapter.cost.ToString();
            KorpCost.Text = configuration.korpus == null ? "" : configuration.korpus.cost.ToString();
            VideoCost.Text = configuration.videoKarta == null ? "" : configuration.videoKarta.cost.ToString();

            sum += configuration.processor == null ? 0 : configuration.processor.cost;
            sum += configuration.plata == null ? 0 : configuration.plata.cost;
            sum += configuration.nokopitel == null ? 0 : configuration.nokopitel.cost;
            sum += configuration.cold == null ? 0 : configuration.cold.cost;
            sum += configuration.blockPitaniua == null ? 0 : configuration.blockPitaniua.cost;
            sum += configuration.adapter == null ? 0 : configuration.adapter.cost;
            sum += configuration.korpus == null ? 0 : configuration.korpus.cost;
            sum += configuration.videoKarta == null ? 0 : configuration.videoKarta.cost;
            AllCost.Text = $"{sum:f2}";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            getConfig(configuration);
        }

        private void RaspClick(object sender, RoutedEventArgs e)
        {
            var list = new List<string>();
            if(configuration.processor != null)
            {
                list.Add($"Процессор - {configuration.processor.name}. Цена: {configuration.processor.cost:f2}");
            }
            else
            {
                list.Add($"Процессор не указан");
            }
            if (configuration.plata != null)
            {
                list.Add($"Материнская плата - {configuration.plata.name}. Цена: {configuration.plata.cost:f2}");

            }
            else
            {
                list.Add($"Материнская плата не указана");

            }
            if (configuration.videoKarta != null)
            {
                list.Add($"Видеокарта - {configuration.videoKarta.name}. Цена: {configuration.videoKarta.cost:f2}");

            }
            else
            {
                list.Add($"Видеокарта не указана");

            }
            if (configuration.nokopitel != null)
            {
                list.Add($"Накопитель - {configuration.nokopitel.name}. Цена: {configuration.nokopitel.cost:f2}");

            }
            else
            {
                list.Add($"Накопитель не указан");

            }
            if (configuration.blockPitaniua != null)
            {
                list.Add($"Блок питания - {configuration.blockPitaniua.name}. Цена: {configuration.blockPitaniua.cost:f2}");

            }
            else
            {
                list.Add($"Блок питания не указан");

            }
            if (configuration.cold != null)
            {
                list.Add($"Охлаждение - {configuration.cold.name}. Цена: {configuration.cold.cost:f2}");

            }
            else
            {
                list.Add($"Охлаждение не указано");

            }
            if (configuration.korpus != null)
            {
                list.Add($"Корпус - {configuration.korpus.name}. Цена: {configuration.korpus.cost:f2}");

            }
            else
            {
                list.Add($"Корпус не указан");

            }
            if (configuration.adapter != null)
            {
                list.Add($"Сетевой адаптер - {configuration.adapter.name}. Цена: {configuration.adapter.cost:f2}");

            }
            else
            {
                list.Add($"Сетевой адаптер не указан");

            }
            list.Add($"Общая стоимость - {sum:f2}");
            if(list.Count == 0)
            {
                MessageBox.Show("Лист пустой");
            }
            var saveDialog = new SaveFileDialog
            {
                Title = "Сохранить конфигурацию",
                Filter = "Документ PDF (*.pdf)|*.pdf",
                FileName = "Конфигурация.pdf"
            };
            if (saveDialog.ShowDialog() == true)
            {
                string filePath = saveDialog.FileName;
                try
                {
                    GlobalFontSettings.FontResolver = new FontsRes();
                    PdfDocument pdf = new PdfDocument();
                    pdf.Info.Title = "Конфигурация компонентов";
                    PdfPage page = pdf.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont titleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
                    XFont textFont = new XFont("Arial", 12, XFontStyleEx.Regular);

                    // Заголовок
                    gfx.DrawString("Конфигурация компонентов", titleFont, XBrushes.Black, new XRect(0, 20, page.Width, page.Height), XStringFormats.TopCenter);

                    // Список элементов
                    int yPosition = 50;
                    foreach (string item in list)
                    {
                        gfx.DrawString(item, textFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height), XStringFormats.TopCenter);
                        yPosition += 20; // Смещение строк вниз
                    }

                    pdf.Save(filePath);

                    MessageBox.Show("Конфигурация успешна записана в PDF.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка при записи в PDF", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ProcDel(object sender, RoutedEventArgs e)
        {
            if(configuration.processor != null)
            {
                if(MessageBox.Show("Вы действительно хотите удалить данный компонент из вашей конфигурации?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sum -= configuration.processor.cost;
                    configuration.processor = null;
                    ProcBox.Text = "";
                    ProcCost.Text = "";
                    AllCost.Text = $"{sum:f2}";
                    MessageBox.Show("Компонент успешно удалён из вашей конфигурации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void MatDel(object sender, RoutedEventArgs e)
        {
            if (configuration.plata != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный компонент из вашей конфигурации?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sum -= configuration.plata.cost;
                    configuration.plata = null;
                    MatBox.Text = "";
                    MatCost.Text = "";
                    AllCost.Text = $"{sum:f2}";
                    MessageBox.Show("Компонент успешно удалён из вашей конфигурации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void VideoDel(object sender, RoutedEventArgs e)
        {
            if (configuration.videoKarta != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный компонент из вашей конфигурации?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sum -= configuration.videoKarta.cost;
                    configuration.videoKarta = null;
                    VideoBox.Text = "";
                    VideoCost.Text = "";
                    AllCost.Text = $"{sum:f2}";
                    MessageBox.Show("Компонент успешно удалён из вашей конфигурации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void NakopDel(object sender, RoutedEventArgs e)
        {
            if (configuration.nokopitel != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный компонент из вашей конфигурации?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sum -= configuration.nokopitel.cost;
                    configuration.nokopitel = null;
                    NakopBox.Text = "";
                    NakopCost.Text = "";
                    AllCost.Text = $"{sum:f2}";
                    MessageBox.Show("Компонент успешно удалён из вашей конфигурации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void BlockDel(object sender, RoutedEventArgs e)
        {
            if (configuration.blockPitaniua != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный компонент из вашей конфигурации?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sum -= configuration.blockPitaniua.cost;
                    configuration.blockPitaniua = null;
                    BlockBox.Text = "";
                    BlockCost.Text = "";
                    AllCost.Text = $"{sum:f2}";
                    MessageBox.Show("Компонент успешно удалён из вашей конфигурации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void ColdDel(object sender, RoutedEventArgs e)
        {
            if (configuration.cold != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный компонент из вашей конфигурации?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sum -= configuration.cold.cost;
                    configuration.cold = null;
                    ColdBox.Text = "";
                    ColdCost.Text = "";
                    AllCost.Text = $"{sum:f2}";
                    MessageBox.Show("Компонент успешно удалён из вашей конфигурации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void KorpDel(object sender, RoutedEventArgs e)
        {
            if (configuration.korpus != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный компонент из вашей конфигурации?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sum -= configuration.korpus.cost;
                    configuration.korpus = null;
                    KorpBox.Text = "";
                    KorpCost.Text = "";
                    AllCost.Text = $"{sum:f2}";
                    MessageBox.Show("Компонент успешно удалён из вашей конфигурации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void AdapterDel(object sender, RoutedEventArgs e)
        {
            if (configuration.adapter != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный компонент из вашей конфигурации?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sum -= configuration.adapter.cost;
                    configuration.adapter = null;
                    AdapterBox.Text = "";
                    AdapterCost.Text = "";
                    AllCost.Text = $"{sum:f2}";
                    MessageBox.Show("Компонент успешно удалён из вашей конфигурации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        
    }
}
