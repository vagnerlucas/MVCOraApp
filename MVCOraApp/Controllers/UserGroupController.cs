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
    public class UserGroupController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /UserGroup/

        public ActionResult Index()
        {
            var tb_group_user = db.TB_GROUP_USER.Include(t => t.TB_GROUP).Include(t => t.TB_USER);//.Include(t => t.TB_PRODUCT);
            return View(tb_group_user.ToList());
        }

        //
        // GET: /UserGroup/Details/5

        public ActionResult Details(int id = 0)
        {
            TB_GROUP_USER tb_group_user = db.TB_GROUP_USER.Find(id);
            if (tb_group_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_group_user);
        }

        //
        // GET: /UserGroup/Create

        public ActionResult Create()
        {
            ViewBag.ID_GROUP = new SelectList(db.TB_GROUP, "ID_GROUP", "GROUP_NAME");
            ViewBag.ID_USER = new SelectList(db.TB_USER, "ID_USER", "LOGIN");
            ViewBag.ID_GROUP_USER = new SelectList(db.TB_PRODUCT, "ID_PRODUCT", "DESCRIPTION");
            return View();
        }

        //
        // POST: /UserGroup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TB_GROUP_USER tb_group_user)
        {
            if (ModelState.IsValid)
            {
                db.TB_GROUP_USER.Add(tb_group_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_GROUP = new SelectList(db.TB_GROUP, "ID_GROUP", "GROUP_NAME", tb_group_user.ID_GROUP);
            ViewBag.ID_USER = new SelectList(db.TB_USER, "ID_USER", "LOGIN", tb_group_user.ID_USER);
            ViewBag.ID_GROUP_USER = new SelectList(db.TB_PRODUCT, "ID_PRODUCT", "DESCRIPTION", tb_group_user.ID_GROUP_USER);
            return View(tb_group_user);
        }

        //
        // GET: /UserGroup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TB_GROUP_USER tb_group_user = db.TB_GROUP_USER.Find(id);
            if (tb_group_user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_GROUP = new SelectList(db.TB_GROUP, "ID_GROUP", "GROUP_NAME", tb_group_user.ID_GROUP);
            ViewBag.ID_USER = new SelectList(db.TB_USER, "ID_USER", "LOGIN", tb_group_user.ID_USER);
            ViewBag.ID_GROUP_USER = new SelectList(db.TB_PRODUCT, "ID_PRODUCT", "DESCRIPTION", tb_group_user.ID_GROUP_USER);
            return View(tb_group_user);
        }

        //
        // POST: /UserGroup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TB_GROUP_USER tb_group_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_group_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_GROUP = new SelectList(db.TB_GROUP, "ID_GROUP", "GROUP_NAME", tb_group_user.ID_GROUP);
            ViewBag.ID_USER = new SelectList(db.TB_USER, "ID_USER", "LOGIN", tb_group_user.ID_USER);
            ViewBag.ID_GROUP_USER = new SelectList(db.TB_PRODUCT, "ID_PRODUCT", "DESCRIPTION", tb_group_user.ID_GROUP_USER);
            return View(tb_group_user);
        }

        //
        // GET: /UserGroup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TB_GROUP_USER tb_group_user = db.TB_GROUP_USER.Find(id);
            if (tb_group_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_group_user);
        }

        //
        // POST: /UserGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_GROUP_USER tb_group_user = db.TB_GROUP_USER.Find(id);
            db.TB_GROUP_USER.Remove(tb_group_user);
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