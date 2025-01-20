using MSSTU.DB.Utility;
using System.ComponentModel;

namespace HospitalManagementExercise.Models
{
    public class DAODoctors : IDAO
    {
        #region Singleton
        private Database db;
        private DAODoctors()
        {
            db = new Database("Hospital", "ArtuzPC");
        }
        private static DAODoctors instance;

        public static DAODoctors GetInstance()
        {
            return instance ??= new DAODoctors();
        }
        #endregion

        #region CRUD
        public bool CreateRecord(Entity entity)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "@DoctorName", ((Doctor)entity).DoctorName.Replace("'", "''") },
                    { "@Surname", ((Doctor)entity).Surname.Replace("'", "''") },
                    { "@DateOfBirth", ((Doctor)entity).DateOfBirth },
                    { "@Residence", ((Doctor)entity).Residence.Replace("'","''") },
                    { "@Unit", ((Doctor)entity).Unit.Replace("'","''") },
                    { "@DepartmentChief", (((Doctor)entity).DepartmentChief?"1":"0") },
                    { "@RecoveredPatients", ((Doctor)entity).RecoveredPatients},
                    { "@TotalDeaths", ((Doctor)entity).TotalDeaths},
                    { "@Hospital", ((Doctor)entity).Hospital.Id}
                };

            const string query = "INSERT INTO Doctors" +
                                 "(doctorName, surname, dateOfBirth, residence, unit, departmentChief, recoveredPatients, totalDeaths, idHospital) " +
                                 "VALUES" +
                                 "(@DoctorName, @Surname, @DateOfBirth, @Residence, @Unit, @DepartmentChief, @RecoveredPatients, @TotalDeaths, @Hospital)";

            return db.UpdateDb(query, parameters);
        }

        public bool DeleteRecord(int recordId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@Id", recordId }
            };

            const string query = "DELETE FROM Doctors WHERE id = @Id";

            return db.UpdateDb(query, parameters);
            
        }

        public Entity? FindRecord(int recordId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@Id", recordId }
            };

            const string query = "SELECT * FROM Doctors WHERE id = @Id";

            var res = db.ReadOneDb(query, parameters);
            if (res == null)
            {
                return null;
            }
            else
            {
                Entity doctor = new Doctor();
                doctor.TypeSort(res);

                return doctor;
            }

        }

        public List<Entity> GetRecords()
        {
            const string query = "SELECT * FROM Doctors";
            List<Entity> entities = new List<Entity>();
            var records = db.ReadDb(query);
            if (records == null)
            {
                return entities;
            }
            else
            {
                foreach (var record in records)
                {
                    Entity doctor = new Doctor();
                    doctor.TypeSort(record);

                    entities.Add(doctor);
                }
            }

            return entities;

        }

        public bool UpdateRecord(Entity entity)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "@DoctorName", ((Doctor)entity).DoctorName.Replace("'", "''") },
                    { "@Surname", ((Doctor)entity).Surname.Replace("'", "''") },
                    { "@DateOfBirth", ((Doctor)entity).DateOfBirth },
                    { "@Residence", ((Doctor)entity).Residence.Replace("'","''") },
                    { "@Unit", ((Doctor)entity).Unit.Replace("'","''") },
                    { "@DepartmentChief", (((Doctor)entity).DepartmentChief?"1":"0") },
                    { "@RecoveredPatients", ((Doctor)entity).RecoveredPatients},
                    { "@TotalDeaths", ((Doctor)entity).TotalDeaths},
                    { "@Hospital", ((Doctor)entity).Hospital.Id},
                    { "@Id", entity.Id}
                };

            const string query = "UPDATE Doctors SET " +
                                 "doctorName = @DoctorName, " +
                                 "surname = @Surname, " +
                                 "dateOfBirth = @DateOfBirth, " +
                                 "residence = @Residence, " +
                                 "unit = @Unit, " +
                                 "departmentChief = @DepartmentChief, " +
                                 "recoveredPatients = @RecoveredPatients, " +
                                 "totalDeaths = @TotalDeaths, " +
                                 "idHospital = @Hospital " +
                                 "WHERE id = @Id";

            return db.UpdateDb(query, parameters);
        }

        #endregion
    }
}
