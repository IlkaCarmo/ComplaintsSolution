using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Application.RabbitMQ
{
    public interface IRabbitMqPublisher
    {
        void Publish<ComplaintDto>(ComplaintDto message);
    }
}
