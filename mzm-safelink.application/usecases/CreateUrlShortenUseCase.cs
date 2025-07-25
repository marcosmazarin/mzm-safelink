using System.Text;
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
        public async Task<UseCaseResult<UrlCreateResponseDTO>> ExecuteAsync(UrlCreateDTO urlCreateDTO)
        {
            string shortUrlCode = await GenerateShortUrLCode();

            Url shortenedUrl = new()
            {
                OriginalUrl = urlCreateDTO.Url,
                ShortenUrl = new Uri(new Uri(Environment.GetEnvironmentVariable("BASE_DOMAIN")!), shortUrlCode).ToString(),
                CodeUrl = shortUrlCode
            };

            await _repo.AddAsync(shortenedUrl);

            UrlCreateResponseDTO urlCreateResponse = new()
            {
                ShortenUrl = shortenedUrl.ShortenUrl
            };

            return UseCaseResult<UrlCreateResponseDTO>.Success("URL created with success", urlCreateResponse);
        }

        private Task<string> GenerateShortUrLCode()
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            StringBuilder shortUrl = new();

            while (shortUrl.Length <= 6)
                shortUrl.Append(chars[Random.Shared.Next(0, chars.Length - 1)]);            
                
            return Task.FromResult(shortUrl.ToString());
        }
    }
}