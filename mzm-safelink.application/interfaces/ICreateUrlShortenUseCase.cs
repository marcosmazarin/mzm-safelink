using mzm_safelink.application.dto.url.actions;
using mzm_safelink.application.helpers;

namespace mzm_safelink.application.interfaces
{
    public interface ICreateUrlShortenUseCase
    {
        Task<UseCaseResult<UrlCreateResponseDTO>> ExecuteAsync(UrlCreateDTO urlCreateDTO);
    }
}