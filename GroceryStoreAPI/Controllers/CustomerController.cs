using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryStoreAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //private string jsonFile = @"C:\Users\jeff chen\OneDrive\Desktop\interview\GroceryStoreAPI\database.json";
        public string jsonFile
        {
            get
            {
                string dir = System.Environment.CurrentDirectory;
                string path=dir.Substring(0, dir.IndexOf("GroceryStoreAPI"));
                return path+"\\GroceryStoreAPI\\database.json";

            }
        }

            // public string jsonFile= System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Example.xml");

            // GET: api/<CustomerController>
        public class RootObject
        {
            public Object Customers { get; set; }
        }
        [HttpGet]
        public RootObject Get()
        {
            var json = System.IO.File.ReadAllText(jsonFile);
            List<Customer> customers = new List<Customer>();
            RootObject ro = new RootObject();
            
            try
            {

                var jObject = JObject.Parse(json);
                JArray customersArray = (JArray)jObject["customers"];

                for (int i = 0; i < customersArray.Count; i++) {
                    customers.Add(customersArray[0].ToObject<Customer>());
                        }
            }catch(Exception exer)
            {
            }
            ro.Customers = customers;
          return  ro;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            var json = System.IO.File.ReadAllText(jsonFile);
            Customer varCust = new Customer();

            try { 
         
                var jObject = JObject.Parse(json);
                JArray customersArray = (JArray)jObject["customers"];
                            

                if (id > 0)
                {
                    var CustomerName = string.Empty;
                    var CustomerToFind = customersArray.FirstOrDefault(obj => obj["id"].Value<int>() == id);
                    varCust = CustomerToFind.ToObject<Customer>();
                }
                else
                {
                    Console.Write("Invalid Customer ID, Try Again!");
                  
                }
                return varCust;
            }
            catch (Exception  exe)
            {
                string msg = exe.Message;
                throw;
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public int Post([FromBody] Customer cust)
        {
            string json = System.IO.File.ReadAllText(jsonFile);

            try
            {
                var jObject = JObject.Parse(json);
                JArray customersArray = (JArray)jObject["customers"];
               
                var custId = cust.id;

                if (custId > 0)
                {
                    
                    var custName = cust.name;

                    foreach (var custer in customersArray.Where(obj => obj["id"].Value<int>() == custId))
                    {
                        custer["name"] = !string.IsNullOrEmpty(custName) ? custName : "";
                    }

                    jObject["customers"] = customersArray;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(jsonFile, output);
                }
                else
                {
                    Console.Write("Invalid CustomerID, Try Again!");
                     
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                Console.WriteLine("Update Error : " + ex.Message.ToString());
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public int  Put([FromBody] Customer cust)
        {

            var newCustomer = "{'id':" + cust.id + ", 'name': '" + cust.name + "'}";
            try
            {
                var json = System.IO.File.ReadAllText(jsonFile);
                var jsonObj = JObject.Parse(json);
                var customerArray = jsonObj.GetValue("customers") as JArray;
                var VarCustomer = JObject.Parse(newCustomer);
                customerArray.Add(VarCustomer);

                jsonObj["customers"] = customerArray;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(jsonFile, newJsonResult);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                Console.WriteLine("Add Error : " + ex.Message.ToString());
            }

        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
