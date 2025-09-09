using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using mzm_safelink.api.helpers;
using mzm_safelink.application.dto.url.actions;
using mzm_safelink.application.helpers;
using mzm_safelink.application.interfaces;

namespace mzm_safelink.api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController(ICreateUrlShortenUseCase createUrlShortenUse, IRedirectUrlUseCase redirectUrlUseCase) : ControllerBase
    {
        private readonly ICreateUrlShortenUseCase _createUrlShortenUseCase = createUrlShortenUse;
        private readonly IRedirectUrlUseCase _redirectUrlUseCase = redirectUrlUseCase;

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlCreateDTO urlCreateDTO, [FromServices] IValidator<UrlCreateDTO> validator)
        {
            var validationResult = await validator.ValidateAsync(urlCreateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _createUrlShortenUseCase.ExecuteAsync(urlCreateDTO);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginalURL([FromRoute] string shortCode)
        {
            UseCaseResult<string> useCaseResult = await _redirectUrlUseCase.ExecuteAsync(shortCode);
            if (!useCaseResult.IsSuccess)
                return NotFound(new ApiResponse<string>(false, "URL original não encontrada"));
            
            return Redirect("https://www.google.com");
        }
    }    
}