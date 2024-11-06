using System;
using System.Net.Sockets;
using System.Text;

namespace ColetaDados
{
    public class Comandos
    {
        public static void Comando(string computador, string comando)
        {
            try
            {
                string computadorIp = computador;
                string comandoIp = comando;
                int serverPort = 27275;

                using (TcpClient client = new TcpClient(computadorIp, serverPort))
                using (NetworkStream stream = client.GetStream())
                {
                    string dados = $"RealizarComandos\n{comandoIp}";
                    byte[] data = Encoding.UTF8.GetBytes(dados);

                    stream.Write(data, 0, data.Length);
                    Console.WriteLine($"Solicitação enviada ao computador: {computadorIp}");

                    byte[] buffer = new byte[5120];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string resposta = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine(resposta);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar solicitação de comandos: {ex.Message}");
            }
        }
    }
}
