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
    public class SubCategoryController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /SubCategory/

        public ActionResult Index()
        {
            var tb_subcategory = db.TB_SUBCATEGORY.Include(t => t.TB_CATEGORY);
            return View(tb_subcategory.ToList());
        }

        //
        // GET: /SubCategory/Details/5

        public ActionResult Details(int id = 0)
        {
            TB_SUBCATEGORY tb_subcategory = db.TB_SUBCATEGORY.Find(id);
            if (tb_subcategory == null)
            {
                return HttpNotFound();
            }
            return View(tb_subcategory);
        }

        //
        // GET: /SubCategory/Create

        public ActionResult Create()
        {
            ViewBag.ID_CATEGORY = new SelectList(db.TB_CATEGORY, "ID_CATEGORY", "DESCRIPTION");
            return View();
        }

        //
        // POST: /SubCategory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TB_SUBCATEGORY tb_subcategory)
        {
            if (ModelState.IsValid)
            {
                db.TB_SUBCATEGORY.Add(tb_subcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CATEGORY = new SelectList(db.TB_CATEGORY, "ID_CATEGORY", "DESCRIPTION", tb_subcategory.ID_CATEGORY);
            return View(tb_subcategory);
        }

        //
        // GET: /SubCategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TB_SUBCATEGORY tb_subcategory = db.TB_SUBCATEGORY.Find(id);
            if (tb_subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CATEGORY = new SelectList(db.TB_CATEGORY, "ID_CATEGORY", "DESCRIPTION", tb_subcategory.ID_CATEGORY);
            return View(tb_subcategory);
        }

        //
        // POST: /SubCategory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TB_SUBCATEGORY tb_subcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_subcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CATEGORY = new SelectList(db.TB_CATEGORY, "ID_CATEGORY", "DESCRIPTION", tb_subcategory.ID_CATEGORY);
            return View(tb_subcategory);
        }

        //
        // GET: /SubCategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TB_SUBCATEGORY tb_subcategory = db.TB_SUBCATEGORY.Find(id);
            if (tb_subcategory == null)
            {
                return HttpNotFound();
            }
            return View(tb_subcategory);
        }

        //
        // POST: /SubCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_SUBCATEGORY tb_subcategory = db.TB_SUBCATEGORY.Find(id);
            db.TB_SUBCATEGORY.Remove(tb_subcategory);
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