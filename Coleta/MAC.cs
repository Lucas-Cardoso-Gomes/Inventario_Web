using System.Net.NetworkInformation;

namespace coleta
{
    public class MAC
    {
        public static string GetFormattedMacAddress()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Ethernet)
                    continue;

                PhysicalAddress macAddress = networkInterface.GetPhysicalAddress();
                if (macAddress != null && macAddress.ToString() != "")
                    return FormatMacAddress(macAddress.GetAddressBytes());
            }
            return "Endereço MAC não encontrado";
        }

        private static string FormatMacAddress(byte[] macBytes)
        {
            return string.Join(":", macBytes.Select(b => b.ToString("X2")));
        }
    }
}