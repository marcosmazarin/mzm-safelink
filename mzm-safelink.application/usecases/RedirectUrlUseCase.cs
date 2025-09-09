using mzm_safelink.application.helpers;
using mzm_safelink.application.interfaces;
using mzm_safelink.domain.entities;
using mzm_safelink.domain.interfaces;

namespace mzm_safelink.application.usecases
{
    public class RedirectUrlUseCase(IUrlRepository urlRepository) : IRedirectUrlUseCase
    {
        private readonly IUrlRepository _urlRepository = urlRepository;
        public async Task<UseCaseResult<string>> ExecuteAsync(string shortCode)
        {
            Url? url = await _urlRepository.GetByShortCodeAsync(shortCode);
            if (url == null)
                return UseCaseResult<string>.Failure("URL not found", "404");

            return UseCaseResult<string>.Success("URL found", url.OriginalUrl);
        }
    }
}