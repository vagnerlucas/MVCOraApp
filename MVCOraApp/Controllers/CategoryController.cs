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
    public class CategoryController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View(db.TB_CATEGORY.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            TB_CATEGORY tb_category = db.TB_CATEGORY.Find(id);
            if (tb_category == null)
            {
                return HttpNotFound();
            }
            return View(tb_category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TB_CATEGORY tb_category)
        {
            if (ModelState.IsValid)
            {
                db.TB_CATEGORY.Add(tb_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TB_CATEGORY tb_category = db.TB_CATEGORY.Find(id);
            if (tb_category == null)
            {
                return HttpNotFound();
            }
            return View(tb_category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TB_CATEGORY tb_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TB_CATEGORY tb_category = db.TB_CATEGORY.Find(id);
            if (tb_category == null)
            {
                return HttpNotFound();
            }
            return View(tb_category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_CATEGORY tb_category = db.TB_CATEGORY.Find(id);
            db.TB_CATEGORY.Remove(tb_category);
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