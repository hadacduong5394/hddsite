using BotDetect.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HD.Site.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public MvcCaptcha LoginCaptCha { get; set; }
    }
}