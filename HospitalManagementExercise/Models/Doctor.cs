using MSSTU.DB.Utility;

namespace HospitalManagementExercise.Models
{
    public class Doctor : Entity
    {
        public string DoctorName { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Residence { get; set; }
        public string Unit { get; set; }
        public bool DepartmentChief { get; set; }
        public int RecoveredPatients { get; set; }
        public int TotalDeaths { get; set; }
        public Hospital Hospital { get; set; }


        
    }
}
