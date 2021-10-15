using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Shared
{
    public interface IValidateUpdate<T1, T2>
    {
        public Task<List<ValidationError>> ValidateUpdateAsync(T1 id, T2 input);
    }
}
