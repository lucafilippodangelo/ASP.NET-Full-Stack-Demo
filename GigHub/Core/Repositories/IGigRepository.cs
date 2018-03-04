using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigUserAttending(string userId);
        Gig GetGigWithAttendees(int GigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId);
    }
}