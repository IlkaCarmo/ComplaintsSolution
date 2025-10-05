using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Application.Interfaces
{
    public interface ISqsPublisher
    {
        Task PublishAsync(string fileName);
    }
}
