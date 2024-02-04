using LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Entities
{
    public class NotificationsDetails : BaseEntity
    {
        public string MessageText { get; set; }
        public int NotificationsId { get; set; }
    }
}
