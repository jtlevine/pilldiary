using PillDiary.Models;
using PillDiary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PillDiary.Controllers
{
    public class PillDiaryController : Controller
    {
        // GET: PillDiary
        public async Task<ActionResult> Index()
        {
            //IPillRepository repo = new PillRepository();
            //var medata = repo.GetPatientMedications("12345678");
            //var reporting = repo.GetPatientPillReporting("12345678");
            //return View(new Patient() { MedicationInfos = medata, PillReportings = reporting });
            var model = await GetFullAndPartialViewModel();
            return View(model);
        }
        //public ActionResult PillDiary()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Insert(PillReporting reporting)
        {
            IPillRepository repo = new PillRepository();
            try
            {
               
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
        private async Task<Patient> GetFullAndPartialViewModel()
        {
            IPillRepository repo = new PillRepository();
            var medata = repo.GetPatientMedications("12345678");
            var reporting = repo.GetPatientPillReporting("12345678");
            return new Patient() { MedicationInfos = medata, PillReportings = reporting };
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public async Task<ActionResult> GetPillReporting()
        {
            var model = await this.GetFullAndPartialViewModel();
            return PartialView("_pillHistory", model);
            // IPillRepository repo = new PillRepository();
            // var medata = repo.GetPatientMedications("12345678");
            // var reportingList = repo.GetPatientPillReporting("12345678");
            //// return View();
            // return PartialView("_pillHistory", new Patient() { MedicationInfos = medata, PillReportings = reportingList });
        }

    }
}