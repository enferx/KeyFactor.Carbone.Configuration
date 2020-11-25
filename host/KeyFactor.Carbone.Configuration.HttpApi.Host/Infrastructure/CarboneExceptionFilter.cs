using KeyFactor.Carbone.Configuration.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Json;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Infrastructure
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
            //if (context.Exception is AbpValidationException)
            //{
            //    //TODO: Parsear abpvalidationexception...               
            //}
            ////else if(context.Exception is BusinessException)
            //{
            //    var exception = context.Exception as BusinessException;
            //    context.Exception = new AbpValidationException(new List<ValidationResult>()
            //    {
            //        new ValidationResult(exception.Message, new List<string>(){ "Product.Number"})
            //    });
            //    context.ExceptionHandled = false;
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
