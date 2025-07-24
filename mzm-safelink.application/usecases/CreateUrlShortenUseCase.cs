using mzm_safelink.application.dto.url.actions;
using mzm_safelink.application.helpers;
using mzm_safelink.application.interfaces;
using mzm_safelink.domain.entities;
using mzm_safelink.domain.interfaces;

namespace mzm_safelink.application.usecases
{
    public class CreateUrlShortenUseCase(IBaseRepository<Url> repo) : ICreateUrlShortenUseCase
    {
        private readonly IBaseRepository<Url> _repo = repo;
        public Task<UseCaseResult<UrlCreateResponseDTO>> ExecuteAsync(UrlCreateDTO urlCreateDTO)
        {

        }

        private Task<string> GenerateShortUrL(string longUrl)
        {

        }
    }
}