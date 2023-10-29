using System.ComponentModel.DataAnnotations;

namespace ITTrend.Dto
{
    public class LoginReqDto : EntityDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
