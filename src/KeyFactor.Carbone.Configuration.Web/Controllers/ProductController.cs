using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Mvc;

namespace KeyFactor.Carbone.Configuration.Web.Controllers
{
    public class ProductController : Controller
    {
        public string Index()
        {
            return "This is my default action...";
        }

    }
}
