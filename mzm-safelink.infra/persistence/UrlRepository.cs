using Microsoft.EntityFrameworkCore;
using mzm_safelink.domain.entities;
using mzm_safelink.domain.interfaces;

namespace mzm_safelink.infra.persistence
{
    public class UrlRepository(AppDbContext context) : BaseRepository<Url>(context), IUrlRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<Url?> GetByShortCodeAsync(string shortCode)
        {
            return await _context.Urls.FirstOrDefaultAsync(u => u.CodeUrl == shortCode);            
        }
        
    }
}