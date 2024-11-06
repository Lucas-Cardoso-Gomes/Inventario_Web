using System;
using System.Threading.Tasks;

namespace ColetaDados
{
    partial class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Coleta de Dados");
                Console.WriteLine("2 - Comandos Remotos");
                Console.WriteLine("3 - Recria Banco de dados");
                Console.WriteLine("4 - Sair");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ColetaDeDados();
                        break;

                    case "2":
                        ExecutarComandosRemotos();
                        break;
           
                    case "3":
                    bool loop = true;
                    while (loop)
                        {
                        Console.Clear();
                        
                        Console.WriteLine("Selecione uma opção:");
                        Console.WriteLine("1 - Limpar Banco de Dados - Computadores");
                        Console.WriteLine("2 - Sair");

                        string limpa = Console.ReadLine();

                        switch (limpa)
                        {
                            case "1":
                            LimpaBD.LimpaBDComputadores();
                            loop = false;
                            break;

                            case "2":
                            Console.WriteLine("Voltando ao menu principal...");
                            loop = false;
                            break;

                            default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                        }
                        }
                        break;
                    case "4":
                        Console.WriteLine("Saindo do programa...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void ColetaDeDados()
        {
            Console.Clear();

            Console.WriteLine("Opção selecionada: Coleta de Dados");

            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1 - Coleta de Dados manual");
            Console.WriteLine("2 - Coleta de Dados automatica (por faixa de IP)");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":

                    Console.WriteLine("Digite o IP/HostName:");

                    string ip = Console.ReadLine();

                    try
                    {
                        Chamada.ColetaBD(ip);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro na coleta de dados: {ex.Message}");
                    }
                    break;

                case "2":
                    Console.WriteLine("Digite qual faixa de IP deseja scanear:");
                    Console.WriteLine("1: 10.0.0.x");
                    Console.WriteLine("2: 10.0.2.x");
                    Console.WriteLine("3: 10.1.1.x");
                    Console.WriteLine("4: 10.1.2.x");
                    Console.WriteLine("5: 10.2.2.x");
                    Console.WriteLine("6: 10.3.3.x");
                    Console.WriteLine("7: 10.4.4.x");
                    Console.WriteLine("8: Todas as faixas acima");

                    string opcao2 = Console.ReadLine();
                    string[] faixas = {};

                    switch (opcao2)
                    {
                        case "1":
                            faixas = new string[] { "10.0.0." };
                            break;
                        case "2":
                            faixas = new string[] { "10.0.2." };
                            break;
                        case "3":
                            faixas = new string[] { "10.1.1." };
                            break;
                        case "4":
                            faixas = new string[] { "10.1.2." };
                            break;
                        case "5":
                            faixas = new string[] { "10.2.2." };
                            break;
                        case "6":
                            faixas = new string[] { "10.3.3." };
                            break;
                        case "7":
                            faixas = new string[] { "10.4.4." };
                            break;
                        case "8":
                            faixas = new string[] { "10.0.0.", "10.0.2.", "10.1.1.", "10.1.2.", "10.2.2.", "10.3.3.", "10.4.4." };
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            return;
                    }

                    Task[] tasks = new Task[faixas.Length];
                    for (int i = 0; i < faixas.Length; i++)
                    {
                        string faixaBase = faixas[i];
                        tasks[i] = Task.Run(() => ColetaDadosPorFaixa(faixaBase));
                    }
                    Task.WaitAll(tasks);
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }

        static void ColetaDadosPorFaixa(string faixaBase)
        {
            Parallel.For(1, 256, i =>
            {
                string ipFaixa = faixaBase + i.ToString();
                try
                {
                    Chamada.ColetaBD(ipFaixa);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro na coleta de dados para {ipFaixa}: {ex.Message}");
                }
            });
        }

        static void ExecutarComandosRemotos()
        {
            Console.Clear();

            Console.WriteLine("Opção selecionada: Comandos Remotos");

            Console.WriteLine("Digite o IP da Máquina:");
            string ip = Console.ReadLine();

            Console.WriteLine("Digite o Comando a ser executado:");
            string comando = Console.ReadLine();

            try
            {
                Comandos.Comando(ip, comando);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar comandos remotos: {ex.Message}");
            }
        }
    }
}