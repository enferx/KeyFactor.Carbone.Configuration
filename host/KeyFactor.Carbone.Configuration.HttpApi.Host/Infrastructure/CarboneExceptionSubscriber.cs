using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;

namespace KeyFactor.Carbone.Configuration.Infrastructure
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
