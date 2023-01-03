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
using Treater2.ViewModels;
using Rotativa.MVC;
using PagedList;
using DocumentFormat.OpenXml;
using ClosedXML;
using ClosedXML.Excel;
using ClosedXML.Extensions;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Treater2.Controllers
{
    public class TreaterTestController : Controller
    {
        private TreatingContext db = new TreatingContext();
        // GET: TreaterTest
        public ActionResult Index(int? ctid, int? page = 1, string searchstr = null, string daterange = null, int labelid = 0)
        {
            ViewBag.Page = page;
            ViewBag.SearchStr = searchstr;
            ViewBag.DateRange = daterange;
            ViewBag.OpenLabel = labelid;
            var treaterTests = db.TreaterTests.Include(t => t.AshPart)
                .Include(t => t.AshPart.TreatingSpec)
                .Include(t => t.TreatingLine);
            treaterTests = treaterTests.OrderByDescending(d => d.TreatmentDateTime);
            var treatercookie = Session["Treater"];
            if (ctid == null && treatercookie == null)
            {
                return RedirectToAction("Settings", "Home");
            }
            if (ctid != null)
            {
                Session["Treater"] = ctid;
            }
            if (ctid == null && treatercookie != null)
            {
                ctid = (int)treatercookie;
            }
            ViewBag.CurrentTreaterID = ctid;
            treaterTests = treaterTests.Where(s => s.TreatingLineID == ctid);
            ViewBag.CurrentTreater = db.TreatingLines.Find(ctid).TreaterName;
            if (treaterTests.Any() && !String.IsNullOrEmpty(searchstr))
            {
                treaterTests = treaterTests.Where(s => s.AshPartID.Contains(searchstr)
                                       || s.RollSkidNum.Contains(searchstr)
                                       || s.JobNum.Contains(searchstr));
            }
            var startdate = DateTime.Today.AddDays(-1);
            var enddate = DateTime.Today.AddDays(1);
            if (treaterTests.Any() && !String.IsNullOrEmpty(daterange))
            {
                startdate = Convert.ToDateTime(daterange.Split(' ')[0]);
                enddate = Convert.ToDateTime(daterange.Substring(daterange.IndexOf(" - ") + 3)).AddDays(1);
            }
            treaterTests = treaterTests.Where(s => s.TreatmentDateTime >= startdate
                                        && s.TreatmentDateTime <= enddate);
            if (treaterTests.Any())
            {
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                treaterTests.ForEach(d => d.TreatmentDateTime = TimeZoneInfo.ConvertTimeFromUtc(d.TreatmentDateTime, centralZone));
            }
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(treaterTests.ToPagedList(pageNumber, pageSize));
        }

        // GET: TreaterTest/Details/5
        public async Task<ActionResult> Details(int? id, int ctid, int? page=null, string searchstr=null, string daterange=null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreaterTest treaterTest = await db.TreaterTests.FindAsync(id);
            if (treaterTest == null)
            {
                return HttpNotFound();
            } else
            {
                DateTime utcTime = treaterTest.TreatmentDateTime;
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                treaterTest.TreatmentDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, centralZone);
            }
            ViewBag.Page = page;
            ViewBag.SearchStr = searchstr;
            ViewBag.DateRange = daterange;
            ViewBag.CurrentTreaterID = ctid;
            return View(treaterTest);
        }


        // GET: TreaterTest/Create
        public ActionResult Create(TreaterTest model, int? ctid, int? page = null, string searchstr = null, string daterange = null)
        {
            if (model == null)
            {
                model = new TreaterTest();
            }
            ViewBag.AshPartID = new SelectList(db.AshParts, "AshPartID", "AshPartID");
            ViewBag.Page = page;
            ViewBag.SearchStr = searchstr;
            ViewBag.DateRange = daterange;
            ViewBag.CurrentTreaterID = ctid;
            TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            model.TreatmentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,centralZone);
            model.TestNum = 1;
            ViewBag.LastRoll = "N/A";
            if (TempData["LastRoll"] != null)
            {
                ViewBag.LastRoll = TempData["LastRoll"];
            }
            ViewBag.BarConfigID = new SelectList(db.BarConfigs, "BarConfigID", "BarConfigID");
            if (model.TreatingLineID != 0)
            {
                ctid = model.TreatingLineID;
                Session["Treater"] = ctid;
                ViewBag.CurrentTreater = db.TreatingLines.Find(ctid).TreaterName;
            }
            else if (Session["Treater"] != null)
            {
                var ct = (int)Session["Treater"];
                model.TreatingLineID = ct;
                ViewBag.CurrentTreater = db.TreatingLines.Find(ct).TreaterName;
            }
            else
            {
                return RedirectToAction("Settings", "Home");
            }
            return View(model);
        }

        // POST: TreaterTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string actiontype, [
            Bind(Include = "ID,TreatingLineID,TreatmentDateTime,AshPartID,Paper,OperatorA,OperatorB,RollSkidNum,Paper_COA,Resin_COA,JobNum,Qty,UOM,DryRollNum,TestNum,Zone1Temp,Zone2Temp,Zone3Temp,Speed,PanConfig,PanLevel,BarConfigID,BarAngle,ResinTemp,ResinSolids,RawPaperWeight,DryPaperWeight,TreatedPaperWeight,VolatilesTreatedWeight,VolatilesOvenDryWeight,TreatedPressedWeight,GurleyPorosity,WetOut,Caliper,Notes")] TreaterTest treaterTest)
        {
            if (ModelState.IsValid)
            {
                DateTime localtime = treaterTest.TreatmentDateTime;
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                treaterTest.TreatmentDateTime = TimeZoneInfo.ConvertTimeToUtc(localtime, centralZone);
                treaterTest.DryRollNum = treaterTest.DryRollNum?.ToUpper();
                treaterTest.JobNum = treaterTest.JobNum?.ToUpper();
                treaterTest.RollSkidNum = treaterTest.RollSkidNum?.ToUpper();
                treaterTest.Paper_COA = treaterTest.Paper_COA?.ToUpper();
                treaterTest.Resin_COA = treaterTest.Resin_COA?.ToUpper();
                db.TreaterTests.Add(treaterTest);
                var ct = 
                await db.SaveChangesAsync();
                TempData["LastRoll"] = treaterTest.RollSkidNum;
                TempData["PanConfigInt"] = -1;
                if (treaterTest.PanConfig.HasValue)
                {
                    var PanConfStr = treaterTest.PanConfig.ToString();
                    TempData["PanConfInt"] = (int)((PanConfig)Enum.Parse(typeof(PanConfig), PanConfStr));
                }
                treaterTest.TreatmentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, centralZone);
                if (actiontype == "Save and Close") { return RedirectToAction("Index"); }
                if (actiontype == "Save and Next Test") {
                    return RedirectToAction("Create",  new TreaterTest{ RollSkidNum = treaterTest.RollSkidNum, AshPartID = treaterTest.AshPartID, TreatingLineID = treaterTest.TreatingLineID, TreatmentDateTime= treaterTest.TreatmentDateTime, OperatorA = treaterTest.OperatorA, OperatorB= treaterTest.OperatorB, JobNum = treaterTest.JobNum, UOM = treaterTest.UOM, TestNum = treaterTest.TestNum+1, DryRollNum = treaterTest.DryRollNum, Paper_COA = treaterTest.Paper_COA, Resin_COA = treaterTest.Resin_COA, DryPaperWeight = treaterTest.DryPaperWeight,
                        RawPaperWeight =treaterTest.RawPaperWeight, Speed = treaterTest.Speed, BarConfigID = treaterTest.BarConfigID, BarAngle = treaterTest.BarAngle, PanLevel = treaterTest.PanLevel, ResinSolids = treaterTest.ResinSolids }); }
                if (actiontype == "Save and Next Job") {
                    return RedirectToAction("Create", new TreaterTest { TreatingLineID = treaterTest.TreatingLineID, TreatmentDateTime = treaterTest.TreatmentDateTime, OperatorA = treaterTest.OperatorA, OperatorB = treaterTest.OperatorB, TestNum = 1 }); }
                if (actiontype == "Save and Next Skid")
                {
                    return RedirectToAction("Create", new TreaterTest {
                        AshPartID = treaterTest.AshPartID,
                        TreatingLineID = treaterTest.TreatingLineID,
                        TreatmentDateTime = treaterTest.TreatmentDateTime, OperatorA = treaterTest.OperatorA, OperatorB = treaterTest.OperatorB, 
                        JobNum = treaterTest.JobNum, TestNum = 1, Speed = treaterTest.Speed, BarConfigID = treaterTest.BarConfigID, BarAngle = treaterTest.BarAngle, PanLevel = treaterTest.PanLevel});
                }
            }
            return View(treaterTest);
        }

        // GET: TreaterTest/Edit/5
        public async Task<ActionResult> Edit(int? id, int ctid, int? page = null, string searchstr = null, string daterange = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreaterTest treaterTest = await db.TreaterTests.FindAsync(id);
            if (treaterTest == null)
            {
                return HttpNotFound();
            } else
            {
                DateTime utcTime = treaterTest.TreatmentDateTime;
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                treaterTest.TreatmentDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, centralZone);
            }
            ViewBag.AshPartID = new SelectList(db.AshParts, "AshPartID", "AshPartID", treaterTest.AshPartID);
            ViewBag.TreatingLineID = new SelectList(db.TreatingLines, "TreatingLineID", "TreaterName", treaterTest.TreatingLineID);
            ViewBag.BarConfigID = new SelectList(db.BarConfigs, "BarConfigID", "BarConfigID",treaterTest.BarConfigID);
            ViewBag.Page = page;
            ViewBag.SearchStr = searchstr;
            ViewBag.DateRange = daterange;
            ViewBag.CurrentTreaterID = ctid;
            return View(treaterTest);
        }

        // GET: TreaterTest/UpdateAshPart
        public async Task<ActionResult> GetAshPart(string id)
        {
            if (id == null)
            {
                return Json(new { Success = "false" });
            }
            AshPart AshPart = await db.AshParts.FindAsync(id);
            if (AshPart == null)
            {
                return Json(new { Success = "false" });
            }
            return Json(new { Success = "true", Data = new { AshPart = AshPart } });
        }

        // POST: TreaterTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string actiontype, [Bind(Include = "ID,TreatingLineID,TreatmentDateTime,AshPartID,OperatorA,OperatorB,RollSkidNum,JobNum,Qty,UOM,DryRollNum,Paper_COA,Resin_COA,TestNum,Zone1Temp,Zone2Temp,Zone3Temp,Speed,PanConfig,PanLevel,BarConfigID,BarAngle,ResinTemp,ResinSolids,RawPaperWeight,DryPaperWeight,TreatedPaperWeight,VolatilesTreatedWeight,VolatilesOvenDryWeight,TreatedPressedWeight,ResinContentCustomer,GurleyPorosity,WetOut,Caliper,Notes")] TreaterTest treaterTest, 
            int ctid, int? page = null, string searchstr = null, string daterange = null)
        {
            if (ModelState.IsValid)
            {
                DateTime localtime = treaterTest.TreatmentDateTime;
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                treaterTest.TreatmentDateTime = TimeZoneInfo.ConvertTimeToUtc(localtime, centralZone);
                treaterTest.DryRollNum = treaterTest.DryRollNum?.ToUpper();
                treaterTest.JobNum = treaterTest.JobNum?.ToUpper();
                treaterTest.RollSkidNum = treaterTest.RollSkidNum?.ToUpper();
                treaterTest.Paper_COA = treaterTest.Paper_COA?.ToUpper();
                treaterTest.Resin_COA = treaterTest.Resin_COA?.ToUpper();
                db.Entry(treaterTest).State = EntityState.Modified;
                int labelid = treaterTest.ID;
                await db.SaveChangesAsync();
                if (actiontype == "Save & Label")
                {
                    return RedirectToAction("Index", new { ctid = ctid, page = page, searchstr = searchstr, daterange = daterange, labelid = labelid });
                }
                return RedirectToAction("Index", new { ctid = ctid, page = page, searchstr = searchstr, daterange = daterange, labelid = 0 });
            }
            return View(treaterTest);
        }

        // GET: TreaterTest/Delete/5
        public async Task<ActionResult> Delete(int? id, int ctid, int? page = null, string searchstr = null, string daterange = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreaterTest treaterTest = await db.TreaterTests.FindAsync(id);
            if (treaterTest == null)
            {
                return HttpNotFound();
            }
            else
            {
                DateTime utcTime = treaterTest.TreatmentDateTime;
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                treaterTest.TreatmentDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, centralZone);
            }
            ViewBag.Page = page;
            ViewBag.SearchStr = searchstr;
            ViewBag.DateRange = daterange;
            ViewBag.CurrentTreaterID = ctid;
            return View(treaterTest);
        }

        // POST: TreaterTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id, int ctid, int? page = null, string searchstr = null, string daterange = null)
        {
            TreaterTest treaterTest = await db.TreaterTests.FindAsync(id);
            db.TreaterTests.Remove(treaterTest);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { ctid = ctid, page = page, searchstr = searchstr, daterange = daterange, labelid = 0 });
        }

        // GET: LabelParameters/Create
        public ActionResult LabelParameters(int id, int ctid, int? page = null, string searchstr = null, string daterange = null)
        {
           var model = new LabelParameters();
           var selectedtest = db.TreaterTests.Where(i => i.ID == id).Single();
           model.ID = id;
            model.Quantity = selectedtest.Qty;
            model.UOM = selectedtest.UOM;
            model.Notes = selectedtest.Notes;
            ViewBag.Page = page;
            ViewBag.SearchStr = searchstr;
            ViewBag.DateRange = daterange;
            ViewBag.CurrentTreaterID = ctid;
            return View(model);
        }

        // POST: LabelParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <param name="actiontype"></param>
        /// <param name="labelParameters"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LabelParameters(string actiontype, [Bind(Include = "ID,Quantity,UOM,Notes")] LabelParameters labelParameters)
        {
            if (ModelState.IsValid)
            {
                var viewModel = new LabelData();
                viewModel.labelParameters = labelParameters;
                viewModel.SelectedTest = db.TreaterTests.Include(i => i.AshPart).Where(i => i.ID == labelParameters.ID.Value).Single();
                DateTime DateOldestTest = DateTime.Today.AddDays(-100);
                viewModel.TreaterTests = db.TreaterTests.Include(i => i.AshPart)
                    .Where(s => s.RollSkidNum == viewModel.SelectedTest.RollSkidNum && s.TreatmentDateTime > DateOldestTest);
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                //Get the Treating Spec
                var treatingSpec = db.TreatingSpecs.Where(s => s.TreatingSpecID == viewModel.SelectedTest.AshPart.TreatingSpecID).ToList();

                //Calculate Skid/Roll Averages
                int itemcount = 0;
                float RCF = 0;
                double NRC = 0;
                float Vol = 0;
                double Flow = 0;
                double Gurley = 0;
                double WetOut = 0;
                double Caliper = 0;
                double BW = 0;
                int NonConforming = 0;
                foreach (TreaterTest t in viewModel.TreaterTests)
                {
                    t.TreatmentDateTime = TimeZoneInfo.ConvertTimeFromUtc(t.TreatmentDateTime, centralZone);
                    RCF = RCF + t.ResinContentFiberesin;
                    //if (t.ResinContentFiberesin > treatingSpec.RCF)
                    NRC = NRC + t.NetRC;
                    Vol = Vol + t.Vol;
                    Flow = Flow + t.Flow;
                    Gurley = Gurley + t.GurleyPorosity;
                    WetOut = WetOut + t.WetOut;
                    Caliper = Caliper + t.Caliper;
                    BW = BW + t.BasisWeight;
                    if(Math.Round(t.ResinContentFiberesin*1000)/1000 > treatingSpec[0].RCMax || t.ResinContentFiberesin < treatingSpec[0].RCMin
                        || Math.Round(t.Vol * 1000) / 1000 > treatingSpec[0].VolMax || t.Vol < treatingSpec[0].VolMin
                        || Math.Round((Double)t.Flow,3) > treatingSpec[0].FlowMax || t.Flow < treatingSpec[0].FlowMin)
                    { NonConforming++; }
                    itemcount++;
                }
                viewModel.NonConforming = NonConforming;
                viewModel.AvgRCF = RCF / itemcount;
                viewModel.AvgNRC = NRC / itemcount;
                viewModel.AvgVol = Vol / itemcount;
                viewModel.AvgFlow = Flow / itemcount;
                viewModel.AvgGurley = Gurley / itemcount;
                viewModel.AvgWetOut = WetOut / itemcount;
                viewModel.AvgCaliper = Caliper / itemcount;
                viewModel.AvgBW = BW / itemcount;

                //var report = new ViewAsPdf("CreatePDF", viewModel);
                //return report;
                return View ("CreatePDF", viewModel);

            }
            return View(labelParameters);
        }

        // GET: Label
        public ActionResult CreatePDF(LabelData LD)
        {
            return View(LD);
        }

        public ActionResult ExportToExcel(string searchString, string dateRange)
        {
            var treaterTests = db.TreaterTests
                    .Include(t => t.AshPart)
                    .Include(t => t.AshPart.TreatingSpec)
                    .Include(t => t.TreatingLine);
            treaterTests = treaterTests.OrderByDescending(d => d.TreatmentDateTime);
            if (Session["Treater"] != null)
            {
                var ct = (int)Session["Treater"];
                treaterTests = treaterTests.Where(t => t.TreatingLineID.Equals(ct));
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                treaterTests = treaterTests.Where(s => s.AshPartID.Contains(searchString)
                                       || s.RollSkidNum.Contains(searchString)
                                       || s.JobNum.Contains(searchString));
            }
            var startdate = DateTime.Today;
            var enddate = DateTime.Today.AddDays(1);
            TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            if (!String.IsNullOrEmpty(dateRange))
            {
                startdate = Convert.ToDateTime(dateRange.Split(' ')[0]);
                startdate = TimeZoneInfo.ConvertTimeToUtc(startdate, centralZone);
                enddate = Convert.ToDateTime(dateRange.Substring(dateRange.IndexOf(" - ") + 3)).AddDays(1);
                enddate = TimeZoneInfo.ConvertTimeToUtc(enddate, centralZone);
            }
            treaterTests = treaterTests.Where(s => s.TreatmentDateTime >= startdate
                                        && s.TreatmentDateTime <= enddate);
            treaterTests.ForEach(d => d.TreatmentDateTime = TimeZoneInfo.ConvertTimeFromUtc(d.TreatmentDateTime, centralZone));
            var treaterTestList = new List<TreaterTest>(treaterTests);
            var wb = new XLWorkbook(); //create workbook
            var ws = wb.Worksheets.Add("TL Test"); //add worksheet to workbook

            if (treaterTestList != null && treaterTestList.Count() > 0)
            {
                //insert data to from second row on
                ws.Cell(2, 1).InsertTable(treaterTestList);
            }
            return wb.Deliver("generatedFile.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
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
