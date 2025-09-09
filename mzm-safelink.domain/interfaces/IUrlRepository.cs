using mzm_safelink.domain.entities;

namespace mzm_safelink.domain.interfaces
{
    public interface IUrlRepository : IBaseRepository<Url>
    {
        Task<Url?> GetByShortCodeAsync(string shortCode);
    }
}