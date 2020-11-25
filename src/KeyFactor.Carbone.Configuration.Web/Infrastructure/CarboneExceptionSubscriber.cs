using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ExceptionHandling;

namespace KeyFactor.Carbone.Configuration.Web.Infrastructure
{
    public class CarboneExceptionSubscriber : ExceptionSubscriber
    {
        public override Task HandleAsync(ExceptionNotificationContext context)
        {
            Console.WriteLine(context.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
