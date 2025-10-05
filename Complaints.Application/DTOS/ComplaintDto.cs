using System;
using System.ComponentModel.DataAnnotations;

namespace Complaints.Application.DTOS
{
    public class ComplaintDto
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string CPF { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        public string Channel { get; set; } = "Digital";
    }
}
