using System;
using Xunit;
using GroceryStoreAPI;
using System.Net.Http;
using System.Web.Http;

namespace GroceryStoreAPI.Tests
{
    public class GroceryStoreAPITest
    {
        [Fact]
        public void CustomerByID()
        {

              // Set up Prerequisites   
                var controller =  CustomerController();
                controller.Request = System.Web.Http.nesw6HttpRequestMessage();
                controller.Configuration = System.Web.Http.newHttpConfiguration();
                // Act on Test  
                var response = controller.Get(1);
                // Assert the result  
                Customer employee;
                Assert.IsTrue(response.TryGetContentValue<Customer>(out employee));
                Assert.AreEqual("Jignesh", employee.Name);
            }
        }
    }
}
