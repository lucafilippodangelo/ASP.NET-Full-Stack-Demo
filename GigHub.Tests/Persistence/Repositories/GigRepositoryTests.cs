using FluentAssertions;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    //LDP3_005
    /*
     * This repository USE A CONCRETE class "ApplicationDbContext", to test it we have to create a database 
     * or EXTRACT TO AN INTERFACE and doing that we can MOCK it in our UNIT TEST
     */
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        //private Mock<ISet<Attendance>> _mockAttendances;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            //_mockAttendances = new Mock<DbSet<Attendance>>();

            //LD by creating the interface "IApplicationDbContext" I can mock it
            // AND PASS IT to the "new GigRepository" 
            var mockContext = new Mock<IApplicationDbContext>();


            //LD When from the context we go to "Gigs" we have to return "_mockGigs"
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            
            
            //mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);
            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            //LD we create a gig
            var gig = new Gig() { DateTime = System.DateTime.Now.AddDays(-1), ArtistId = "1" };

            //LD we create a list of gig and set as "SOURCE" of our MOCK "_mockGigs"
            _mockGigs.SetSource(new[] { gig });


            //LD NOW I DO THE ACTION!!! I call my repository by "_repository" 
            // and save the return in "gigs".
            var gigs = _repository.GetUpcomingGigsByArtist("1");

            //LD now I do the ASSERT
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist("1");

            gigs.Should().BeEmpty();

        }


        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);

        }

        //// This test helped me catch a bug in GetGigsUserAttending() method. 
        //// It used to return gigs from the past. 
        //[TestMethod]
        //public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturned()
        //{
        //    var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1) };
        //    var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

        //    _mockAttendances.SetSource(new[] { attendance });

        //    var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

        //    gigs.Should().BeEmpty();
        //}

        //[TestMethod]
        //public void GetGigsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        //{
        //    var gig = new Gig() { DateTime = DateTime.Now.AddDays(1) };
        //    var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

        //    _mockAttendances.SetSource(new[] { attendance });

        //    var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId + "-");

        //    gigs.Should().BeEmpty();
        //}

        //[TestMethod]
        //public void GetGigsUserAttending_UpcomingGigUserAttending_ShouldBeReturned()
        //{
        //    var gig = new Gig() { DateTime = DateTime.Now.AddDays(1) };
        //    var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

        //    _mockAttendances.SetSource(new[] { attendance });

        //    var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

        //    gigs.Should().Contain(gig);
        //}

    }
}
