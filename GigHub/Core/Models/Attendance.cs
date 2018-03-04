using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    //LDP1_003
    //LD "Attandance" is a join table between "Gig" and "ApplicationUser"
    public class Attendance
    {
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }

        [Key] //LD part of the composite key: "GigId"+"AttendeeId"
        [Column(Order = 1)]
        public int GigId { get; set; }

        [Key] //LD part of the composite key: "GigId"+"AttendeeId"
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}