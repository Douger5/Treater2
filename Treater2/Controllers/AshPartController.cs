using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treater2.DAL;
using Treater2.Models;

namespace Treater2.Controllers
{
    public class AshPartController : Controller
    {
        private TreatingContext db = new TreatingContext();

        // GET: AshPart
        public async Task<ActionResult> Index()
        {
            return View(await db.AshParts.ToListAsync());
        }

        // GET: AshPart/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AshPart ashPart = await db.AshParts.FindAsync(id);
            if (ashPart == null)
            {
                return HttpNotFound();
            }
            return View(ashPart);
        }

        // GET: AshPart/Create
        public ActionResult Create()
        {
            ViewBag.TreatingSpecID = new SelectList(db.TreatingSpecs, "TreatingSpecID", "TreatingSpecID");
            return View();
        }

        // POST: AshPart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AshPartID,ResinMix,TreatingSpecID,Paper,Size")] AshPart ashPart)
        {
            if (ModelState.IsValid)
            {
                db.AshParts.Add(ashPart);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TreatingSpecID = new SelectList(db.TreatingSpecs, "TreatingSpecID", "TreatingSpecID", ashPart.TreatingSpecID);
            return View(ashPart);
        }

        // GET: AshPart/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AshPart ashPart = await db.AshParts.FindAsync(id);
            if (ashPart == null)
            {
                return HttpNotFound();
            }
            ViewBag.TreatingSpecID = new SelectList(db.TreatingSpecs, "TreatingSpecID", "TreatingSpecID", ashPart.TreatingSpecID);
            return View(ashPart);
        }

        // POST: AshPart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AshPartID,ResinMix,TreatingSpecID,Paper,Size")] AshPart ashPart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ashPart).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TreatingSpecID = new SelectList(db.TreatingSpecs, "TreatingSpecID", "TreatingSpecID", ashPart.TreatingSpecID);
            return View(ashPart);
        }

        // GET: AshPart/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AshPart ashPart = await db.AshParts.FindAsync(id);
            if (ashPart == null)
            {
                return HttpNotFound();
            }
            return View(ashPart);
        }

        // POST: AshPart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AshPart ashPart = await db.AshParts.FindAsync(id);
            try
            {
                db.AshParts.Remove(ashPart);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "You can't delete an AshPart that has been used in the Treater Test Log.");
            }
            return View(ashPart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
