using System.ComponentModel.DataAnnotations;

namespace IpManager.Contract.Request
{
    public class ReportRequestModel
    {
        [Required]
        public List<string> CountryCodes { get; set; }
    }
}
