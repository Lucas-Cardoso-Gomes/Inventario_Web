using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Models;
using System.Data.SqlClient;

namespace MeuProjeto.Controllers
{
    public class ComputadoresController : Controller
    {
        private readonly string _connectionString;

        public ComputadoresController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            List<Computador> computadores = new List<Computador>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT MAC, IP, Usuario, Hostname, Fabricante, Processador, ProcessadorFabricante, ProcessadorCore, ProcessadorThread, ProcessadorClock, Ram, RamTipo, RamVelocidade, RamVoltagem, ArmazenamentoC, ArmazenamentoCTotal, ArmazenamentoCLivre, ArmazenamentoD, ArmazenamentoDTotal, ArmazenamentoDLivre, ConsumoCPU, SO, DataColeta FROM Computadores";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Computador computador = new Computador
                            {
                                MAC = reader["MAC"].ToString(),
                                IP = reader["IP"].ToString(),
                                Usuario = reader["Usuario"].ToString(),
                                Hostname = reader["Hostname"].ToString(),
                                Fabricante = reader["Fabricante"].ToString(),
                                Processador = reader["Processador"].ToString(),
                                ProcessadorFabricante = reader["ProcessadorFabricante"].ToString(),
                                ProcessadorCore = reader["ProcessadorCore"].ToString(),
                                ProcessadorThread = reader["ProcessadorThread"].ToString(),
                                ProcessadorClock = reader["ProcessadorClock"].ToString(),
                                Ram = reader["Ram"].ToString(),
                                RamTipo = reader["RamTipo"].ToString(),
                                RamVelocidade = reader["RamVelocidade"].ToString(),
                                RamVoltagem = reader["RamVoltagem"].ToString(),
                                ArmazenamentoC = reader["ArmazenamentoC"].ToString(),
                                ArmazenamentoCTotal = reader["ArmazenamentoCTotal"].ToString(),
                                ArmazenamentoCLivre = reader["ArmazenamentoCLivre"].ToString(),
                                ArmazenamentoD = reader["ArmazenamentoD"].ToString(),
                                ArmazenamentoDTotal = reader["ArmazenamentoDTotal"].ToString(),
                                ArmazenamentoDLivre = reader["ArmazenamentoDLivre"].ToString(),
                                ConsumoCPU = reader["ConsumoCPU"].ToString(),
                                SO = reader["SO"].ToString(),
                                DataColeta = Convert.ToDateTime(reader["DataColeta"])
                            };
                            computadores.Add(computador);
                        }
                    }
                }
            }

            return View(computadores);
        }
    }
}
