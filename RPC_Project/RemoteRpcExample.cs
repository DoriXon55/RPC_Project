using System;
using System.Management;

namespace RPC_Project;

class RemoteRpcExample
{
    public static void RunExample()
    {
        string remoteHost = "localhost";
        string username = "doria";
        string password = "2115";

        ConnectionOptions options = new ConnectionOptions
        {
            //Username = username,
            //Password = password,
            Impersonation = ImpersonationLevel.Impersonate,
            EnablePrivileges = true,
            Authentication = AuthenticationLevel.PacketPrivacy
        };

        ManagementScope scope = new ManagementScope($"\\\\{remoteHost}\\root\\cimv2", options);

        try
        {
            scope.Connect();
            Console.WriteLine("Connected to WMI on remote host: " + remoteHost);

            ManagementClass processClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), null);
            object[] methodArgs = { "notepad.exe", null, null, 0 };

            var result = processClass.InvokeMethod("Create", methodArgs);
            Console.WriteLine((uint)result == 0 ? "Proces uruchomiony." : $"Błąd: kod {result}");
        } catch (Exception ex)
        {
            Console.WriteLine("Błąd RPC/WMI: " + ex.Message);
        }

    }
}
