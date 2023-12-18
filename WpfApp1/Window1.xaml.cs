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
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public int value;
        public Window1()
        {
            InitializeComponent();
        }
        private void bad_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            cube3.Content = "";
            for (int i = 0; i < 10; i++)
            {
                cube1.Content = rnd.Next(1, 21).ToString();
                cube2.Content = rnd.Next(1, 21).ToString();
            }
            if (Convert.ToInt32(cube1.Content) < Convert.ToInt32(cube2.Content)) value = Convert.ToInt32(cube1.Content);
            else value = Convert.ToInt32(cube2.Content);
            count.Content = value.ToString();
        }

        private void defaultt_Click(object sender, RoutedEventArgs e)
        {
            cube1.Content = "";
            cube2.Content = "";
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                cube3.Content = rnd.Next(1, 21).ToString();
                value = Convert.ToInt32(cube3.Content);
            }
            count.Content = value.ToString();
        }

        private void good_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            cube3.Content = "";
            for (int i = 0; i < 10; i++)
            {
                cube1.Content = rnd.Next(1, 21).ToString();
                cube2.Content = rnd.Next(1, 21).ToString();
            }
            if (Convert.ToInt32(cube1.Content) > Convert.ToInt32(cube2.Content)) value = Convert.ToInt32(cube1.Content);
            else value = Convert.ToInt32(cube2.Content);
            count.Content = value.ToString();
        }
    }
}