using System.ComponentModel.DataAnnotations;

namespace RenderApp.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress, MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(300)]
        public string? PhotoPath { get; set; } // путь к файлу фото    }
    }
}
