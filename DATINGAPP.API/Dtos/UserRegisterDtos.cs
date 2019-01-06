using System.ComponentModel.DataAnnotations;

namespace DATINGAPP.API.Dtos
{
    public class UserRegisterDtos
    {
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="Must required char between 4 and 8.")]
        public string password { get; set; }
        [Required]
        public string  username { get; set; }
    }
}