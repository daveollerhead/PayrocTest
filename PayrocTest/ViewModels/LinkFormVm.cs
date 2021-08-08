using System.ComponentModel.DataAnnotations;
using PayrocTest.Attributes;

namespace PayrocTest.ViewModels
{
    public class LinkFormVm
    {
        [Required]
        [ValidUrl]
        public string Link { get; set; }
    }
}