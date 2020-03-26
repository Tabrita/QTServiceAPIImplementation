using ISWAPIImplementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISWAPIImplementation.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
     
        public IEnumerable<Category> GetAllCategories()
        {
            return Categories;
        }
    }
}
