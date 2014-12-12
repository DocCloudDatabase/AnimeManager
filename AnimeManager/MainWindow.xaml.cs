using AnimeManager.core;
using AnimeManager.Enums;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AnimeManager
{
    
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] collections = new string[]{
            "\\\\MYBOOKLIVE\\Collection\\ANimE",
            "\\\\192.168.1.10\\Anime",
            "\\\\192.168.1.10\\Database",
            "\\\\192.168.1.10\\Fresh Anime"
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        public StackPanel AddMainFolder(String name)
        {
            string escaped = System.Security.SecurityElement.Escape(name);
            Expander container = new Expander();
            ScrollViewer ground = new ScrollViewer();
            StackPanel panel = new StackPanel();
            //Container Config
            //this.RegisterName(escaped, container);
            //container.Name = escaped;
            container.Header = escaped;
            container.Margin = new Thickness(0, 0, 0, 0);
            container.Height = 185;
            container.Width = 436;
            //Panel Config
            panel.Margin = new Thickness(0, 0, 0, 0);
            //Box in the Box
            container.Content = ground;
            
            ground.Content = panel;
            lista.Children.Add(container);
            return panel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string collection in collections)
            {
                string cName = Anime.parseOut(collection, "\\");
                StackPanel panel = AddMainFolder(cName);
                Anime.AnalizeCollection(ref panel, collection);
            }
        }


        
    }
}
