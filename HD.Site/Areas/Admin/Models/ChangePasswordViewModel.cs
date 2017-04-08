namespace HD.Site.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        public string OlderPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}