using GigHub.Models;
using GigHub.Persistence;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //LD add a reference to "UnitOfWork"
        //LDP3_001
        public GigsController(IUnitOfWork unitOfWork)//LD DEPENDENCY INJECTION, I pass this parameter
        {
            _unitOfWork = unitOfWork;//LD I initialize my provate _UnitOfWork 
        }


        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel) //LD I receive the "ViewModel"
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ViewResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);

            return View(gigs);
        }

       

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            
            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances= _unitOfWork.Attendances .GetFutureAttendances(userId).ToLookup(a => a.GigId)//LD I look into attendance by "GigId", then I add "Attendances" in "GigsViewModel"
            };

            return View("Gigs",viewModel);
        }
        // GET: Gigs
        //public ActionResult Index()
        //{
        //    return View(_unitOfWork .Gigs.);
        //}

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated) //LD just if the user is authenticated.
            {
                var userId = User.Identity.GetUserId();
                
                //LDP3_001
                //LD in "Attendances" we check if we have any objects that match criterias
                viewModel.IsAttending = _unitOfWork.Attendances .GetAttendance(gig.Id, userId) != null;

                viewModel.IsFollowing = _unitOfWork.Followings .GetFollowing (userId , gig.ArtistId )!= null;
            }

            return View("Details", viewModel);
        }

        //LDP2_001
        // GET: Gigs/Create
        [Authorize] 
        public ActionResult Create()
        {

            //LD instead of pass the common view he 
            // "declare" the VIEWMODEL, after "fill" part of it, and then "pass" it to the view
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres .GetGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm",viewModel);
            
        }

        //LDP2_001
        // POST: Gigs/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel) 
        {

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres (); //LD if I don't set it I will have empty combo
                return View("GigForm ", viewModel);
            }

            var gig = new Gig
            {
                ArtistId  = User.Identity.GetUserId(),
                //LD  
                // DateTime = viewModel.GetDateTime, // when it was a property
                DateTime = viewModel.GetDateTime(),// now it is a method, I did the conversion because of REFLECTION
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);//LD now I delegate the repository to save. I have the reference to the "DbContext" there. //LD before --> db.Gigs.Add(gig);

            _unitOfWork.Complete();//LD old way --> db.SaveChanges();

            return RedirectToAction("Mine", "Gigs"); //LD after that I create a Gig I will be redirected to the view with all my GIGS
        }



        //// GET: Gigs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Gig gig = db.Gigs.Find(id);
        //    if (gig == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(gig);
        //}

        //LDP2_001
        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            //LD prepare the form autopopulated with the data from the db
            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                //LD by setting this the property "Value" in the view model will work well
                Id = gig.Id,
                Genres = _unitOfWork .Genres .GetGenres (),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId, 
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        ////LD common way to delete not used
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Gig gig = db.Gigs.Find(id);
        //    if (gig == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(gig);
        //}

        ////LD common way to delete not used
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Gig gig = db.Gigs.Find(id);
        //    db.Gigs.Remove(gig);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //LDP2_001
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork .Genres .GetGenres ();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);
            //LD he leave here in the controller the check of "ArtistId"
            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();



            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }



        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
