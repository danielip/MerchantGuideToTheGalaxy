using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuideToTheGalaxy.Processador
{
    /// <summary>
    /// Classe responsável por realizar as conversões de texto para valores  do conteudo recebido no arquivo
    /// </summary>
    public class ProcessadorValores
    {
        public InterpretadorValores valoresValidos = new InterpretadorValores();

        /// <summary>
        /// Método responsável por descrobri o valor do metal
        /// Exemplo:  se a frase for "glob glob Silver is 34 Credits"
        /// Nesta caso, o método irá fazer a divisão dos creditos sobre os valores das palavras
        /// A lógica é metal = credito/soma dos valores. ex: Silver = 34 / glob + glob
        /// </summary>
        /// <param name="valores">Valores inciais da frase (ex: Glob)</param>
        /// <param name="credito">valor do crédito</param>
        /// <returns>Retorna o valor do metal</returns>
        public double RealizarCalculoMetal(List<string> valores, double credito, Dictionary<string, int> romanosConvertidos, Dictionary<int, int[]> romanosSubtraiveis)
        {
            return credito / CalcularRomanos(valores, romanosConvertidos, romanosSubtraiveis);
        }

        /// <summary>
        /// Realiza o cálculo dos numero romanos.
        /// O calculo considera as regras de subtração e adicão disponiveis em: http://en.wikipedia.org/wiki/Roman_numerals
        /// </summary>
        /// <param name="romanos">lista de numeros romanos</param>
        /// <returns>Retorna o valor da soma dos romanos</returns>
        public double CalcularRomanos(List<string> romanos,  Dictionary<string, int> romanosConvertidos, Dictionary<int, int[]> romanosSubtraiveis)
        {
            int valorAnteriorSubtraido = 0;
            int somaAtual = 0;
            foreach (var item in romanos)
            {
                if (romanosConvertidos.ContainsKey(item))
                {
                    var romano = romanosConvertidos.First(r => r.Key == item).Value;
                    if (romanosSubtraiveis.ContainsKey(romano)) //se o romano pode subtrair alguem
                    {
                        var romanoSubtraivel = romanosSubtraiveis.First(r => r.Key == romano);
                        //se o meu anterior tambem puder me subtrair
                        if (romanosSubtraiveis.ContainsValue(new int[valorAnteriorSubtraido]))
                        {
                            somaAtual = romano - valorAnteriorSubtraido;
                        }
                        valorAnteriorSubtraido = romano;
                        somaAtual += romano;
                    }
                    else //se eu nao posso subtrair ninguem
                    {
                        //se o meu anterior tambem puder me subtrair                            
                        if (romanosSubtraiveis.Where(v => v.Value[0] == valorAnteriorSubtraido) != null ||
                            romanosSubtraiveis.Where(v => v.Value[1] == valorAnteriorSubtraido) != null)
                        {
                            somaAtual = romano - valorAnteriorSubtraido;
                        }
                        else
                        {
                            somaAtual += romano;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("I have no idea what you are talking about");
                    Console.Read();
                }
            }
            return somaAtual;
        }      
    }
}
