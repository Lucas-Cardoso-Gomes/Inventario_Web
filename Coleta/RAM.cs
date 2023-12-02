using System.Management;

namespace coleta
{
    public class RAM
    {
        public static string GetRamInfo()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
            {
                var result = searcher.Get().Cast<ManagementBaseObject>().FirstOrDefault();
                if (result != null)
                {
                    ulong ramSize = Convert.ToUInt64(result["TotalPhysicalMemory"]);
                    string ramSizeMB = $"{ramSize / (1024 * 1024)} MB";

                    string ramType = GetRamType();
                    string ramSpeed = GetRamSpeed();
                    string ramVoltage = GetRamVoltage();

                    return $"{ramSizeMB}\n{ramType}\n{ramSpeed}\n{ramVoltage}";
                }
            }
            return "Informação de RAM não disponível";
        }

        private static string GetRamType()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
            {
                var result = searcher.Get().Cast<ManagementBaseObject>().FirstOrDefault();
                if (result != null)
                {
                    int memoryType = Convert.ToInt32(result["MemoryType"]);

                    switch (memoryType)
                    {
                        case 0:
                            return "Tipo de RAM não disponível";
                        case 21:
                            return "21 (DDR2)";
                        case 22:
                            return "22 (DDR2)";
                        case 24:
                            return "24 (DDR3L)";
                        case 25:
                            return "25 (DDR3)";
                        case 26:
                            return "26 (DDR4)";
                        case 29:
                            return "29 (LPDDR3)";
                        case 30:
                            return "30 (LPDDR4)";
                        default:
                            return $"{memoryType} Tipo desconhecido";
                    }
                }
                return "Tipo de RAM não disponível";
            }
        }

        private static string GetRamSpeed()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
            {
                var result = searcher.Get().Cast<ManagementBaseObject>().FirstOrDefault();
                if (result != null)
                {
                    uint ramSpeed = Convert.ToUInt32(result["ConfiguredClockSpeed"]);
                    return $"{ramSpeed} MHz";
                }
            }
            return "Velocidade de RAM não disponível";
        }

        private static string GetRamVoltage()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
            {
                var result = searcher.Get().Cast<ManagementBaseObject>().FirstOrDefault();
                if (result != null)
                {
                    uint ramVoltage = Convert.ToUInt32(result["ConfiguredVoltage"]);
                    return $"{ramVoltage} Volts";
                }
            }
            return "Voltagem de RAM não disponível";
        }
    }
}
