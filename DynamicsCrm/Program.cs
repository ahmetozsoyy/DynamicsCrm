using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = @"
AuthType=ClientSecret;
Url=*;
ClientId=*;
ClientSecret=*;
TenantId=*;

";

        try
        {
            using (var serviceClient = new ServiceClient(connectionString))
            {
                if (serviceClient.IsReady)
                {
                    Console.WriteLine("Bağlantı başarılı!");

                    var query = new QueryExpression("account")
                    {
                        ColumnSet = new ColumnSet(true),
                        TopCount = 5
                    };

                    var result = serviceClient.RetrieveMultiple(query);

                    foreach (var entity in result.Entities)
                    {
                        Console.WriteLine($"Account: {entity.GetAttributeValue<string>("name")}");
                    }
                }
                else
                {
                    Console.WriteLine("Bağlantı başarısız.");
                    Console.WriteLine("Hata: " + serviceClient.LastError);
                    Console.WriteLine("İç hata: " + serviceClient.LastException?.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata oluştu: " + ex.Message);
        }
    }
}
