using FluentValidation;

namespace mzm_safelink.application.dto.url.actions
{
    public class UrlCreateDTOValidator : AbstractValidator<UrlCreateDTO>
    {
        public UrlCreateDTOValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL cannot be empty.")
                .Must(BeAValidUrl).WithMessage("Invalid URL format.");
        }

        private bool BeAValidUrl(string url) => Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _);
    }
}