using MSSTU.DB.Utility;

namespace HospitalManagementExercise.Models
{
    public class Hospital : Entity
    {
        public string LocationName { get; set; }
        public string HospitalName { get; set; }
        public bool IsPublic { get; set; }
    }
}
