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
            string formatedUrl = await FormatOriginalUrl(urlCreateDTO.Url);

            Url shortenedUrl = new()
            {
                OriginalUrl = formatedUrl,
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

        private static Task<string> GenerateShortUrLCode()
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            StringBuilder shortUrl = new();

            while (shortUrl.Length <= 6)
                shortUrl.Append(chars[Random.Shared.Next(0, chars.Length - 1)]);

            return Task.FromResult(shortUrl.ToString());
        }

        private static Task<string> FormatOriginalUrl(string originalUrl)
        {
            if (!originalUrl.TrimStart().StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                originalUrl = "https://" + originalUrl;

            if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out var urlDestino) || urlDestino.Scheme != Uri.UriSchemeHttps)
                return Task.FromResult("");

            return Task.FromResult(originalUrl);
        }
    }
}