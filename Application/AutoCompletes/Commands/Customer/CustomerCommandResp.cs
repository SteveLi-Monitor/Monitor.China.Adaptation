using System.Collections.Generic;

namespace Application.AutoCompletes.Commands.Customer
{
    public class CustomerCommandResp
    {
        public IEnumerable<Customer> Customers { get; set; }

        public class Customer
        {
            public string Id { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }
        }
    }
}
