using GigHub.Models;
using System;

namespace GigHub.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        //LD we don't want return a normal "Gig" but a "GigDto",
        // in order to show to the client just the informations that we want.
        public GigDto Gig { get; set; } 
    }
}