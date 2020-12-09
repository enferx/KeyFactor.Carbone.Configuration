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
        }
    }
}
