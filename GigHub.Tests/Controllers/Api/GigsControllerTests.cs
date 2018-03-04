using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Models;
using GigHub.Persistence;
using GigHub.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;


namespace GigHub.Tests.Controllers.Api
{
    //LDP3_006
    [TestClass]
    public class GigsControllerTests
    {

        private GigHub.Controllers.Api.GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        //LD THE CONTROLLER OF THE TEST CLASS IS USEFUL TO INITIALIZE THE 
        // CONTROLLER AND THE USER THAT I WANT TEST
        public GigsControllerTests()
        {
            _mockRepository = new Mock<IGigRepository>();

            //LD MOCK of "IUnitOfWork"
            var mockUoW = new Mock<IUnitOfWork>(); 

            //LD MOCK of a specific repository in unitOfWork 
            mockUoW .SetupGet (u => u.Gigs).Returns (_mockRepository .Object ); 

            //LD mock of the specific controller
             _controller = new GigsController(mockUoW.Object);
             _userId = "1";
            //LD we will assign the user to the controller into the method "MockCurrentUser".
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            //LD WE NEED TO TELL TO OUR MOCK REPOSITORY TO RETURNA CANCELED GIG
            
            //LD I create a "Gig" and I cancel it. I will use it BELOW
            var gig = new Gig();
            gig.Cancel();

            //LD now that I have an istance of repository, internally to this repository I go 
            //to the method "GetGigWithAttendees" and I return the GIG that I CANCELED ABOVE!!!
            // THAT IS BECAUSE into this action the repository will be called and we
            // want that the instance returned is a "canceled gig" 
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);


            //LD now we call the controller. when I will call the "Cancel" ACTION and
            //the "GetGigWithAttendees" method of the "mockRepository" called by the "UnitOfWork",
            //I will be sure to get the canceled gig 
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }


        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            //LD creation and setup of a gig with specific propertyes
            var gig = new Gig { ArtistId = _userId + "-" };

            //LD in the current instance of the mocked repository I move internally to 
            //the method "GetGigWithAttendees" and I return the gig just created ABOVE
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    _mockRepository = new Mock<IGigRepository>();

        //    var mockUoW = new Mock<IUnitOfWork>();
        //    mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

        //    _controller = new GigsController(mockUoW.Object);
        //    _userId = "1";
        //    _controller.MockCurrentUser(_userId, "user1@domain.com");
        //}






        //[TestMethod]
        //public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        //{
        //    var gig = new Gig { ArtistId = _userId + "-" };

        //    _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

        //    var result = _controller.Cancel(1);

        //    result.Should().BeOfType<UnauthorizedResult>();
        //}

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }

    }
}
