using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Common.HubMessagesModel
{
    public class MessageModel
    {
        public string? SenderName { get; set; }
        public string? SenderPhotoUrl { get; set; }
        public int RecipientId { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientPhotoUrl { get; set; }
        public string MessageText { get; set; }
        public bool? IsRead { get; set; }
        
        public DateTime? DateRead { get; set; }
        public int CreatedBy { get; set; }
    }
}
