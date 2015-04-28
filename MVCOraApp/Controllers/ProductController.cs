using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Web.Security;

namespace MVCOraApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            var tb_product = db.TB_PRODUCT.Include(t => t.TB_SUBCATEGORY);//(t => t.TB_GROUP_USER);//.Include(t => t.TB_SUBCATEGORY);
            return View(tb_product.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            TB_PRODUCT tb_product = db.TB_PRODUCT.Find(id);
            if (tb_product == null)
            {
                return HttpNotFound();
            }
            return View(tb_product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.ID_PRODUCT = new SelectList(db.TB_GROUP_USER, "ID_GROUP_USER", "OBS");
            ViewBag.ID_SUBCATEGORY = new SelectList(db.TB_SUBCATEGORY, "ID_SUBCATEGORY", "DESCRIPTION");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TB_PRODUCT tb_product)
        {
            if (ModelState.IsValid)
            {
                var user = db.TB_USER.FirstOrDefault(u => u.LOGIN == User.Identity.Name);
                tb_product.ID_USER = user.ID_USER;
                var subcategory = db.TB_SUBCATEGORY.FirstOrDefault(u => u.ID_SUBCATEGORY == tb_product.ID_SUBCATEGORY);
                tb_product.TB_SUBCATEGORY = subcategory;
                db.TB_PRODUCT.Add(tb_product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PRODUCT = new SelectList(db.TB_GROUP_USER, "ID_GROUP_USER", "OBS", tb_product.ID_PRODUCT);
            ViewBag.ID_SUBCATEGORY = new SelectList(db.TB_SUBCATEGORY, "ID_SUBCATEGORY", "DESCRIPTION", tb_product.ID_SUBCATEGORY);
            return View(tb_product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TB_PRODUCT tb_product = db.TB_PRODUCT.Find(id);
            if (tb_product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PRODUCT = new SelectList(db.TB_GROUP_USER, "ID_GROUP_USER", "OBS", tb_product.ID_PRODUCT);
            ViewBag.ID_SUBCATEGORY = new SelectList(db.TB_SUBCATEGORY, "ID_SUBCATEGORY", "DESCRIPTION", tb_product.ID_SUBCATEGORY);
            return View(tb_product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TB_PRODUCT tb_product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PRODUCT = new SelectList(db.TB_GROUP_USER, "ID_GROUP_USER", "OBS", tb_product.ID_PRODUCT);
            ViewBag.ID_SUBCATEGORY = new SelectList(db.TB_SUBCATEGORY, "ID_SUBCATEGORY", "DESCRIPTION", tb_product.ID_SUBCATEGORY);
            return View(tb_product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TB_PRODUCT tb_product = db.TB_PRODUCT.Find(id);
            if (tb_product == null)
            {
                return HttpNotFound();
            }
            return View(tb_product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_PRODUCT tb_product = db.TB_PRODUCT.Find(id);
            db.TB_PRODUCT.Remove(tb_product);
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