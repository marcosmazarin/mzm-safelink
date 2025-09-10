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

        /// <summary>
        /// Cria uma URL encurtada a partir de uma URL original.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/url
        ///     {
        ///       "url": "https://exemplo.com"
        ///     }
        ///
        /// Resposta de sucesso:
        ///
        ///     {
        ///       "shortenUrl": "https://mzm-safelink.onrender.com/abc123"
        ///     }
        ///
        /// Resposta de erro:
        ///
        ///     [
        ///       { "propertyName": "url", "errorMessage": "URL inválida" }
        ///     ]
        /// </remarks>
        /// <returns>Retorna a URL encurtada ou mensagem de erro</returns>
        /// <response code="200">URL encurtada criada com sucesso</response>
        /// <response code="400">Erro ao criar URL encurtada</response>
        [HttpPost]
        [ProducesResponseType(typeof(UrlCreateResponseDTO), 200)]
        [ProducesResponseType(typeof(object), 400)]
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

        /// <summary>
        /// Redireciona para a URL original a partir do código encurtado.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/url/abc123
        ///
        /// Resposta de redirecionamento (302):
        ///   Redireciona para a URL original.
        ///
        /// Resposta de erro (404):
        ///
        ///     {
        ///       "success": false,
        ///       "message": "URL original não encontrada",
        ///       "data": null
        ///     }
        /// </remarks>
        /// <returns>Redireciona para a URL original ou retorna mensagem de erro</returns>
        /// <response code="302">Redireciona para a URL original</response>
        /// <response code="404">URL não encontrada</response>
        [HttpGet("{shortCode}")]
        [ProducesResponseType(302)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> RedirectToOriginalURL([FromRoute] string shortCode)
        {
            UseCaseResult<string> useCaseResult = await _redirectUrlUseCase.ExecuteAsync(shortCode);
            if (!useCaseResult.IsSuccess)
                return NotFound(new ApiResponse<string>(false, "URL original não encontrada"));

            return Redirect(useCaseResult.Data!);
        }
    }    
}