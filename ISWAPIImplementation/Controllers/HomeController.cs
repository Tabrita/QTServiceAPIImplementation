using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ISWAPIImplementation.Models;
using ISWAPIImplementation.Helpers;
using System.Net.Http;
using ISWAPIImplementation.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ISWAPIImplementation.ViewModels;

namespace ISWAPIImplementation.Controllers
{
    public class HomeController : Controller
    {
        ISWAPI _api = new ISWAPI();
        AllMethods service = new AllMethods();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Get All Billers
        public async Task<IActionResult> GetBillersAsync(int page = 1)
        {
            
            var billers = new List<Billers>();

            HttpClient client = service.GetAllBillers();

            try
            {
                var response = await client.GetAsync("billers");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var deserializedjsonResult = JObject.Parse(result);
                    var ListOfBillers = deserializedjsonResult["billers"].ToList();

                    billers = ListOfBillers.Select(b => b.ToObject<Billers>()).ToList();
                    //var biller = JsonConvert.DeserializeObject<List<Biller>>(result);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            var billersVM = new BillerViewModel
            {
                Billers = billers.OrderByDescending(b => b.BillerName),
                BillerPerPage = 15,
                CurrentPage = page
            };

            return View(billersVM);
        }

        //Get All Categories
        public async Task<ActionResult> GetCategories()
        {
            var categories = new List<Category>();

            HttpClient client = service.GetAllCategories();

            try
            {
                var response = await client.GetAsync("categorys");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var deserializedjsonResult = JObject.Parse(result);
                    var ListOfBillers = deserializedjsonResult["categorys"].ToList();

                    categories = ListOfBillers.Select(b => b.ToObject<Category>()).ToList();
                    //var biller = JsonConvert.DeserializeObject<List<Biller>>(result);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            var categoryVM = new CategoryViewModel
            {
                Categories = categories.OrderBy(c => c.CategoryName)
            };

            return PartialView(categoryVM);
        }

        //Get Billers By Categories
        public async Task<ActionResult> GetBillerByCategory(int categoryId)
        {
            string uri = "categorys/" + categoryId + "/billers";

            HttpClient client = service.GetBillerPaymentItems(categoryId);
            var billers = new List<Billers>();
            var paymentItemsViewModel = new PaymentItemsViewModel();
            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var deserializedjsonResult = JObject.Parse(result);
                    var listOfPaymentItems = deserializedjsonResult["paymentitems"].ToList();

                    billers = listOfPaymentItems.Select(x => x.ToObject<Billers>()).ToList();
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var billerVM = new BillerViewModel
            {
                Billers = billers.OrderBy(b => b.BillerName)
            };

            return View(billerVM);
        }

        //Get PaymentItems
        public async Task<IActionResult> GetBillerPaymentItems(int id)
        {
            string uri = "billers/" + id + "/paymentitems";

            HttpClient client = service.GetBillerPaymentItems(id);
            var paymentItems = new List<PaymentItems>();
            var paymentItemsViewModel = new PaymentItemsViewModel();
            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var deserializedjsonResult = JObject.Parse(result);
                    var listOfPaymentItems = deserializedjsonResult["paymentitems"].ToList();

                    paymentItems = listOfPaymentItems.Select(x => x.ToObject<PaymentItems>()).ToList();
                }

                //paymentItemsViewModel.PaymentItemsVM = "Biller Payment Items";
                paymentItemsViewModel.PaymentItemsVM = paymentItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(paymentItemsViewModel);
        }

        //Query Transaction
        //GET
        public async Task<IActionResult> TransactionQuery(string requestRef)
        {           
            var queryParams = new Dictionary<string, string>()
            {
                { "RequestReference", requestRef }
            };

            HttpClient client = service.TransactionQuery(requestRef);            
            try
            {
                var response = await client.GetAsync(queryParams.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var deserializedjsonResult = JObject.Parse(result);
                    var listOfPaymentItems = deserializedjsonResult["paymentitems"].ToList();

                    //paymentItems = listOfPaymentItems.Select(x => x.ToObject<PaymentItems>()).ToList();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        [HttpGet]
        public IActionResult SendBillPaymentAdvice()
        {
            var rand = new Random();
            BillPaymentPayload model = new BillPaymentPayload();

            ViewBag.Ref = new BillPaymentPayload().requestReference = "1453" + rand.Next(999999999);
            
            return View(model);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> SendBillPaymentAdvice(BillPaymentPayload model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }                       

            HttpClient client = service.SendBillPaymentAdvice();
            try
            {                
                var response = await client.PostAsJsonAsync<BillPaymentPayload>("payments/advices", model);

                if (response.IsSuccessStatusCode)
                {
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }
    }
}
