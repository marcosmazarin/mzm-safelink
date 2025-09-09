using mzm_safelink.application.helpers;

namespace mzm_safelink.application.interfaces
{
    public interface IRedirectUrlUseCase
    {
        Task<UseCaseResult<string>> ExecuteAsync(string shortCode);
    }
}