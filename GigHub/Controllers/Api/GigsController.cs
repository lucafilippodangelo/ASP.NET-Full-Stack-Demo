using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}


//[HttpDelete]
//public IHttpActionResult Cancel(int id)
//{
//    var userId = User.Identity.GetUserId();

//    //LD BEFORE cod111
//    //var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
//    //LD AFTER cod222
//    var gig = _context.Gigs
//         .Include(g => g.Attendances.Select(a => a.Attendee))
//         .Single(g => g.Id == id && g.ArtistId == userId);


//    if (gig.IsCanceled)
//        return NotFound();

//    //LD FANTASMAGORICO: vado a settare tutte le relazioni e poi salvo alla fine con il comando sotto "_context.SaveChanges();"
//    gig.Cancel();

//    _context.SaveChanges();

//    return Ok();
//}
