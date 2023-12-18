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
using System.Windows.Markup;
using System.Runtime.Remoting.Contexts;
using System.Data.OleDb;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Remoting.Messaging; 
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.CodeDom;
using System.Security.Cryptography.X509Certificates;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string connect = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\pavlo\\Desktop\\dada\\WpfApp1\\Database1.accdb";
        private OleDbConnection connection;
        private string name;
        private string clas;
        private string race;
        private int dext;
        private int str;
        private int wisdom;
        private int intel;
        private int chr;
        private int build;
        private int expir;
        private int mb;
        private int hps;
        private int dcs;
        private int stat;       
        int[] bob = new int[5];
        public int value;
        SolidColorBrush br=new SolidColorBrush(Colors.Beige);
        public MainWindow()
        {
            InitializeComponent();
            connection = new OleDbConnection(connect);
            connection.Open();
            rasses();
            classes();
            hp.IsReadOnly = true;
            dc.IsReadOnly = true;
            mast.IsReadOnly= true;
            lvl.IsReadOnly = true;
            tema.Items.Add("red");
            tema.Items.Add("lime");
            tema.Items.Add("pink");
            tema.Items.Add("darkcyan");
            tema.Items.Add("white");
            tema.Items.Add("default");

        }
        private void classes()
        {
            OleDbCommand cmd = new OleDbCommand($"SELECT COUNT(*) FROM classes", connection);
            int count = (Int32)cmd.ExecuteScalar() + 1;
            for (int i = 1; i < count; i++)
            {
                cmd = new OleDbCommand($"SELECT class FROM classes where id like '{i}'", connection);
                combo1.Items.Add(cmd.ExecuteScalar().ToString());
            }
        }
        private void rasses()
        {
            OleDbCommand cmd = new OleDbCommand($"SELECT COUNT(*) FROM races", connection);
            int count = (Int32)cmd.ExecuteScalar() + 1;
            for (int i = 1; i < count; i++)
            {
                cmd = new OleDbCommand($"SELECT race FROM races where id like '{i}'", connection);
                combo2.Items.Add(cmd.ExecuteScalar().ToString());
            }
        }
        private void combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand($"SELECT hp FROM classes WHERE class like '{combo1.SelectedValue}'", connection);
            hps = Convert.ToInt32(cmd.ExecuteScalar());
            hp.Text = hps.ToString();
            cmd = new OleDbCommand($"SELECT dc FROM classes WHERE class like '{combo1.SelectedValue}'", connection);
            dcs = Convert.ToInt32(cmd.ExecuteScalar());
            dc.Text = dcs.ToString();
        }
            private void combo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stats();
            Build.Text = build.ToString();
            Charizma.Text = chr.ToString();
            Wisdom.Text = wisdom.ToString();
            Intellect.Text = intel.ToString();
            Strength.Text = str.ToString();
            Dexterity.Text = dext.ToString();
        }
        private void stats()
        {
            OleDbCommand cmd = new OleDbCommand($"SELECT build FROM races WHERE race like '{combo2.SelectedValue}'", connection);
            build = Convert.ToInt32(cmd.ExecuteScalar());
            cmd = new OleDbCommand($"SELECT charzma FROM races WHERE race like '{combo2.SelectedValue}'", connection);
            chr = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd = new OleDbCommand($"SELECT wisdom FROM races WHERE race like '{combo2.SelectedValue}'", connection);
            wisdom = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd = new OleDbCommand($"SELECT intellect FROM races WHERE race like '{combo2.SelectedValue}'", connection);
            intel = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd = new OleDbCommand($"SELECT strenght FROM races WHERE race like '{combo2.SelectedValue}'", connection);
            str = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd = new OleDbCommand($"SELECT dexterity FROM races WHERE race like '{combo2.SelectedValue}'", connection);
            dext = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        }
        private bool check()
        {
            if (text1.Text == "") MessageBox.Show("Введите имя");
            else if (combo1.SelectedIndex == -1) MessageBox.Show("Выберите класс");
            else if (combo2.SelectedIndex == -1) MessageBox.Show("Выберите рассу");
            else if (Convert.ToInt32(points.Content) != 0) MessageBox.Show("Распределите все очки характеристик");
            else if (exp.Text == "") MessageBox.Show("Введите количество опыта");
            else return true;
            return false;
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                CheckBox[] cheks = new CheckBox[] { cd1, cd2, cd3, cs1, cm1, cm2, cm3, cm4, cm5, ci1, ci2, ci3, ci4, ci5, cc1, cc2, cc3, cc4 };                
                int count = 0;
                for (int i = 0; i < cheks.Length; i++)
                {
                    if (cheks[i].IsChecked == true)
                    {
                        bob[count] = i;
                        count++;
                    }
                }
                updated();
                OleDbCommand cmd = new OleDbCommand($"INSERT INTO blank (name,class,race,expirience,build,dexterity,intellect,wisdom,charizma,strength,masterbonus,hp,dc,bonus1,bonus2,bonus3,bonus4,bonus5) VALUES ('{text1.Text}','{combo1.Text}','{combo2.Text}','{exp.Text}','{Build.Text}','{Dexterity.Text}','{Intellect.Text}','{Wisdom.Text}','{Charizma.Text}',{Strength.Text},'{mast.Text}','{hp.Text}','{dc.Text}','{bob[0]}','{bob[1]}','{bob[2]}','{bob[3]}','{bob[4]}')", connection);
                cmd.ExecuteNonQuery();
                bdload();
                MessageBox.Show("Анкета успешно создана");
            }
        }
        private void Dexterity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);          
        }
        private void Strength_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);          
        }
        private void Intellect_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);           
        }
        private void Wisdom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);          
        }
        private void Charizma_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);            
        }
        private void Build_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        private static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 0 && i <= 20;
        }
        private int bigger20(TextBox textbox)
        {
            if (int.Parse(textbox.Text) > 20)
                textbox.Text = "20";
            return int.Parse(textbox.Text);
        }
        private int point()
        {
            int max = 75 + maximum();
            stats2();
            max = max - (bigger20(Dexterity) + bigger20(Strength) + bigger20(Intellect) + bigger20(Wisdom) + bigger20(Charizma) + bigger20(Build));
            if (max >= 0) points.Content = max;
            else { MessageBox.Show($"у вас не хватает {Math.Abs(max)} очков прокачки"); }
            return max;
        }
        private int maximum()
        {
            return (build + chr + wisdom + str + intel + dext);
        }
        private void stats2()
        {
            if (Build.Text == "" || Convert.ToInt32(Build.Text) < build)
                Build.Text = build.ToString();
            if (Charizma.Text == "" || Convert.ToInt32(Charizma.Text) < chr)
                Charizma.Text = chr.ToString();
            if (Wisdom.Text == "" || Convert.ToInt32(Wisdom.Text) < wisdom)
                Wisdom.Text = wisdom.ToString();
            if (Intellect.Text == "" || Convert.ToInt32(Intellect.Text) < intel)
                Intellect.Text = intel.ToString();
            if (Strength.Text == "" || Convert.ToInt32(Strength.Text) < str)
                Strength.Text = str.ToString();
            if (Dexterity.Text == "" || Convert.ToInt32(Dexterity.Text) < dext)
                Dexterity.Text = dext.ToString();
        }
        private void load_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = datagrid1.SelectedItem as DataRowView;
            if (row != null)
            {
                CheckBox[] cheks = new CheckBox[] { cd1, cd2, cd3, cs1, cm1, cm2, cm3, cm4, cm5, ci1, ci2, ci3, ci4, ci5, cc1, cc2, cc3, cc4 };
                for (int i = 0; i < cheks.Length; i++) cheks[i].IsChecked = false;
                name = row.Row.ItemArray[1].ToString();
                clas = row.Row.ItemArray[2].ToString();
                race = row.Row.ItemArray[3].ToString();
                expir = Convert.ToInt32(row.Row.ItemArray[4]);                
                build = Convert.ToInt32(row.Row.ItemArray[5]);
                dext = Convert.ToInt32(row.Row.ItemArray[6]);
                intel = Convert.ToInt32(row.Row.ItemArray[7]);
                wisdom = Convert.ToInt32(row.Row.ItemArray[8]);
                chr = Convert.ToInt32(row.Row.ItemArray[9]);
                str = Convert.ToInt32(row.Row.ItemArray[10]);
                mb = Convert.ToInt32(row.Row.ItemArray[11]);
                hps = Convert.ToInt32(row.Row.ItemArray[12]);
                dcs = Convert.ToInt32(row.Row.ItemArray[13]);                
                loadstats(name, clas, race, expir, build, dext, intel, wisdom, chr, mb, hps, dcs,str);
                stats();
                tab.SelectedIndex = 0;
                TextBox[] texts = new TextBox[] { c1, c2, c3, c4 };
                dopstats(int.Parse(Charizma.Text), texts);
                texts = new TextBox[] { m1, m2, m3, m4, m5 };
                dopstats(int.Parse(Wisdom.Text), texts);
                texts = new TextBox[] { i1, i2, i3, i4, i5 };
                dopstats(int.Parse(Intellect.Text), texts);
                texts = new TextBox[] { s1 };
                dopstats(int.Parse(Strength.Text), texts);
                texts = new TextBox[] { d1, d2, d3 };
                dopstats(int.Parse(Dexterity.Text), texts);                
                for (int i = 0; i < bob.Length; i++)
                {
                    OleDbCommand cmd = new OleDbCommand($"SELECT bonus{i+1} FROM blank WHERE id like '{row.Row.ItemArray[0]}'", connection);
                    bob[i] = Convert.ToInt32(cmd.ExecuteScalar());
                    cheks[bob[i]].IsChecked = true;
                }
                updated();
                MessageBox.Show("Анкета загружена");
            }
            else MessageBox.Show("Выберите анкету");
        }
        private void loadstats(string name, string clas, string race, int expir, int build, int dext, int intel, int wisdom, int chr, int mp, int hps, int dcs,int str)
        {
            text1.Text = name;
            combo1.Text = clas;
            combo2.Text = race;
            exp.Text = expir.ToString();
            Build.Text = build.ToString();
            Dexterity.Text = dext.ToString();
            Intellect.Text = intel.ToString();
            Wisdom.Text = wisdom.ToString();
            Charizma.Text = chr.ToString();
            mast.Text = mp.ToString();
            hp.Text = hps.ToString();
            dc.Text = dcs.ToString();
            Strength.Text = str.ToString();
        }
        private void bdload()
        {
            string conn = $"select * FROM blank";
            DataTable table = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(conn, connection);
            adapter.Fill(table);
            datagrid1.ItemsSource = table.DefaultView;
        }
        private void bd_Click(object sender, RoutedEventArgs e)
        {
            bdload();
        }
        private void toload_Click(object sender, RoutedEventArgs e)
        {
            bdload();
            tab.SelectedIndex = 1;
            MessageBox.Show("Выберите анкету");
        }
        private void dopstats(int value, TextBox[] texts)
        {            
            switch (value)
            {
                case 1:
                case 0:
                    stat = -5;
                    break;
                case int n when (n >= 2 && n <= 3):
                    stat = -4;
                    break;
                case int n when (n >= 4 && n <= 5):
                    stat = -3;
                    break;
                case int n when (n >= 6 && n <= 7):
                    stat = -2;
                    break;
                case int n when (n >= 8 && n <= 9):
                    stat = -1;
                    break;
                case int n when (n >= 10 && n <= 11):
                    stat = 0;
                    break;
                case int n when (n >= 12 && n <= 13):
                    stat = 1;
                    break;
                case int n when (n >= 14 && n <= 15):
                    stat = 2;
                    break;
                case int n when (n >= 16 && n <= 17):
                    stat = 3;
                    break;
                case int n when (n >= 18 && n <= 19):
                    stat = 4;
                    break;
                case 20:
                    stat = 5;
                    break;
            }
            if (texts != null)
            {
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i].IsReadOnly = true;
                    texts[i].Text = stat.ToString();
                }
            }
        }
        private void c(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                c11();
            }
        }
        private void d(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                d11();
            }
        }
        private void s(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                s11();
            }
        }
        private void i(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                i11();
            }
        }
        private void m(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                m11();
            }
        }
        private void b(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                b11();
            }
        }
        private void c11()
        {           
            point();
            TextBox[] texts = new TextBox[] { c1, c2, c3, c4 };
            dopstats(int.Parse(Charizma.Text), texts);
            ChrMod.Content = "";
            if (stat > 0) ChrMod.Content = "+";
            ChrMod.Content += stat.ToString();
        }
        private void d11()
        {           
            point();
            TextBox[] texts = new TextBox[] { d1, d2, d3 };
            dopstats(int.Parse(Dexterity.Text), texts);
            DexMod.Content = "";
            if (stat > 0) DexMod.Content = "+";
            DexMod.Content += stat.ToString();
        }
        private void s11()
        {
            point();
            TextBox[] texts = new TextBox[] { s1 };
            dopstats(int.Parse(Strength.Text), texts);
            StrMod.Content = "";
            if (stat > 0) StrMod.Content = "+";
            StrMod.Content += stat.ToString();
        }
        private void i11()
        {          
            point();
            TextBox[] texts = new TextBox[] { i1, i2, i3, i4, i5 };
            dopstats(int.Parse(Intellect.Text), texts);
            IntMod.Content = "";
            if (stat > 0) IntMod.Content = "+";
            IntMod.Content += stat.ToString();
        }
        private void m11()
        {           
            point();
            TextBox[] texts = new TextBox[] { m1, m2, m3, m4, m5 };
            dopstats(int.Parse(Wisdom.Text), texts);
            WisMod.Content = "";
            if (stat > 0) WisMod.Content = "+";
            WisMod.Content += stat.ToString();
        }
        private void b11()
        {
            point();
            dopstats(int.Parse(Build.Text), null);
            hp.Text = (hps + stat).ToString();
            BldMod.Content = "";
            if (stat > 0) BldMod.Content = "+";
            BldMod.Content += stat.ToString();
        }
        private void level(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) expr();
        }
        private void expr()
        {
            if (int.Parse(exp.Text) >= 355000) exp.Text = "35500";
            OleDbCommand cmd = new OleDbCommand($"SELECT БМ FROM lvl WHERE Опыт > {exp.Text}", connection);
            mast.Text = (cmd.ExecuteScalar().ToString());
            cmd = new OleDbCommand($"SELECT Уровень FROM lvl WHERE Опыт > {exp.Text}", connection);
            lvl.Text = (cmd.ExecuteScalar().ToString());
        }
        private bool checkb(TextBox[] texts, CheckBox[] cheks)
        {
            int count = 0;            
            for (int i = 0; i < texts.Length; i++)
            {
                if (cheks[i].IsChecked == true)
                { 
                    count++;
                }
            }
            if (count == 5)
            {
                for (int i = 0; i < texts.Length; i++)
                {
                    if (cheks[i].IsChecked == true)
                    {
                        OleDbCommand cmd = new OleDbCommand($"SELECT БМ FROM lvl WHERE Опыт > {exp.Text}", connection);
                        texts[i].Text = (Convert.ToInt32(texts[i].Text) + Convert.ToInt32(cmd.ExecuteScalar())).ToString();
                    }
                }
            }
            else MessageBox.Show("Выберите 5 навыков");
            return true;          
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            updated();
        }
        private void updated()
        {
            TextBox[] texts = new TextBox[] { d1, d2, d3, s1, m1, m2, m3, m4, m5, i1, i2, i3, i4, i5, c1, c2, c3, c4 };
            CheckBox[] cheks = new CheckBox[] { cd1, cd2, cd3, cs1, cm1, cm2, cm3, cm4, cm5, ci1, ci2, ci3, ci4, ci5, cc1, cc2, cc3, cc4 };
            c11();
            d11();
            m11();
            s11();
            i11();
            b11();
            expr();
            checkb(texts, cheks);
        }
        private void enterupdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updated();
            }
        }
        private void cube_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.grd1.Background = br;
            window.ShowDialog();          
        }
        private void cubehit_Click(object sender, RoutedEventArgs e)
        {
            Window2 window = new Window2();
            window.grd2.Background = br;
            window.ShowDialog();
        }
        private void tema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tema.SelectedIndex)
            {
                case 0:
                    br = new SolidColorBrush(Colors.Red);
                    grd.Background = br;                    
                    break;
                case 1:
                    br = new SolidColorBrush(Colors.Lime);
                    grd.Background = br;                   
                    break;
                case 2:
                    br = new SolidColorBrush(Colors.Pink);
                    grd.Background = br;                   
                    break;
                case 3:
                    br = new SolidColorBrush(Colors.DarkCyan);
                    grd.Background =br;                   
                    break;
                case 4:
                    br = new SolidColorBrush(Colors.White);
                    grd.Background = br;                    
                    break;
                case 5:
                    br = new SolidColorBrush(Colors.Beige);
                    grd.Background = br;                 
                    break;                   
            }
        }
    }
}