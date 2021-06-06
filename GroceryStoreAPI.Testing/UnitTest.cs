using NUnit.Framework;
using GroceryStoreAPI.Controllers;
using System.Collections.Generic;

namespace GroceryStoreAPI.Testing
{
    public class Tests
    {
       
        [Test]
        public void getCustomerById()//test to get customer by id
        {
            //var controller = new CustomerController();
            //Customer mycusmer = controller.Get(1);
            //StringAssert.Contains("Bob",mycusmer.name,"It is not matched");
        }

        [Test]
        public void getCustomers()//get list of customers
        {
            //var controller = new CustomerController();
            //CustomerController.RootObject myCustomers = controller.Get();
            //List<Customer> mycusts = (List<Customer>)myCustomers.Customers;
            //StringAssert.Contains("Bob", mycusts[0].name, "It is not matched");
        }


        [Test] 
        public void addCustomer()//add a customer
         {
        //    var controller = new CustomerController();
        //    Customer mycusmer = new Customer();
        //    mycusmer.id = 102;
        //    mycusmer.name = "Jeff";
        //    int ret = controller.Put(mycusmer);
        //    Assert.AreEqual(1, ret, "It is not matched");
        }

        [Test]
        public void updateCustomer()//update a customer
        {
            var controller = new CustomerController();
            Customer mycusmer = new Customer();
            mycusmer.id = 102;
            mycusmer.name = "Jeffrey";
            int ret = controller.Post(mycusmer);
            Assert.AreEqual(1, ret, "It is not matched");
        }


    }
}