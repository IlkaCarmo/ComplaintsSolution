using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Application.DTOS
{
    public class ComplaintDto : ComplaintInputDto
    {
        public List<string> AttachmentUrls { get; set; } = new();

    }
}
