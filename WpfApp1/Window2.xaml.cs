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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private int crithit;
        private int value;
        public Window2()
        {
            InitializeComponent();
            combohits.Items.Add("1 к 4");
            combohits.Items.Add("1 к 6");
            combohits.Items.Add("1 к 8");
            combohits.Items.Add("1 к 10");
            combohits.Items.Add("1 к 12");
        }
        private void hit_Click(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                switch (combohits.SelectedIndex)
                {
                    case 0:
                        value = 4;
                        break;
                    case 1:
                        value = 6;
                        break;
                    case 2:
                        value = 8;
                        break;
                    case 3:
                        value = 10;
                        break;
                    case 4:
                        value = 12;
                        break;
                }
                int damage = 0;
                Random rnd = new Random();
                for (int i = 0; i < 1 + crithit; i++)
                {
                    damage += rnd.Next(1, value+1);
                }
                hit1.Content = ($"Вам выпало число: {damage}");
            }
        }
        private bool check()
        {
            if (crit.IsChecked == false && nocrit.IsChecked == false)
            {
                MessageBox.Show("Выберите тип удара");
                return false;
            }
            else if (combohits.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите кости");
                return false;
            }
            else if(crit.IsChecked == true)
            {
                crithit=1;
            }
            else crithit= 0;
            return true;
        }
    }
}
