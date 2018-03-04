using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }//LD PRIVATE because after the initialization shoudln't be possible change the value

        public ApplicationUser User { get; private set; }//LD PRIVATE because after the initialization shoudln't be possible change the value

        public Notification Notification { get; private set; }//LD PRIVATE because after the initialization shoudln't be possible change the value

        public bool IsRead { get; private  set; }

        //LD this is the constructor for entity framework, is a constructor that just .net can call
        protected UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (notification == null)
                throw new ArgumentNullException("notification");

            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }

    }
}