using  System.ComponentModel.DataAnnotations;


namespace YOYO.HRMS.Web.SystemManagement.Areas.SystemManagement.Models
{
    public class TestModel
    {
        [Required]
        [StringLength(1000,MinimumLength = 4)]
        public string SystemNo { get; set; }
        [Required]        
        public int Seqno { get; set; }
    }
}