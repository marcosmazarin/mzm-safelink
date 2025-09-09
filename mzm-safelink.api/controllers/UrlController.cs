using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using mzm_safelink.application.dto.url.actions;
using mzm_safelink.application.interfaces;

namespace mzm_safelink.api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController(ICreateUrlShortenUseCase createUrlShortenUse) : ControllerBase
    {
        private readonly ICreateUrlShortenUseCase _createUrlShortenUseCase = createUrlShortenUse;

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
    }    
}