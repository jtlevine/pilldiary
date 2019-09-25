using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillDiary.Models
{
  
    [TableName("Patient")]
    [PrimaryKey("PatientId")]
    public class Patient
    {
        public int PatientId { get; set; }
        public string MRN { get; set; }
        public String PatientName { get; set; }
        public List<PatientMedication> MedicationInfos { get; set; }
        public List<PillReporting> PillReportings { get; set; }
    }

    [TableName("PatientMedication")]
    public class PatientMedication
    {
        public int PatientId { get; set; }
        public int MedicationId { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan AMDosageTime { get; set; }
        public TimeSpan PMDosageTime { get; set; }
        public MedicationInfo MedicationInfo { get; set; }
    }
    [TableName("MedicationInfo")]
    [PrimaryKey("Id")]
    public class MedicationInfo
    {
        public int Id { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string AdditionalInformation { get; set; }

    }
    [TableName("PillReporting")]
    public class PillReporting
    {
        public int PatientId { get; set; }
        public int MedicationId { get; set; }
        public DateTime? PillTimeStamp { get; set; }
        public bool? PillStatus { get; set; }
        //[Ignore]
        public MedicationInfo MedicationInfo { get; set; }
    }
}