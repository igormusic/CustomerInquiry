using Bogus;
using CustomerInquiry.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInquiry.PublishFakes
{
    public class FakerFactory
    {
        public static Faker<Customer> CreateCustomerFaker()
        {
            var customerIds = 100000000;

            var customerFaker = new Faker<Customer>()
            //Optional: Call for objects that have complex initialization
            .CustomInstantiator(f => new Customer())
                .RuleFor(u => u.CustomerID, f => customerIds++)
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.MiddleName, f=> f.Name.FirstName())
                .RuleFor(u=> u.DateOfBirth , f=> f.Date.Past(70, new DateTime(2000,1,1)))
                .RuleFor(u => u.PhoneNumber, f=> f.Phone.PhoneNumber("(###) ###-####"))
                .RuleFor(u => u.EmailAddress, f=> f.Internet.Email() )
                //After all rules are applied finish with the following action
                .FinishWith((f, u) =>
                {
                    //Console.WriteLine("Customer Created! Id={0}", u.CustomerID);
                });

            return customerFaker;

            //var user = testUsers.Generate();
            //return JsonConvert.SerializeObject(user);
        }

       
        
    }
}
