using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }
        

        [Required]
        public string ArtistId { get; set; }
        public ApplicationUser Artist { get; set; }

        public bool IsCanceled { get; private set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }


        [Required]
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            this.IsCanceled = true; //LD we refer to this specific object

            //LD here he just create the object "Notification"
            var notification = Notification.GigCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                //LD for each "User" I create an "UserNotification" instance
                // it will be saved in a second moment in the controller
                // so in the CONTROLLER I save the record od the "Gig"+"UserNotification". 
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            //LD I call the static method that is in the class "Notification" that create a new "Notification" and return it
            var notification = Notification.GigUpdated(this,DateTime,Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

    }
}