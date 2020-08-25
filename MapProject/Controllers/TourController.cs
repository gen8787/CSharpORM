using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MapProject.Controllers
{
    public class TourController : Controller
    {
    // Convert current user in Session to uid
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("userId");
            }
        }
    // Returns true or false if current user is logged in
        private bool isLoggedIn
        {
            get
            {
                return uid != null;
            }
        }
    }
}