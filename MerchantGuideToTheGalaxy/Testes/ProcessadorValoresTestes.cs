using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MerchantGuideToTheGalaxy.Processador;

namespace Testes
{
    [TestClass]
    public class ProcessadorValoresTeste
    {
        private Dictionary<string, string> DefinirValoresInicias()
        {
            Dictionary<string, string> valores = new Dictionary<string, string>();
            valores.Add("GLOB", "I");
            valores.Add("PROK", "V");
            return valores;
        }

        [TestMethod]
        public void CalcularRomanosTeste()
        {
            InterpretadorValores interpretador = new InterpretadorValores();
            ProcessadorValores processador = new ProcessadorValores();

            List<string> romanos = new List<string>() { "GLOB", "PROK" };
            Dictionary<string, int> romanosConvertidos = interpretador.ConverterValoresIniciaisNumeros(DefinirValoresInicias());
            Dictionary<int, int[]> romanosSubtraiveis = interpretador.PreencherRomanosSubtraidos();

            double esperado = 4;
            double resultado = processador.CalcularRomanos(romanos, romanosConvertidos, romanosSubtraiveis);

            Assert.IsNotNull(resultado, "Resultado nao deveria ser nulo");
            Assert.AreEqual(esperado, resultado, "Valores deveriam ser iguais");


        }
    }
}
