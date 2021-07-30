using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Shared
{
    public interface IValidateCreate<T>
    {
        public Task<List<ValidationError>> ValidateCreateAsync(T input);

    }
}
