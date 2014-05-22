using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuideToTheGalaxy.Processador
{
    public class InterpretadorValores
    {
        //TODO: deveria ser uma constante
        public List<string> valoresRomanosValidos = new List<string>() { "I", "V", "X", "L", "C", "D", "M" };

        /// <summary>
        /// Preenche os valores romanos que podem ser subtraidos.
        /// Indica qual numero cada numero romano pode subtrair
        /// </summary>
        public Dictionary<int, int[]> PreencherRomanosSubtraidos() //TODO: deveria ser uma constante
        {
            Dictionary<int, int[]> romanos = new Dictionary<int, int[]>();
            romanos.Add(1, new int[2] { 5, 10 });
            romanos.Add(10, new int[2] { 50, 100 });
            romanos.Add(100, new int[2] { 50, 100 });
            return romanos;
        }

        /// <summary>
        /// Método responsável por realizar a conversão das palavras inciais(ex: Glob) para números, com base nos algarismos romanos
        /// </summary>
        /// <param name="valores">Retornará a palavra e o valor. Ex: (Glob, 1)</param>
        public Dictionary<string, int> ConverterValoresIniciaisNumeros(Dictionary<string, string> valores)
        {
            Dictionary<string, int> romanosConvertidos = new Dictionary<string, int>();

            foreach (var item in valores)
            {
                switch (item.Value)
                {
                    case "I":
                        romanosConvertidos.Add(item.Key, 1);
                        break;
                    case "V":
                        romanosConvertidos.Add(item.Key, 5);
                        break;
                    case "X":
                        romanosConvertidos.Add(item.Key, 10);
                        break;
                    case "L":
                        romanosConvertidos.Add(item.Key, 50);
                        break;
                    case "C":
                        romanosConvertidos.Add(item.Key, 100);
                        break;
                    case "D":
                        romanosConvertidos.Add(item.Key, 500);
                        break;
                    case "M":
                        romanosConvertidos.Add(item.Key, 1000);
                        break;
                }
            }
            return romanosConvertidos;
        }

        /// <summary>
        /// Método responsável por interpretar a frase que contém os créditos para descobrir o valor dos metais
        /// </summary>
        /// <param name="fraseMetal">frase completa com o valor do credito</param>
        /// <param name="valoresConvertidos">Retorna o metal e seu valor.</param>
        public Dictionary<string, double> DefinirValorMetais(List<string> fraseMetal, Dictionary<string, string> valoresVariaveis, Dictionary<string, int> romanosConvertidos, Dictionary<int, int[]> romanosSubtraiveis)
        {
            Dictionary<string, double> valoresMetais = new Dictionary<string, double>();
            
            var nomeMetal = string.Empty;
            var credito = double.MinValue;
            int indiceUltimoValor = 0;

            foreach (var item in fraseMetal)
            {
                var indiceMetal = 0;
                List<string> valores = new List<string>();
                var frase = item.ToUpper().Split(' ');
                for (int i = 0; i < frase.Length; i++)
                {
                    if (valoresVariaveis.ContainsKey(frase[i])) //se for um valor válido conhecido
                    {
                        valores.Add(frase[i]);
                        indiceUltimoValor = i;//TODO: não é usado
                    }
                    else if (indiceMetal == 0) //se for um metal
                    {
                        nomeMetal = frase[i];
                        indiceMetal = i;
                    }
                }

                credito = double.Parse(frase[indiceMetal + 2]);//sempre será duas posições depois do metal. ex: silver is 34
                ProcessadorValores processador = new ProcessadorValores();
                valoresMetais.Add(nomeMetal, processador.RealizarCalculoMetal(valores, credito, romanosConvertidos, romanosSubtraiveis));
            }
            return valoresMetais;
        }
    }
}
