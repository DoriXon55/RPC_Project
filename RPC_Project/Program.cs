// See https://aka.ms/new-console-template for more information


namespace RPC_Project;

internal class Program
{

    static void Main(string[] args)
    {
        RemoteRpcExample.RunExample();
        RpcWmiExample.RunExampleWci();
    }
}