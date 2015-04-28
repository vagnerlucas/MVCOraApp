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
    public class GroupController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Group/

        public ActionResult Index()
        {
            return View(db.TB_GROUP.ToList());
        }

        //
        // GET: /Group/Details/5

        public ActionResult Details(int id = 0)
        {
            TB_GROUP tb_group = db.TB_GROUP.Find(id);
            if (tb_group == null)
            {
                return HttpNotFound();
            }
            return View(tb_group);
        }

        //
        // GET: /Group/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TB_GROUP tb_group)
        {
            if (ModelState.IsValid)
            {
                db.TB_GROUP.Add(tb_group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_group);
        }

        //
        // GET: /Group/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TB_GROUP tb_group = db.TB_GROUP.Find(id);
            if (tb_group == null)
            {
                return HttpNotFound();
            }
            return View(tb_group);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TB_GROUP tb_group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_group);
        }

        //
        // GET: /Group/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TB_GROUP tb_group = db.TB_GROUP.Find(id);
            if (tb_group == null)
            {
                return HttpNotFound();
            }
            return View(tb_group);
        }

        //
        // POST: /Group/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_GROUP tb_group = db.TB_GROUP.Find(id);
            db.TB_GROUP.Remove(tb_group);
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