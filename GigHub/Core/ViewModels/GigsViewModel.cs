using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }

        //LD the "ILookup" interface is like a ditionary, in this case the key is "int" because of "GigId"
        //the second parameter "Attendance" is the type of element that we are storing in this lookup 
        public ILookup<int, Attendance> Attendances { get; set; }
    }
}