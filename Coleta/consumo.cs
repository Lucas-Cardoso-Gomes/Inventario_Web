using System;
using System.Diagnostics;

namespace coleta
{
    public class Consumo
    {
        public static string Uso()
        {
            // Obtém a instância atual do processo
            Process process = Process.GetCurrentProcess();

            // Obtém o contador de uso da CPU
            PerformanceCounter cpuCounter = new(
                "Process",
                "% Processor Time",
                process.ProcessName);

            // Lê o valor atual do contador
            float cpuUsage = cpuCounter.NextValue();

            // Aguarda um momento para coletar outra leitura
            System.Threading.Thread.Sleep(1000);

            // Lê novamente o valor do contador após um intervalo de tempo
            cpuUsage = cpuCounter.NextValue();
            string Consumo = cpuUsage.ToString();

            // Converte o valor float para uma string e retorna
            return Consumo;
        }
    }
}
