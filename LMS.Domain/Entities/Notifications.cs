﻿using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Notifications : BaseEntity
    {
        public int RecipientId { get; set; }
        public bool IsRead { get; set; }
        public bool Deleted { get; set; }
        public int ReferenceMassageId { get; set; }
        public int RecipientRoleId { get; set; }
        public int NotificationTypeId { get; set; }
        public virtual ICollection<NotificationsDetails> NotificationsDetails { get; set; }
    }
}
