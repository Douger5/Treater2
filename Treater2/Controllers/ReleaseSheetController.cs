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
using Rotativa.MVC;

namespace Treater2.Controllers
{
    public class ReleaseSheetController : Controller
    {
        private TreatingContext db = new TreatingContext();

        // GET: ReleaseSheet
        public async Task<ActionResult> Index(int? id)
        {
            if (id == null) { id = 0; }
            ViewBag.OpenLabel = id;
            var releaseSheets = db.ReleaseSheets.Include(r => r.Item);
            releaseSheets = releaseSheets.OrderByDescending(d => d.ReleaseDateTime);
            return View(await releaseSheets.ToListAsync());
        }

        // GET: ReleaseSheet/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleaseSheet releaseSheet = await db.ReleaseSheets.FindAsync(id);
            if (releaseSheet == null)
            {
                return HttpNotFound();
            }
            return View(releaseSheet);
        }

        // GET: ReleaseSheet/Create
        public ActionResult Create()
        {
            ReleaseSheet model = new ReleaseSheet();
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemIDDescription");
            TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            model.ReleaseDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, centralZone);
            return View(model);
        }

        // POST: ReleaseSheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string actiontype, [Bind(Include = "ID,ItemID,ReleaseDateTime,Job,Board,Width,Length,Lbs,Sheets,Roll,Rejected,Notes")] ReleaseSheet releaseSheet)
        {
            if (ModelState.IsValid)
            {
                db.ReleaseSheets.Add(releaseSheet);
                await db.SaveChangesAsync();
                int ID = releaseSheet.ID;
                if (actiontype == "Save & Close") { return RedirectToAction("Index", new { id = 0 }); }
                if (actiontype == "Save & Label") { return RedirectToAction("Index", new { id = ID }); }
            }

            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemIDDescription", releaseSheet.ItemID);
            return View(releaseSheet);
        }

        // GET: ReleaseSheet/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleaseSheet releaseSheet = await db.ReleaseSheets.FindAsync(id);
            if (releaseSheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Description", releaseSheet.ItemID);
            return View(releaseSheet);
        }

        // POST: ReleaseSheet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ItemID,ReleaseDateTime,Job,Board,Width,Length,Lbs,Sheets,Roll,Rejected,Notes")] ReleaseSheet releaseSheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(releaseSheet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Description", releaseSheet.ItemID);
            return View(releaseSheet);
        }

        // GET: ReleaseSheet/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleaseSheet releaseSheet = await db.ReleaseSheets.FindAsync(id);
            if (releaseSheet == null)
            {
                return HttpNotFound();
            }
            return View(releaseSheet);
        }

        // POST: ReleaseSheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReleaseSheet releaseSheet = await db.ReleaseSheets.FindAsync(id);
            db.ReleaseSheets.Remove(releaseSheet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public ActionResult CreateRSPDF(int? id)
        {
            ReleaseSheet model = db.ReleaseSheets.Find(id);
            return View(model);
        }

        public ActionResult Label(int? id)
        {
            ReleaseSheet model = db.ReleaseSheets.Find(id);
            return View("CreateRSPDF", model);
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
