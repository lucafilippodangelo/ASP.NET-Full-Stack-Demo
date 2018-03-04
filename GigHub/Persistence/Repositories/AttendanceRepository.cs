using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext db;

        public AttendanceRepository(ApplicationDbContext dbc)
        {
            db = dbc;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return db.Attendances
               .Where(a => a.AttendeeId == userId && a.Gig.DateTime > System.DateTime.Now)
               .ToList();
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return db.Attendances
                .SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }
    }
}