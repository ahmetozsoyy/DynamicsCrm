using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace DynamicsCrm
{
    public class ContactService
    {
        private readonly ServiceClient _serviceClient;

        public ContactService(ServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }


        public List<ContactInfo> GetContacts()
        {
            var query = new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet("fullname", "emailaddress1", "parentcustomerid", "telephone1")
            };

            var result = _serviceClient.RetrieveMultiple(query);
            var contacts = new List<ContactInfo>();

            foreach(var entity in result.Entities)
            {
                var contact = new ContactInfo
                {
                    FullName = entity.GetAttributeValue<string>("fullname"),
                    Email = entity.GetAttributeValue<string>("emailaddress1"),
                    ParentCustomerId = entity.GetAttributeValue<EntityReference>("parentcustomerid")?.Name,
                    Phone = entity.GetAttributeValue<string>("telephone1")
                };
                contacts.Add(contact);
            }
            return contacts;
        }
    }
    public class ContactInfo
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ParentCustomerId { get; set; }
        public string Phone { get; set; }

    }

}
