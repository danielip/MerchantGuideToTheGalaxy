using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MerchantGuideToTheGalaxy.Processador;

namespace MerchantGuideToTheGalaxy
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ProcessadorArquivo processador = new ProcessadorArquivo();
                processador.AbrirArquivo();
                ProcessarArquivo(processador);
            }
            catch (Exception erro)
            {
                Console.Error.WriteLine("Ocorreu um erro ao realizar processamento: ", erro.Message);
                Console.Read();
            }
        }

        /// <summary>
        /// Método principal do programa, responsável por realizar o processamento do arquivo
        /// </summary>
        /// <param name="processador"></param>
        private static void ProcessarArquivo(ProcessadorArquivo processador)
        {
            ProcessadorValores conversor = new ProcessadorValores();
            InterpretadorValores interpretadorValor = new InterpretadorValores();
            ProcessadorSaida saida = new ProcessadorSaida();
            Dictionary<string, int> romanosConvertidos = interpretadorValor.ConverterValoresIniciaisNumeros(processador.valoresIniciaisEntrada);
            Dictionary<int, int[]> romanosSubtraidos = interpretadorValor.PreencherRomanosSubtraidos();
            Dictionary<string, double> valoresMetais = interpretadorValor.DefinirValorMetais(processador.frasesMetal, processador.valoresIniciaisEntrada, romanosConvertidos, romanosSubtraidos);
           
            saida.ResponderPerguntas(processador.perguntas, romanosConvertidos, valoresMetais, romanosSubtraidos);
        }
    }
}
