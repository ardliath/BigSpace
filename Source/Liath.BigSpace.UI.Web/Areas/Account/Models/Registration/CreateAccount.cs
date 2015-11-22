using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liath.BigSpace.UI.Web.Areas.Account.Models.Registration
{
    public class CreateAccount
    {
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Errors { get; set; }
    }
}