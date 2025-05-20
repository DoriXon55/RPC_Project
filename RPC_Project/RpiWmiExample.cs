using System;
using System.Management;
using System.Security.Principal;

namespace RPC_Project;

class RpcWmiExample
{
    public static void RunExampleWci()
    {
        string fullUser = WindowsIdentity.GetCurrent().Name;
        Console.WriteLine("Zalogowany użytkownik: " + fullUser);

        string userName = Environment.UserName;
        string domainName = Environment.UserDomainName;

        Console.WriteLine("Nazwa użytkownika: " + userName);
        Console.WriteLine("Nazwa domeny: " + domainName);

        string remoteHost = "localhost";
        string user = "doria";
        string password = "2115";

        var options = new ConnectionOptions
        {
            //Username = user,
            //Password = password,
            Impersonation = ImpersonationLevel.Impersonate,
            EnablePrivileges = true,
            Authentication = AuthenticationLevel.PacketPrivacy
        };

        var scope = new ManagementScope($"\\\\{remoteHost}\\root\\cimv2", options);

        try
        {
            scope.Connect();
            Console.WriteLine("Połączono z WMI na zdalnym hoście.");
            var processClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), null);
            string commandLine = "notepad.exe";
            object[] methodArgs = { commandLine, null, null, 0 };

            var result = processClass.InvokeMethod("Create", methodArgs);

            if ((uint)result == 0)
            {
                Console.WriteLine("Zdalnie uruchomiono: " + commandLine);
            }
            else
            {
                Console.WriteLine("Błąd uruchamiania procesu: " + result);
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd połączenia: " + ex.Message);
        }
    }
}
