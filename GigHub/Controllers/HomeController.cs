using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private readonly AttendanceRepository _AttendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _AttendanceRepository = new AttendanceRepository(_context);
        }

         
        public ActionResult Index(String query=null)
        {
            var upcomingGigs = _context.Gigs
                                .Include(g => g.Artist) //LD we load all the navigational properties
                                .Include (g => g.Genre)
                                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            //LD Updating the query with the SEARCH
            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                            g.Artist.Name.Contains(query) ||
                            g.Genre.Name.Contains(query) ||
                            g.Venue.Contains(query));
            }


            //LD LOOKUP()
            var userId = User.Identity.GetUserId();
            var attendances = _AttendanceRepository .GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);//LD I look into attendance by "GigId", then I add "Attendances" in "GigsViewModel"


            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs",viewModel); //LD before: return View(upcomingGigs);

                                       
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}