using System.ComponentModel.DataAnnotations;

namespace ContentService.Models
{
    public class Content
    {
        public Guid Id {get; set;}
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string title {get; set;}
        [Required]
        [StringLength(1000)]
        public string description {get; set;}
        [Required]
        [StringLength(1000)]
        public string url {get; set;}
        [Required]
        [StringLength(10)]
        public string Genre {get; set;}
    }
}
