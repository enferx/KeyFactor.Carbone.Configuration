using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace KeyFactor.Carbone.Configuration.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}
