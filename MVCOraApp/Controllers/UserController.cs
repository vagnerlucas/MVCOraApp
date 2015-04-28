using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace MVCOraApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(db.TB_USER.ToList());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            TB_USER tb_user = db.TB_USER.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TB_USER tb_user)
        {
            if (ModelState.IsValid)
            {
                db.TB_USER.Add(tb_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TB_USER tb_user = db.TB_USER.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }

            return View(tb_user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TB_USER tb_user)
        {
            if (ModelState.IsValid)
            {
                var crypt = new SimpleCrypto.PBKDF2();
                var encrypts = crypt.Compute(tb_user.PASSWD);

                tb_user.PASSWD = encrypts;
                tb_user.PASSWSALT = crypt.Salt;

                db.Entry(tb_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TB_USER tb_user = db.TB_USER.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_USER tb_user = db.TB_USER.Find(id);
            db.TB_USER.Remove(tb_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}