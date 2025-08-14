using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

public class AccountService
{
    private readonly ServiceClient _serviceClient;

    public AccountService(ServiceClient serviceClient)
    {
        _serviceClient = serviceClient;
    }

    public List<AccountInfo> GetAccounts()
    {
        var query = new QueryExpression("account")
        {
            ColumnSet = new ColumnSet("name", "telephone1", "address1_city", "primarycontactid"),
        };

        var contactLink = query.AddLink(
            "contact",
            "primarycontactid",
            "contactid",
            JoinOperator.LeftOuter
        );

        contactLink.Columns = new ColumnSet("emailaddress1");
        contactLink.EntityAlias = "pc";

        var result = _serviceClient.RetrieveMultiple(query);

        var accounts = new List<AccountInfo>();

        foreach (var entity in result.Entities)
        {
            var account = new AccountInfo
            {
                Name = entity.GetAttributeValue<string>("name"),
                Phone = entity.GetAttributeValue<string>("telephone1"),
                City = entity.GetAttributeValue<string>("address1_city"),
                PrimaryContactName = entity.GetAttributeValue<EntityReference>("primarycontactid")?.Name,
                Email = entity.Contains("pc.emailaddress1")
                    ? (string)entity.GetAttributeValue<AliasedValue>("pc.emailaddress1").Value
                    : ""
            };

            accounts.Add(account);
        }

        return accounts;
    }
}

public class AccountInfo
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string PrimaryContactName { get; set; }
    public string Email { get; set; }
}
