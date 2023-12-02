using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;

namespace coleta
{
    partial class Program
    {
        static void Main()
        {
            Console.Clear();
                        
            try
            {
            int port = 27275;
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"Aguardando solicitações na porta {port}...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                Console.WriteLine($"Conexão recebida do IP: {clientIP}");

                byte[] buffer = new byte[5120];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string autenticacao = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (autenticacao == "SolicitarInformacoes")
                {
                    string processador = Processador.GetProcessorInfo();
                    string ram = RAM.GetRamInfo();
                    string usuario = User.GetUserInfo();
                    string mac = MAC.GetFormattedMacAddress();
                    string sistemaOperacional = OS.GetOSInfo();
                    string armazenamento = Armazenamento.GetStorageInfo();
                    string consumoCPU = Consumo.Uso();

                    string resposta =   $"{processador}\n" + 
                                        $"{ram}\n" +
                                        $"{usuario}\n" +
                                        $"{mac}\n" +
                                        $"{sistemaOperacional}\n" +
                                        $"{armazenamento}\n" +
                                        $"{consumoCPU}";
                                        
                    byte[] responseBytes = Encoding.UTF8.GetBytes(resposta);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                    
                    Console.WriteLine($"Informações enviadas para o IP: {clientIP}");

                    client.Close();
                }
                else
                {
                    string resposta = "Código de Autenticação Incorreto!";

                    byte[] responseBytes = Encoding.UTF8.GetBytes(resposta);
                    stream.Write(responseBytes, 0, responseBytes.Length);

                    Console.WriteLine($"Falha no codigo de autenticação para o IP: {clientIP}");

                    client.Close();
                }
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir dados na tabela 'Computadores': " + ex.Message);
            }
            Console.WriteLine ("Fora do Loop, sistema finalizando operações...");
        }
    }
}