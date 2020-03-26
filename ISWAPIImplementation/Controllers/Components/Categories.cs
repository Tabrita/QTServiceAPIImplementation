using ISWAPIImplementation.Helpers;
using ISWAPIImplementation.Models;
using ISWAPIImplementation.Services;
using ISWAPIImplementation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ISWAPIImplementation.Controllers.Components
{
    public class Categories : ViewComponent
    {
        ISWAPI _api = new ISWAPI();
        AllMethods service = new AllMethods();

        public async Task<IViewComponentResult> InvokeAsync()
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

            return View("GetCategories", categoryVM);
        }
    }
}
