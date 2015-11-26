using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain.UserAccountDomain;
using Liath.BigSpace.Session;
using Liath.BigSpace.UI.Web.Areas.Account.Models.Registration;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Liath.BigSpace.UI.Web.Areas.Account.Controllers
{
    public class RegistrationController : Controller
    {
        private IRegistrationManager _registrationManager;
        private ISessionManager _sessionManager;
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public RegistrationController(ISessionManager sessionManager, IRegistrationManager registrationManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            if (registrationManager == null) throw new ArgumentNullException("registrationManager");

            _sessionManager = sessionManager;
            _registrationManager = registrationManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            return View(new CreateAccount());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(CreateAccount createAccount)
        {
            if (createAccount == null) throw new ArgumentNullException("CreateAccount");

            using(_sessionManager.CreateUnitOfWork())
            {
                try
                {
                    string error;
                    UserAccount user;
                    if (_registrationManager.RegisterUser(createAccount.Username, createAccount.EmailAddress, createAccount.Password, createAccount.ConfirmPassword, out user, out error))
                    {
                        // We'll have all sorts of issues if we try and set auths/redirects without http contexts
                        if (this.HttpContext != null)
                        {
                            FormsAuthentication.SetAuthCookie(createAccount.Username, false);
                            return Redirect(Url.RouteUrl(new
                            {
                                area = "OuterSpace",
                                controller = "LocalArea",
                                action = "Display"
                            }));
                        }
                    }
                    else
                    {
                        createAccount.Errors = error;
                    }
                }
                catch(Exception ex)
                {
                    logger.Error("Error registering user", ex);
                    _sessionManager.GetCurrentUnitOfWork().Rollback();
                }
            }

            return View(createAccount);
        }
    }
}