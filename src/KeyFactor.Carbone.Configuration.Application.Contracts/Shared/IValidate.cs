using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Shared
{
    public interface IValidate<T1, T2, T3>
    {
        public Task<IReadOnlyList<ValidationError>> ValidateCreateAsync(T2 input);

        public Task<IReadOnlyList<ValidationError>> ValidateUpdateAsync(T1 id, T3 input);

    }
}
