using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MerchantGuideToTheGalaxy.Processador
{
    /// <summary>
    /// Classe responsável por realizar manipulações nos arquivos txt (entrada e saida)
    /// </summary>
    public class ProcessadorArquivo
    {
        private const string _caminhoArquivo = "C:\\entrada.txt";
        public List<string> perguntas = new List<string>();
        public Dictionary<string, string> valoresIniciaisEntrada = new Dictionary<string, string>();
        public List<string> frasesMetal = new List<string>();

        /// <summary>
        /// Método responsável por abrir o arquivo e iniciar o processamento das linhas
        /// </summary>
        public void AbrirArquivo()
        {
            try
            {
                if(!File.Exists(_caminhoArquivo))
                {
                    Console.Error.WriteLine("O programa não encontrou o arquivo de entrada. O arquivo deve estar em: c:\\entrada.txt");
                    Console.Read();
                }

                using (StreamReader sr = File.OpenText(_caminhoArquivo))
                {
                    string linha = string.Empty;

                    if (sr.ToString() == null)
                    {
                        Console.Error.WriteLine("O arquivo está vazio, verifique o arquivo.");
                        Console.Read();
                    }

                    while ((linha = sr.ReadLine()) != null)
                    {
                        ProcessarLinha(linha.ToUpper());
                    }
                    sr.Close();
                }
            }
            catch (IOException erro)
            {
                Console.Error.WriteLine(erro.Message);
            }
        }

        /// <summary>
        /// Realiza o processamento da linha. Verifica se a linha é uma declaração de variável, pergunta ou definição do valor do crédito
        /// </summary>
        /// <param name="linha"></param>
        private void ProcessarLinha(string linha)
        {
            var palavras = linha.Split(' ');

            if (linha.EndsWith("?"))
            {
                perguntas.Add(linha);
            }
            else if (palavras[1] == "IS")//se for uma definição de valores. ex: glob is I
            {
                ValidarValorRomano(palavras[2]);
                valoresIniciaisEntrada.Add(palavras[0], palavras[2]);
            }
            else if (linha.EndsWith("CREDITS"))
            {
                frasesMetal.Add(linha);
            }
        }

        /// <summary>
        /// Valida se os valores passados são numeros romanos
        /// </summary>
        /// <param name="valorRomano"></param>
        private void ValidarValorRomano(string valorRomano)
        {
            InterpretadorValores valoresValidos = new InterpretadorValores();
            if (!valoresValidos.valoresRomanosValidos.Contains(valorRomano))
            {
                Console.Error.WriteLine("Os valores definidos não são válidos. Os valores válidos são: I,V,X,L,C,D,M. Verifique o arquivo de entrada.");
                Console.Read();
            }
        }
    }
}
