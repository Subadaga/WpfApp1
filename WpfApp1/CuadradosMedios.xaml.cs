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
    /// Interaction logic for CuadradosMedios.xaml
    /// </summary>
    public partial class CuadradosMedios : Window
    {
        public CuadradosMedios()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }

        public void generarNumeros()
        {
            KolmogorovSmirnov kolmogorovSmirnov = new KolmogorovSmirnov();

            if (IsNumeric(SemillaInput.Text) || IsNumeric(IteracionesInput.Text))
            {
                int semilla = Int32.Parse(SemillaInput.Text);
                int iteraciones = Int32.Parse(IteracionesInput.Text);
                int semillaTemporal = 0;
                double resultado = 0.0;
                int resultante = 0;
                

                List<Iteracion> iteracionesLista = new List<Iteracion>();

                List<double> numAl = new List<double>();
                char[] numString = new char[8];
                for (int i = 0; i < iteraciones; i++)
                {
                    semillaTemporal = semilla;
                    semilla = semilla * semilla;
                    resultante = semilla;
                    numString = semilla.ToString().ToCharArray();
                    semilla = getNum(numString);
                    resultado = (double)semilla / 10000;
                    iteracionesLista.Add(new Iteracion(semillaTemporal, resultante, semilla, resultado));
                    numAl.Add(resultado);
                }

                DataGridGenerador.ItemsSource = iteracionesLista;
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

        public bool IsNumeric(string input)
        {
            return int.TryParse(input, out int test);
        }

        private static int getNum(char[] xs)
        {
            int x = 0;
            switch (xs.Length)
            {
                case 3:
                    x = Int32.Parse(new string(xs));
                    break;
                case 4:
                    x = Int32.Parse(new string(xs));
                    break;
                case 5:
                    Array.Resize(ref xs, xs.Length - 1);
                    x = Int32.Parse(new string(xs));
                    break;
                case 6:
                    xs = xs.Skip(1).ToArray();
                    Array.Resize(ref xs, xs.Length - 1);
                    x = Int32.Parse(new string(xs));
                    break;
                case 7:
                    xs = xs.Skip(1).ToArray();
                    Array.Resize(ref xs, xs.Length - 1);
                    Array.Resize(ref xs, xs.Length - 1);
                    x = Int32.Parse(new string(xs));
                    break;
                case 8:
                    xs = xs.Skip(1).ToArray();
                    xs = xs.Skip(1).ToArray();
                    Array.Resize(ref xs, xs.Length - 1);
                    Array.Resize(ref xs, xs.Length - 1);
                    x = Int32.Parse(new string(xs));
                    break;
                default:
                    x = 0;
                    break;
            }
            return x;
        }

            private void SemillaInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void IteracionesInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GenerarButton_Click(object sender, RoutedEventArgs e)
        {
            generarNumeros();
        }

        private void RegresarButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

    public class Iteracion
    {
        public Iteracion(int _Generador, int _Resultante, int _NoXi, double _NoAleatorio)
        {
            Generador = "X = ("+ _Generador + " * "+ _Generador+")";
            Resultante = _Resultante.ToString();
            NoXi = _NoXi.ToString();
            NoAleatorio = _NoAleatorio.ToString();
        }

        public String Generador { get; set; }
        public String Resultante { get; set; }
        public String NoXi { get; set; }
        public String NoAleatorio { get; set; }
    }
}
