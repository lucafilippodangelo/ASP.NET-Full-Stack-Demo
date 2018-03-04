using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Moq;

namespace GigHub.IntegrationTests.Extensions
{
    public static class ControllerExtensions
    {
        /*
         * //LDP3_004
        //LD to don't revrite code when I have to test a controller and I have to create an user. 
        to do that he create an EXTENSION METHOD TO APPLY TO THE CONTROLLER in the folder "Extensions" -> 
        "ApiControllerExtensions.cs"
        by doing that we are creating IDENTITY and PRINCIPLE OBJECT to MOC THE USER
        */

        public static void MockCurrentUser(this Controller controller, string userId, string username)
        {
            var identity = new GenericIdentity(username);
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));

            var principal = new GenericPrincipal(identity, null);

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext == Mock.Of<HttpContextBase>(http =>
                    http.User == principal));
        }
    }
}
