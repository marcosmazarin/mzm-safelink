using System.ComponentModel.DataAnnotations;

namespace mzm_safelink.domain.entities
{    
    public class Url
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string OriginalUrl { get; set; } = string.Empty;
        [Required]
        public string ShortenUrl { get; set; } = string.Empty;
        [Required]
        [MaxLength(7)]
        public string CodeUrl { get; set; } = string.Empty;
    }
}