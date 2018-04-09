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
    /// Interaction logic for CongruencialMixto.xaml
    /// </summary>
    public partial class CongruencialMixto : Window
    {
        public CongruencialMixto()
        {
            InitializeComponent();

            this.ResizeMode = ResizeMode.NoResize;
        }


        public void generarNumeros()
        {
            KolmogorovSmirnov kolmogorovSmirnov = new KolmogorovSmirnov();

            if (IsNumeric(SemillaInput.Text) || IsNumeric(MultiplicadorInput.Text) || IsNumeric(IncrementoInput.Text) || IsNumeric(ModuloInput.Text) || IsNumeric(IteracionesInput.Text))
            {
                
                int semilla = Int32.Parse(SemillaInput.Text);
                int multiplicador = Int32.Parse(MultiplicadorInput.Text);
                int incremento = Int32.Parse(IncrementoInput.Text);
                int iteraciones = Int32.Parse(IteracionesInput.Text);
                int modulo = Int32.Parse(ModuloInput.Text);
                int semillaTemporal = 0;
                double resultado = 0.0;

                if (validacionHD(multiplicador, incremento, modulo))
                {
                    List<IteracionCongruencialMixta> iteracionesLista = new List<IteracionCongruencialMixta>();
                    List<double> numAl = new List<double>();

                    for (int i = 0; i < iteraciones; i++)
                    {
                        semillaTemporal = semilla;
                        semilla = (multiplicador * semilla + incremento) % modulo;
                        resultado = (double)semilla / modulo;

                        iteracionesLista.Add(new IteracionCongruencialMixta(semillaTemporal, semilla, resultado, multiplicador, incremento, modulo));
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


            }
            else
            {
                MessageBox.Show("Los valores ingresados no son numéricos");
            }
        }

        private void RegresarButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void GenerarSecuencia_Click(object sender, RoutedEventArgs e)
        {
            generarNumeros();
        }

        public bool IsNumeric(string input)
        {
            return int.TryParse(input, out int test);
        }

        private static bool validacionHD(int a, int c, int m)
        {
            int mII = m;
            int cI, mI;
            int aux = 0;
            int aux2 = 0;
            if (m > c)
            {
                aux = c;
                c = m;
                m = aux;
            }
            cI = c;
            mI = m;

            do
            {
                aux2 = m;
                m = c % m;
                if (m > aux)
                {
                    aux = m;
                }
                c = aux2;
            } while (m != 0);
            Console.WriteLine("El M.C.D. entre " + cI + " y " + mI + " es: " + aux2);
            if (aux2 == 1)
            {
                Console.WriteLine("Cumple con el primer criterio de Hull-Dobell");
                int maxDivisorM = maxDivisor(mII);
                Console.WriteLine("Máximo divisor de m es: " + maxDivisorM);
                int segundoC = (a - 1) % maxDivisorM;
                Console.WriteLine("a ≡ 1modq: " + segundoC);
                if (segundoC == 0)
                {
                    Console.WriteLine("Cumple con el segundo criterio de Hull-Dobell");
                    int tercerC = (a - 1) % 4;
                    Console.WriteLine("a ≡ 1mod4: " + tercerC);
                    if (tercerC == 0)
                    {
                        Console.WriteLine("Cumple con el tercer criterio de Hull-Dobell");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("No cumple con el tercer criterio de Hull - Dobell");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("No cumple con el segundo criterio de Hull - Dobell");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("No cumple con el primer criterio de Hull - Dobell");
                return false;
            }
        }
        public static int maxDivisor(int num)
        {
            List<int> listaFactores = new List<int>();
            if (num == 1)
            {
                listaFactores.Add(num);
                return 1;
            }
            while (num > 1)
            {
                int divisor = 2;
                while (num % divisor != 0)
                {
                    divisor++;
                }
                num = num / divisor;
                listaFactores.Add(divisor);
            }
            return listaFactores.Max();
        }
    }

    public class IteracionCongruencialMixta
    {
        public IteracionCongruencialMixta(int _Generador, int _NoXi, double _NoAleatorio, int multiplicador, int incremento, int modulo)
        {
            Generador = "X = [" + multiplicador + "(" + _Generador + ") + " + incremento + "]mod" + modulo;
            NoXi = _NoXi.ToString();
            NoAleatorio = _NoAleatorio.ToString();

        }

        public String Generador { get; set; }
        public String NoXi { get; set; }
        public String NoAleatorio { get; set; }

    }
}
