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
    /// Interaction logic for GeneradorMultiplicativo.xaml
    /// </summary>
    public partial class GeneradorMultiplicativo : Window
    {
        public GeneradorMultiplicativo()
        {
            InitializeComponent();
        }

        public void generarNumeros()
        {
            KolmogorovSmirnov kolmogorovSmirnov = new KolmogorovSmirnov();

            if (IsNumeric(SemillaInput.Text) || IsNumeric(MultiplicadorInput.Text) || IsNumeric(ModuloInput.Text) || IsNumeric(IteracionesInput.Text))
            {
                int semilla = Int32.Parse(SemillaInput.Text);
                int multiplicador = Int32.Parse(MultiplicadorInput.Text);
                int iteraciones = Int32.Parse(IteracionesInput.Text);
                int modulo = Int32.Parse(ModuloInput.Text);
                int semillaTemporal = 0;
                double resultado = 0.0;

                List<IteracionGeneradorMultiplicativo> iteracionesLista = new List<IteracionGeneradorMultiplicativo>();
                List<double> numAl = new List<double>();

                for (int i = 0; i < iteraciones; i++)
                {
                    semillaTemporal = semilla;
                    semilla = (multiplicador * semilla) % modulo;
                    resultado = (double)semilla / modulo;

                    iteracionesLista.Add(new IteracionGeneradorMultiplicativo(semillaTemporal, semilla, resultado, multiplicador, modulo));
                    numAl.Add(resultado);

                }

                DataGridCongruencial.ItemsSource = iteracionesLista;
                bool valoracionKolmogorov = kolmogorovSmirnov.kolmogorov(numAl, boxSignificancia.Text);
                if (kolmogorovSmirnov.kolmogorov(numAl, boxSignificancia.Text))
                {
                    ResultadoKol.Content = "Se acepta la prueba";
                    ResultadoKol.Foreground = Brushes.Green;
                }
                else
                {
                    ResultadoKol.Content = "Se rechaza la prueba";
                    ResultadoKol.Foreground = Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show("Los valores ingresados no son numéricos");
            }
        }

        private void GenerarSecuencia_Click(object sender, RoutedEventArgs e)
        {
            generarNumeros();
        }

        public bool IsNumeric(string input)
        {
            return int.TryParse(input, out int test);
        }

        private void RegresarButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }

    public class IteracionGeneradorMultiplicativo
    {
        public IteracionGeneradorMultiplicativo(int _Generador, int _NoXi, double _NoAleatorio, int multiplicador, int modulo)
        {
            Generador = "X = [" + multiplicador + "(" + _Generador + ")]mod" + modulo;
            NoXi = _NoXi.ToString();
            NoAleatorio = _NoAleatorio.ToString();

        }

        public String Generador { get; set; }
        public String NoXi { get; set; }
        public String NoAleatorio { get; set; }

    }
}
