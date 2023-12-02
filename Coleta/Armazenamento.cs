using System.Management;
using System.Text;

namespace coleta
{
    public class Armazenamento
    {
        public static string GetStorageInfo()
        {
            var ArmazenamentoBuilder = new StringBuilder();
            bool unidadeDEncontrada = false;

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType=3"))
            {
                foreach (var result in searcher.Get())
                {
                    ulong total = Convert.ToUInt64(result["Size"]);
                    ulong livre = Convert.ToUInt64(result["FreeSpace"]);
                    string letra = result["Name"].ToString();

                    if (letra.Equals("D:"))
                    {
                        unidadeDEncontrada = true;
                    }

                    string armazenamento = $"{letra}\n{total / (1024 * 1024 * 1024)} GB\n{livre / (1024 * 1024 * 1024)} GB";
                    ArmazenamentoBuilder.AppendLine(armazenamento);
                }
            }

            // Se a unidade D: não for encontrada, adicione informações fictícias
            if (!unidadeDEncontrada)
            {
                string letraD = "D:";
                string armazenamentoD = $"{letraD}\n0 GB\n0 GB";
                ArmazenamentoBuilder.AppendLine(armazenamentoD);
            }

            if (ArmazenamentoBuilder.Length > 0)
            {
                return ArmazenamentoBuilder.ToString();
            }
            else
            {
                return "Informação de armazenamento não disponível";
            }
        }
    }
}
