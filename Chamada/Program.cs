namespace ColetaDados
{
    partial class Program
    {
        static void Main()
        {

            /*int tempIP= 100;
            int espera= 7200;*/

            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";

            Console.Clear();
            Console.WriteLine("Executando em: " + mainThread.Name);
            try
            {
                Chamada.ColetaBD("localhost");
                Chamada.ColetaBD("10.1.1.251");
                //Chamada.ColetaBD("10.1.1.249");
                //Chamada.ColetaBD("26.90.143.101");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao coletar dados dos teste - " + "Cod:" + ex.Message);
            }
            /*
            Console.WriteLine("Executando Loop de coletas: Thered 1 a 6!");
            while(true){
            if(tempIP==255)
            {
                tempIP= 100;
                espera= 7200;
                do{
                    Console.WriteLine("Proxima varredura em: " + espera + "Segundos");
                    espera--;
                    Task.Delay(1000).Wait();
                }while(espera!=0);
            }
            Thread thread1 = new Thread(() => UGN(tempIP));
            Thread thread2 = new Thread(() => SBJ(tempIP));
            Thread thread3 = new Thread(() => CUF(tempIP));
            Thread thread4 = new Thread(() => ITJ(tempIP));
            Thread thread5 = new Thread(() => FOZ(tempIP));
            Thread thread6 = new Thread(() => DIO(tempIP));

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();
            thread6.Start();
            Task.Delay(5000).Wait();
            tempIP++;
            }
            */
        }

        public static void UGN(int tempIP)
        {
            string IPUNG= "10.0.0."+tempIP;
            try
            {
                Chamada.ColetaBD(IPUNG);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao coletar dados de: " + IPUNG + "Cod:" + ex.Message);
            }
        }
        public static void SBJ(int tempIP)
        {
            string IPUNG= "10.1.1."+tempIP;
            try
            {
                Chamada.ColetaBD(IPUNG);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao coletar dados de: " + IPUNG + "Cod:" + ex.Message);
            }
        }
        public static void CUF(int tempIP)
        {
            string IPUNG= "10.1.2."+tempIP;
            try
            {
                Chamada.ColetaBD(IPUNG);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao coletar dados de: " + IPUNG + "Cod:" + ex.Message);
            }
        }
        public static void ITJ(int tempIP)
        {
            string IPUNG= "10.2.2."+tempIP;
            try
            {
                Chamada.ColetaBD(IPUNG);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao coletar dados de: " + IPUNG + "Cod:" + ex.Message);
            }
        }
        public static void FOZ(int tempIP)
        {
            string IPUNG= "10.3.3."+tempIP;
            try
            {
                Chamada.ColetaBD(IPUNG);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao coletar dados de: " + IPUNG + "Cod:" + ex.Message);
            }
        }
        public static void DIO(int tempIP)
        {
            string IPUNG= "10.4.4."+tempIP;
            try
            {
                Chamada.ColetaBD(IPUNG);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao coletar dados de: " + IPUNG + "Cod:" + ex.Message);
            }
        }
    }
}
