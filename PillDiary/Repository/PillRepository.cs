using NPoco;
using PillDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillDiary.Repository
{
    public interface IPillRepository
    {
        List<PatientMedication> GetPatientMedications(string MRN);
        List<PillReporting> GetPatientPillReporting(string MRN);
        void InsertPill(PillReporting pillDiary);
    }
    public class PillRepository : IPillRepository
    {
        public List<PatientMedication> GetPatientMedications(string MRN)
        {
            string location = string.Empty;
            IDatabase db = new Database("PillDiaryConn");

            //var result = db.Query<PatientMedication>(string.Format("select m.* from PatientMedication m INNER JOIN Patient p ON m.PatientId=p.PatientId  where  mrn='{0}'", MRN));
            var result = db.Fetch<PatientMedication>(string.Format("select m.*,i.* from PatientMedication m INNER JOIN Patient p ON m.PatientId=p.PatientId inner join MedicationInfo i on m.MedicationId=i.Id  where  mrn='{0}'", MRN));


            return result;
        }
        public List<PillReporting> GetPatientPillReporting(string MRN)
        {
            string location = string.Empty;
            IDatabase db = new Database("PillDiaryConn");

            //var result = db.Query<PatientMedication>(string.Format("select m.* from PatientMedication m INNER JOIN Patient p ON m.PatientId=p.PatientId  where  mrn='{0}'", MRN));
            var result = db.Fetch<PillReporting>(string.Format("  select r.*, i.* from PatientMedication m INNER JOIN Patient p ON m.PatientId=p.PatientId inner join MedicationInfo i on m.MedicationId=i.Id inner join[PillReporting] r on p.PatientId = r.patientid and m.MedicationId = r.medicationid  where  mrn='{0}' order by pilltimestamp desc", MRN));


            return result;
        }
        public void InsertPill(PillReporting pillDiary)
        {
            IDatabase db = new Database("PillDiaryConn");
            var result = db.Fetch<PillReporting>(string.Format("  select r.*, i.* from PatientMedication m INNER JOIN Patient p ON m.PatientId=p.PatientId inner join MedicationInfo i on m.MedicationId=i.Id inner join[PillReporting] r on p.PatientId = r.patientid and m.MedicationId = r.medicationid  where  p.patientid='{0}' and m.medicationid='{1}' order by pilltimestamp desc", pillDiary.PatientId, pillDiary.MedicationId));
            if (result?.Count > 0)
            {
                string str = "update PillReporting set pilltimestamp='" + pillDiary.PillTimeStamp + "' where patientid=" + pillDiary.PatientId + " and medicationid=" + pillDiary.MedicationId;
                db.Execute(str);
                //UpdatePill(pillDiary);
            }
            else
            {
                string str = "INSERT INTO PillReporting VALUES("+ pillDiary.MedicationId + ","+ pillDiary.PatientId + ","+Convert.ToInt16( pillDiary.PillStatus.Value)+", '" + pillDiary.PillTimeStamp + "')" ;
                db.Execute(str);
                //db.Insert(pillDiary);
            }


        }
        public void UpdatePill(PillReporting pillDiary)
        {
            IDatabase db = new Database("PillDiaryConn");
            db.Update(pillDiary);

        }
    }
}