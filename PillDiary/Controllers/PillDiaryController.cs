using PillDiary.Models;
using PillDiary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PillDiary.Controllers
{
    public class PillDiaryController : Controller
    {
        // GET: PillDiary
        public ActionResult Index()
        {
            IPillRepository repo = new PillRepository();
            var medata = repo.GetPatientMedications("12345678");
            var reporting = repo.GetPatientPillReporting("12345678");
            return View(new Patient() { MedicationInfos = medata, PillReportings = reporting });
        }
        //public ActionResult PillDiary()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Insert(PillReporting reporting)
        {
            try
            {
                IPillRepository repo = new PillRepository();
                if (reporting.PillTimeStamp.HasValue)
                    reporting.PillTimeStamp = DateTime.Now.Date + reporting.PillTimeStamp.Value.TimeOfDay;
                repo.InsertPill(reporting);
            }
            catch (Exception ex)
            {
                //Log errror
            }
            return Json(new { status = "success" });
        }
    }
}