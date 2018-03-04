using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext db;

        public GigRepository(IApplicationDbContext dbc)
        {
            db = dbc;
        }

        public Gig GetGigWithAttendees(int GigId)
        {
            return db.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == GigId);
        }

        public IEnumerable<Gig> GetGigUserAttending(string userId)
        {
            //LD he start from "Attendances", filter it and after MOVE THE FOCUS on "Gig" AFTER for the "Gig" include the navigational properties informations
            return db.Attendances
                .Where(a => a.AttendeeId == userId) //LD at this point we have a list of "Attendances", but we want a list of "Gig"
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
            //LD the query return all the "gigs" where a specific user is going

        }

        public Gig GetGig(int gigId)
        {
            return db.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId)
        {
            return db.Gigs
                .Where(g =>
                    g.ArtistId == artistId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }


        public void Add(Gig gig)
        {
            db.Gigs.Add(gig);
        }

    }
}



