using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace DynamicsTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"
        AuthType=ClientSecret;
        Url=*
        ClientId=*
        ClientSecret=*;
        TenantId=*;
";
            using (var serviceClient = new ServiceClient(connectionString))
            {
                if (serviceClient.IsReady)
                {
                    Console.WriteLine("Bağlantı başarılı.");
                }
                else
                {
                    Console.WriteLine("Bağlantı başarısız.");
                    Console.WriteLine(serviceClient.LastError);
                }
            }
        }
    }
}