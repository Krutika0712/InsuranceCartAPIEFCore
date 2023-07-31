using Castle.Core.Resource;
using InsuranceCartAPIEFCore.Models;
using InsuranceCartAPIEFCore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace InsuranceCartAPIEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        [HttpGet]
        [Route("GetAllCustomer")]
        public async Task<ActionResult> GetCustomer()
        {
            try
            {

          
            List<Customer> custInfo = new List<Customer>();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    //       client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("https://localhost:7207/api/Customer");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var CustResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        custInfo = JsonConvert.DeserializeObject<List<Customer>>(CustResponse);

                    }
                    //returning the employee list to view  
                    return Ok(custInfo);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddCustomer(Customer c)
        {
            try 
            {
               Customer Custobj = new Customer();
               using (var httpClient = new HttpClient())
               {
                  StringContent content = new StringContent(JsonConvert.SerializeObject(c),
                  Encoding.UTF8, "application/json");

                   using (var response = await httpClient.PostAsync("https://localhost:7207/api/Customer", content))
                   {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Custobj = JsonConvert.DeserializeObject<Customer>(apiResponse);
                   }
               }
              return Ok(Custobj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetCustomerByID")]
        public async Task<ActionResult> UpdateCustomer(int id)
        {
            try
            {
                Customer customer = new Customer();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7207/api/Customer/GetCustomerById?id=" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer(int id,Customer c)
        {
            try
            {
                Customer receivedcust = new Customer();

                using (var httpClient = new HttpClient())
                {

                    int cid = c.CustomerId;
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(c)
                      , Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("https://localhost:7207/api/Customer?id=" + id, content1))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        receivedcust = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                }
                return Ok(receivedcust);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]

        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                Customer cust = new Customer();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync("https://localhost:7207/api/Customer?id=" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cust = JsonConvert.DeserializeObject<Customer>(apiResponse);

                    }
                }
                return Ok(cust);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Unable to delete the record. It is referenced by another entity.");
            }

        }
    }
}
