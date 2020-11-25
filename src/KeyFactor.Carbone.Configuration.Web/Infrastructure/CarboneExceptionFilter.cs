using KeyFactor.Carbone.Configuration.ProductManagement;
using KeyFactor.Carbone.Configuration.Web.Pages;
using KeyFactor.Carbone.Configuration.Web.Pages.Configuration.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Json;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Web.Infrastructure
{
    public class CarboneExceptionFilter : AbpExceptionFilter
    {
        public CarboneExceptionFilter(IExceptionToErrorInfoConverter errorInfoConverter, IHttpExceptionStatusCodeFinder statusCodeFinder, IJsonSerializer jsonSerializer):
            base(errorInfoConverter, statusCodeFinder, jsonSerializer)
        {

        }

        protected override bool ShouldHandleException(ExceptionContext context)
        {
            return base.ShouldHandleException(context);
        }

        protected override Task HandleAndWrapException(ExceptionContext context)
        {
            return base.HandleAndWrapException(context);
            //context.ModelState.AddModelError("Product.Number", context.Exception.Message);
            //context.ExceptionHandled = true;
            //context.Result = new RedirectToPageResult("/Configuration/Products/EditProduct", 
            //    routeValues: new { Id = context.HttpContext.Request.Form["Id"] });
            
            //base.HandleAndWrapException(context);
            ////if(context.Exception is AbpValidationException)
            //{
            //    //TODO: Parsear abpvalidationexception...               
            //} 
            //else if(context.Exception is BusinessException)
            //{
            //    var exception = context.Exception as BusinessException;
            //    context.Result = new JsonResult(new[] { exception });
            //    context.ExceptionHandled = true;
            //}

            //context.Result = new NoContentResult();
            //var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(EditProductModel));
            //var viewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(metadata, context.ModelState)
            //{
            //    ["Id"] = "fe5093a5-4d71-4c9b-b4f9-9d2c99499b2a",
            //    ["Model.Id"] = "fe5093a5-4d71-4c9b-b4f9-9d2c99499b2a"
            //};
            //context.Result = new ViewResult()
            //{
            //    ViewData = viewData,
            //    ViewName = "EditProduct",

            //};
            //(context.Result as ViewResult).ViewData.Model = new EditProductModel()
            //context.HttpContext.Response.Clear();
            //return Task.CompletedTask;
        }
    }
}
