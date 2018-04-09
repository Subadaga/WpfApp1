using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class KolmogorovSmirnov
    {

        public bool kolmogorov(List<double> randoms, String significancia)
        {
            randoms.Sort();
            List<double> kTable5 = new List<double>() { .975, .842, .708, .624, .565,
                                                .521, .486, .457, .432, .410,
                                                .391, .375, .361, .349, .338,
                                                .328, .318, .309, .301, .294, .154};
            List<double> kTable1 = new List<double>() { .975, .842, .708, .624, .565,
                                                .521, .486, .457, .432, .410,
                                                .391, .375, .361, .349, .338,
                                                .328, .318, .309, .301, .294, .154};
            List<double> kTable2 = new List<double>() { .975, .842, .708, .624, .565,
                                                .521, .486, .457, .432, .410,
                                                .391, .375, .361, .349, .338,
                                                .328, .318, .309, .301, .294, .154};
            List<double> kTable10 = new List<double>() { .975, .842, .708, .624, .565,
                                                .521, .486, .457, .432, .410,
                                                .391, .375, .361, .349, .338,
                                                .328, .318, .309, .301, .294, .154};
            List<double> kTable15 = new List<double>() { .975, .842, .708, .624, .565,
                                                .521, .486, .457, .432, .410,
                                                .391, .375, .361, .349, .338,
                                                .328, .318, .309, .301, .294, .154};
            List<double> kTable20 = new List<double>() { .975, .842, .708, .624, .565,
                                                .521, .486, .457, .432, .410,
                                                .391, .375, .361, .349, .338,
                                                .328, .318, .309, .301, .294, .154};
            List<double> sN = sn(randoms);
            List<double> fX = fx(lambda(promediar(randoms)), randoms);
            List<double> restaSNFX = restaSnFx(fX, sN);
            List<double> restaFXSN = restaFxSn(fX, sN);

            double dMas = restaSNFX.Max();
            double dMenos = restaFXSN.Max();
            double dN = Math.Max(dMas, dMenos);
            double dAlpha = 0;
            switch (significancia)
            {
                case "0.05":
                    if (randoms.Count() <= 20)
                    {
                        dAlpha = kTable5[randoms.Count() - 1];
                        Console.WriteLine(dAlpha);
                    }
                    else
                    {
                        dAlpha = 1.3581 / Math.Sqrt(randoms.Count);
                    }
                    break;
                case "0.01":
                    if (randoms.Count() <= 20)
                    {
                        dAlpha = kTable1[randoms.Count() - 1];
                        Console.WriteLine(dAlpha);
                    }
                    else
                    {
                        dAlpha = 1.62762 / Math.Sqrt(randoms.Count);
                    }
                    break;
                case "0.02":
                    if (randoms.Count() <= 20)
                    {
                        dAlpha = kTable2[randoms.Count() - 1];
                        Console.WriteLine(dAlpha);
                    }
                    else
                    {
                        dAlpha = 1.51743 / Math.Sqrt(randoms.Count);
                    }
                    break;
                case "0.1":
                    if (randoms.Count() <= 20)
                    {
                        dAlpha = kTable10[randoms.Count() - 1];
                        Console.WriteLine(dAlpha);
                    }
                    else
                    {
                        dAlpha = 1.22385 / Math.Sqrt(randoms.Count);
                    }
                    break;
                case "0.15":
                    if (randoms.Count() <= 20)
                    {
                        dAlpha = kTable15[randoms.Count() - 1];
                        Console.WriteLine(dAlpha);
                    }
                    else
                    {
                        dAlpha = 1.13795 / Math.Sqrt(randoms.Count);
                    }
                    break;
                case "0.2":
                    if (randoms.Count() <= 20)
                    {
                        dAlpha = kTable20[randoms.Count() - 1];
                        Console.WriteLine(dAlpha);
                    }
                    else
                    {
                        dAlpha = 1.07275 / Math.Sqrt(randoms.Count);
                    }
                    break;
                default:
                    Console.WriteLine("No es posible utilizar ese nivel de significancia");
                    break;
            }
            Console.WriteLine(dN);
            if (dN > dAlpha)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static double promediar(List<double> randoms)
        {
            double aux = randoms[0];
            for (int i = 1; i < randoms.Count(); i++)
            {
                aux = aux + randoms[i];
            }
            aux = aux / randoms.Count();
            return aux;
        }

        public static double lambda(double promedio)
        {
            double lambda = 1 / promedio;
            return lambda;
        }

        public static List<double> fx(double lambda, List<double> randoms)
        {
            List<double> fx = new List<double>();
            double x;
            for (int i = 1; i < randoms.Count() + 1; i++)
            {
                x = 1 - Math.Exp(-lambda * (randoms[i - 1]));
                fx.Add(x);
            }
            return fx;
        }

        public static List<double> sn(List<double> randoms)
        {
            List<double> sn = new List<double>();
            double aux;
            double total = randoms.Count();
            for (int i = 1; i < randoms.Count() + 1; i++)
            {
                aux = (i / total);
                sn.Add(aux);
            }
            return sn;
        }

        public static List<double> restaSnFx(List<double> fx, List<double> sn)
        {
            List<double> resta = new List<double>();
            double aux;
            for (int i = 0; i < fx.Count(); i++)
            {
                aux = Math.Abs(fx[i] - sn[i]);
                resta.Add(aux);
            }
            return resta;
        }

        public static List<double> restaFxSn(List<double> fx, List<double> sn)
        {
            List<double> resta = new List<double>();
            double aux;
            for (int i = 0; i < fx.Count(); i++)
            {
                if (i == 0)
                {
                    aux = Math.Abs(0 - fx[i]);
                    resta.Add(aux);
                }
                else
                {
                    aux = Math.Abs(sn[i - 1] - fx[i]);
                    resta.Add(aux);
                }
            }
            return resta;
        }
    }
}
