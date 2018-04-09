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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

       
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void CuadradosMedios_Click(object sender, RoutedEventArgs e)
        {
            CuadradosMedios cuadradosMedios = new WpfApp1.CuadradosMedios();
            cuadradosMedios.Show();
            this.Close();
        }

        private void MetodoCongruencial_Click(object sender, RoutedEventArgs e)
        {
            MetodoCongruencial metodoCongruencial = new WpfApp1.MetodoCongruencial();
            metodoCongruencial.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CongruencialMixto congruencialMixto = new CongruencialMixto();
            congruencialMixto.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GeneradorMultiplicativo generadorMultiplicativo = new GeneradorMultiplicativo();
            generadorMultiplicativo.Show();
            this.Close();
        }
    }
}
