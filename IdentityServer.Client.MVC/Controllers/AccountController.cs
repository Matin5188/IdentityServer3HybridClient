﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityServer.Client.MVC.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult SignIn()
        {
            return this.Redirect("/");
        }

        public ActionResult SignOut()
        {
            this.Request.GetOwinContext().Authentication.SignOut();
            return this.Redirect("/");
        }
    }
}