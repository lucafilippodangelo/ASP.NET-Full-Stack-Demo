using FluentAssertions;
using GigHub.Controllers;
using GigHub.IntegrationTests.Extensions;
using GigHub.Models;
using GigHub.Persistence;
using GigHub.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            //LD we have created a "_context" in the "SetUp()" method, and then we have to dispose it
            _context.Dispose();
        }

        //####################################### METHOD IN TEST IN -> "Mine_WhenCalled_ShouldReturnUpcomingGigs()"
        //[Authorize]
        //public ViewResult Mine()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);
        //    return View(gigs);
        //}
        [Test ,Isolated ]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            // Arrange ################################
            //LD take an user from the users in the mocked db
            var user = _context.Users.First();
            //LD we need to correlate the current user with the controller "Identity" property
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            //LD now we need to add a "Gig" in the database, save it in the database and ensure that the application work
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //LD OUR controller by the method "Mine()", do a query in "Gigs"

            // Act
            var result = _controller.Mine();
         

            // Assert
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenGig()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = _controller.Update(new GigFormViewModel
            {
                Id = gig.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2
            });

            // Assert
            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Venue");
            gig.GenreId.Should().Be(2);

        }


    }
}
