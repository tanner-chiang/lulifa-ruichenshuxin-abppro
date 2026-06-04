using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace RuichenShuxin.AbpPro.Storage.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
