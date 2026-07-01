using System.ComponentModel.DataAnnotations;

namespace UserDirectory.Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string Pincode { get; set; } = null!;
    }
}
