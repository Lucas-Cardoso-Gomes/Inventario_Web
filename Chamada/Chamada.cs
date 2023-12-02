using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text;

namespace ColetaDados
{
    public class Chamada
    {      
        public static void ColetaBD(string computador)
        {
            string computadorIp = computador;
            int serverPort = 27275;

            using (TcpClient client = new TcpClient(computadorIp, serverPort))
            {
                NetworkStream stream = client.GetStream();

                string autenticacao = "SolicitarInformacoes";

                byte[] data = Encoding.UTF8.GetBytes(autenticacao);

                stream.Write(data, 0, data.Length);

                Console.WriteLine($"Solicitação enviada ao computador: {computadorIp}");

                byte[] buffer = new byte[5120];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string resposta = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Resposta do computador: {computadorIp}");

                var dadosColetados = resposta.Split('\n');

                string Processador = dadosColetados[0].Trim();
                string ProcessadorFabricante = dadosColetados[1].Trim();
                string ProcessadorCore = dadosColetados[2].Trim();
                string ProcessadorThread = dadosColetados[3].Trim();
                string ProcessadorClock = dadosColetados[4].Trim();
                string Ram = dadosColetados[5].Trim();
                string RamTipo = dadosColetados[6].Trim();
                string RamVelocidade = dadosColetados[7].Trim();
                string RamVoltagem = dadosColetados[8].Trim();
                string Usuario = dadosColetados[9].Trim();
                string MAC = dadosColetados[10].Trim();
                string SO = dadosColetados[11].Trim();
                string ArmazenamentoC = dadosColetados[12].Trim();
                string ArmazenamentoCTotal = dadosColetados[13].Trim();
                string ArmazenamentoCLivre = dadosColetados[14].Trim();
                string ArmazenamentoD = dadosColetados[15].Trim();
                string ArmazenamentoDTotal = dadosColetados[16].Trim();
                string ArmazenamentoDLivre = dadosColetados[17].Trim();
                string ConsumoCPU = dadosColetados[18].Trim();
                
                string connectionString = "Server=SHADOWMOVEL;Database=Coletados;User Id=sa;Password=shadow;Trusted_Connection=False;";
                SqlConnection connection = new SqlConnection(connectionString);

                try
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Computadores (IP, Processador, ProcessadorFabricante, ProcessadorCore, ProcessadorThread, ProcessadorClock, Ram, RamTipo, RamVelocidade, RamVoltagem, Usuario, MAC, SO, ArmazenamentoC, ArmazenamentoCTotal, ArmazenamentoCLivre, ArmazenamentoD, ArmazenamentoDTotal, ArmazenamentoDLivre, ConsumoCPU) " +
                                         "VALUES (@IP, @Processador, @ProcessadorFabricante, @ProcessadorCore, @ProcessadorThread, @ProcessadorClock, @Ram, @RamTipo, @RamVelocidade, @RamVoltagem, @Usuario, @MAC, @SO, @ArmazenamentoC, @ArmazenamentoCTotal, @ArmazenamentoCLivre, @ArmazenamentoD, @ArmazenamentoDTotal, @ArmazenamentoDLivre, @ConsumoCPU)";

                    SqlCommand cmd = new SqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@IP", computadorIp);
                    cmd.Parameters.AddWithValue("@Processador", Processador);
                    cmd.Parameters.AddWithValue("@ProcessadorFabricante", ProcessadorFabricante);
                    cmd.Parameters.AddWithValue("@ProcessadorCore", ProcessadorCore);
                    cmd.Parameters.AddWithValue("@ProcessadorThread", ProcessadorThread);
                    cmd.Parameters.AddWithValue("@ProcessadorClock", ProcessadorClock);
                    cmd.Parameters.AddWithValue("@Ram", Ram);
                    cmd.Parameters.AddWithValue("@RamTipo", RamTipo);
                    cmd.Parameters.AddWithValue("@RamVelocidade", RamVelocidade);
                    cmd.Parameters.AddWithValue("@RamVoltagem", RamVoltagem);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@MAC", MAC);
                    cmd.Parameters.AddWithValue("@SO", SO);
                    cmd.Parameters.AddWithValue("@ArmazenamentoC", ArmazenamentoC);
                    cmd.Parameters.AddWithValue("@ArmazenamentoCTotal", ArmazenamentoCTotal);
                    cmd.Parameters.AddWithValue("@ArmazenamentoCLivre", ArmazenamentoCLivre);
                    cmd.Parameters.AddWithValue("@ArmazenamentoD", ArmazenamentoD);
                    cmd.Parameters.AddWithValue("@ArmazenamentoDTotal", ArmazenamentoDTotal);
                    cmd.Parameters.AddWithValue("@ArmazenamentoDLivre", ArmazenamentoDLivre);
                    cmd.Parameters.AddWithValue("@ConsumoCPU", ConsumoCPU);
                    
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Dados inseridos com sucesso na tabela 'Computadores'");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao inserir dados na tabela 'Computadores': " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                stream.Close();
                client.Close();
            }
        }
    }
}