using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using DAL;
using System.Data;

namespace MVCOraApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TB_USER model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (CheckCredentialsData(model.LOGIN, model.PASSWD))
                {
                    FormsAuthentication.SetAuthCookie(model.LOGIN, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "O nome de usuário ou senha está incorreto");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(TB_USER model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Entities())
                {

                    var user = db.TB_USER.FirstOrDefault(u => u.LOGIN == model.LOGIN);

                    if (user == null)
                    {
                        var crypt = new SimpleCrypto.PBKDF2();
                        var encrypts = crypt.Compute(model.PASSWD);

                        var vUser = db.TB_USER.Create();

                        vUser.LOGIN = model.LOGIN;
                        vUser.PASSWD = encrypts;
                        vUser.PASSWSALT = crypt.Salt;
                        vUser.USER_NAME = model.USER_NAME;

                        db.TB_USER.Add(vUser);
                        db.SaveChanges();

                        return RedirectToAction("Index", "Home");

                    }
                    else
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", model.LOGIN));
                    
                    }
            }
            else
            {
                ModelState.AddModelError("", "Dados incorretos");
            }

            return View();//model);
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Sua senha foi alterada"
                : message == ManageMessageId.SetPasswordSuccess ? "Sua senha foi atribuida"
                : message == ManageMessageId.RemoveLoginSuccess ? "O login externo foi removido"
                : "";
            ViewBag.HasLocalPassword = true;
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(UserPasswordModel model)
        {
            bool hasLocalAccount = true;
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    bool changePasswordSucceeded = false;
                    try
                    {
                        using (var db = new Entities())
                        {

                            var user = db.TB_USER.FirstOrDefault(u => u.LOGIN == User.Identity.Name);

                            if (CheckCredentialsData(user.LOGIN, model.OldPassword))
                            {
                                var crypt = new SimpleCrypto.PBKDF2();
                                var encrypts = crypt.Compute(model.NewPassword);
                                user.PASSWD = encrypts;
                                user.PASSWSALT = crypt.Salt;
                                db.Entry(user).State = EntityState.Modified;
                                db.SaveChanges();
                                changePasswordSucceeded = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Não foi atribuída uma nova senha porque a senha atual está errada.");
                    }
                }
            }

            return View(model);
        }

        private bool CheckCredentialsData(string login, string passwd)
        {
            var crypt = new SimpleCrypto.PBKDF2();

            using (var db = new Entities())
            {
                var user = db.TB_USER.FirstOrDefault(u => u.LOGIN == login);

                if (user != null)
                    return (user.PASSWD == crypt.Compute(passwd, user.PASSWSALT));
            }

            return false;
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
