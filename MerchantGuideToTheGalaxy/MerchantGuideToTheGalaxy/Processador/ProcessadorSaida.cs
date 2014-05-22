using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuideToTheGalaxy.Processador
{
    /// <summary>
    /// Classe responsável por responder as perguntas do arquivo texto e mostrar as respostas na tela
    /// </summary>
    public class ProcessadorSaida
    {
        public ProcessadorValores processadorValores = new ProcessadorValores();
        public InterpretadorValores valoresValidos = new InterpretadorValores();
        public Dictionary<string, int> _romanosConvertidos = new Dictionary<string, int>();
        public Dictionary<int, int[]> _romanosSubtraiveis = new Dictionary<int, int[]>();

        /// <summary>
        /// Método responsável por interpretar as perguntas e respondê-las
        /// </summary>
        /// <param name="perguntas"></param>
        public void ResponderPerguntas(List<string> perguntas, Dictionary<string, int> romanos, Dictionary<string, double> valoresMetais, Dictionary<int, int[]> romanosSubtraiveis)
        {
            _romanosConvertidos = romanos;
            _romanosSubtraiveis = romanosSubtraiveis;

            foreach (var item in perguntas)
            {
                var palavras = item.Split(' ');
                if (item.StartsWith("HOW MUCH"))
                {
                    ResponderHowMuch(palavras);
                }
                else if (item.StartsWith("HOW MANY"))
                {
                    ResponderHowMany(palavras, valoresMetais);
                }
                else
                {
                    Console.WriteLine("As perguntas não seguem o padrão. Verifique o arquivo de entrada, perguntas devem começar com how much ou how many");
                    Console.Read();
                }
            }
        }

        /// <summary>
        /// Interpreta a pergunta quando ela inicia com "How Many"
        /// Neste caso, as perguntas tem valores romanos e de metais
        /// </summary>
        /// <param name="palavras"></param>
        private void ResponderHowMany(string[] palavras, Dictionary<string, double> valoresMetais)
        {
            /*
             * Exemplo de frase(palavras com indice): HOW[0] MANY[1] CREDITS[2] IS[3] GLOB[4] PROK[5] SILVER[6] ?[7]
             */

            StringBuilder resposta = new StringBuilder();
            List<string> valores = new List<string>();

            var valorMetal = double.MinValue;
            var nomeMetal = string.Empty;
            for (int i = 0; i < palavras.Length; i++)
            {
                if (i > 3)//Se não forem as palavras HOW MANY IS Credits 
                {
                    //Se não for o ponto de interrogação nem o valor do metal são valores válidos
                    if (i < palavras.Length - 2)
                    {
                        valores.Add(palavras[i]);
                        resposta.AppendFormat(string.Format("{0}{1}", palavras[i], " "));
                    }
                    else if (!palavras[i].EndsWith("?")) //Se for um metal
                    {
                        nomeMetal = palavras[i];
                        valorMetal = valoresMetais.First(v => v.Key == palavras[i]).Value;
                    }
                }
            }

            // A lógica de quando tem um metal na pergunta é diferente de quando tem somente numeros romanos. 
            // Quando há metal, deve-se fazer multiplicação
            var soma = processadorValores.CalcularRomanos(valores, _romanosConvertidos, _romanosSubtraiveis) * valorMetal;

            Console.WriteLine(string.Format("{0}{1}{2}{3}{4}", resposta.ToString(), nomeMetal, " is ", soma, " Credits"));
        }

        /// <summary>
        /// Interpreta a pergunta quando ela se inicia com "How Much".
        /// Esse tipo de pergunta contempla somente valores de numeros romanos
        /// </summary>
        /// <param name="palavras"></param>
        private void ResponderHowMuch(string[] palavras)
        {
            StringBuilder resposta = new StringBuilder();
            List<string> valores = new List<string>();

            for (int i = 0; i < palavras.Length; i++)
            {
                // Se não forem as palavras HOW -MUCH- IS e não for o ponto de interrogação são valores válidos
                if (i > 2 && i < palavras.Length - 1)
                {
                    valores.Add(palavras[i]);
                    resposta.AppendFormat(string.Format("{0}{1}", palavras[i], " "));
                }
            }

            //Quando somente existem numeros romanos na pergunta, a lógica do calculo segue a lógica normal nos numeros romanos
            Console.WriteLine(string.Format("{0}{1}{2}", resposta.ToString(), " is ", processadorValores.CalcularRomanos(valores, _romanosConvertidos, _romanosSubtraiveis)));
        }
    }
}
