using DynamicsCrm;
using Microsoft.PowerPlatform.Dataverse.Client;
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
                if (!serviceClient.IsReady)
                {
                    Console.WriteLine("Bağlantı başarısız.");
                    Console.WriteLine("Hata: " + serviceClient.LastError);
                    Console.WriteLine("İç hata: " + serviceClient.LastException?.Message);
                    return;
                }

                Console.WriteLine("Bağlantı başarılı!");

                var accountService = new AccountService(serviceClient);
                var accounts = accountService.GetAccounts();
                Console.WriteLine("ACCOUNTS");
                foreach (var acc in accounts)
                {
                    
                    Console.WriteLine($"Name: {acc.Name}");
                    Console.WriteLine($"Phone: {acc.Phone}");
                    Console.WriteLine($"City: {acc.City}");
                    Console.WriteLine($"Primary Contact: {acc.PrimaryContactName}");
                    Console.WriteLine($"Email: {acc.Email}");
                    Console.WriteLine(new string('-', 30));
                }
                var contactService = new ContactService(serviceClient);
                var contacts = contactService.GetContacts();
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("CONTACTS");
                foreach (var acs in contacts)
                {
                    
                    Console.WriteLine($"FullName: {acs.FullName}");
                    Console.WriteLine($"Phone: {acs.Phone}");
                    Console.WriteLine($"Primary Contact: {acs.ParentCustomerId}");
                    Console.WriteLine($"Email: {acs.Email}");
                    Console.WriteLine(new string('-', 30));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata oluştu: " + ex.Message);
        }
    }
}
