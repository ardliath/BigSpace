using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.UserAccountDomain;
using Liath.BigSpace.Session;
using Liath.BigSpace.UI.Web.Areas.Account.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Liath.BigSpace.UI.Web.Areas.Account.Controllers
{
    public class AuthenticationController : Controller
    {
        private ISecurityManager _securityManager;
        private ISessionManager _sessionManager;

        public AuthenticationController(ISessionManager sessionManager, ISecurityManager securityManager)
        {
            if (securityManager == null) throw new ArgumentNullException("securityManager");
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");

            _securityManager = securityManager;
            _sessionManager = sessionManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View(new Login());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string returnUrl)
        {
            if (login == null) throw new ArgumentNullException("login");

            using (_sessionManager.CreateUnitOfWork())
            {
                SecurityUserAccount currentUser;
                if (_securityManager.Login(login.EmailAddress, login.Password, out currentUser))
                {
                    // We'll have all sorts of issues if we try and set auths/redirects without http contexts
                    if (this.HttpContext != null)
                    {
                        FormsAuthentication.SetAuthCookie(currentUser.Username, false);
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return Redirect(Url.RouteUrl(new
                            {
                                area = "Site",
                                controller = "Default",
                                action = "Index"
                            }));
                        }
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.OK); // this is a hack designed for the spec tests
                }
                else
                {
                    return View(login);
                }
            }
        }
    }
}