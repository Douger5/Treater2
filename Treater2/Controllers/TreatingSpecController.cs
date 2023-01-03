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
    public class TreatingSpecController : Controller
    {
        private TreatingContext db = new TreatingContext();

        // GET: TreatingSpec
        public async Task<ActionResult> Index()
        {
            return View(await db.TreatingSpecs.ToListAsync());
        }

        // GET: TreatingSpec/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatingSpec treatingSpec = await db.TreatingSpecs.FindAsync(id);
            if (treatingSpec == null)
            {
                return HttpNotFound();
            }
            return View(treatingSpec);
        }

        // GET: TreatingSpec/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TreatingSpec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TreatingSpecID,FlowTarget,FlowMax,FlowMin,RCTarget,RCMax,RCMin,VolTarget,VolMax,VolMin," +
            "NetRCTarget,NetRCMin,NetRCMax,GurleyPorosityTarget,GurleyPorosityMin,GurleyPorosityMax,WetOutTarget,WetOutMin,WetOutMax," +
            "CaliperTarget,CaliperMin,CaliperMax,BasisWeightTarget,BasisWeightMin,BasisWeightMax,Comment")] TreatingSpec treatingSpec)
        {
            if (ModelState.IsValid)
            {
                db.TreatingSpecs.Add(treatingSpec);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(treatingSpec);
        }

        // GET: TreatingSpec/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatingSpec treatingSpec = await db.TreatingSpecs.FindAsync(id);
            if (treatingSpec == null)
            {
                return HttpNotFound();
            }
            return View(treatingSpec);
        }

        // POST: TreatingSpec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TreatingSpecID,FlowTarget,FlowMax,FlowMin,RCTarget,RCMax,RCMin,VolTarget,VolMax,VolMin," +
            "NetRCTarget,NetRCMin,NetRCMax,GurleyPorosityTarget,GurleyPorosityMin,GurleyPorosityMax,WetOutTarget,WetOutMin,WetOutMax," +
            "CaliperTarget,CaliperMin,CaliperMax,BasisWeightTarget,BasisWeightMin,BasisWeightMax,Comment")] TreatingSpec treatingSpec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatingSpec).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(treatingSpec);
        }

        // GET: TreatingSpec/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatingSpec treatingSpec = await db.TreatingSpecs.FindAsync(id);
            if (treatingSpec == null)
            {
                return HttpNotFound();
            }
            return View(treatingSpec);
        }

        // POST: TreatingSpec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TreatingSpec treatingSpec = await db.TreatingSpecs.FindAsync(id);
            try
            {
                db.TreatingSpecs.Remove(treatingSpec);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "You can't delete a Treating Specification that is used in an AshPart.");
            }
            return View(treatingSpec);
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
