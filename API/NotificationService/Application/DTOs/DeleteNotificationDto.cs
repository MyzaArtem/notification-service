using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DeleteNotificationDto
    {
        public string ServiceName { get; set; }

        public string NotificationId { get; set; }
    }
}
