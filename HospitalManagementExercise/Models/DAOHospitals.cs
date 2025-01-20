using MSSTU.DB.Utility;
using System.Diagnostics.Eventing.Reader;

namespace HospitalManagementExercise.Models
{
    public class DAOHospitals : IDAO
    {
        #region Singleton
        private Database db;
        private DAOHospitals()
        {
            db = new Database("Hospital", "ArtuzPC");
        }
        private static DAOHospitals instance;

        public static DAOHospitals GetInstance()
        {
            return instance ??= new DAOHospitals();
        }
        #endregion

        #region CRUD
        public bool CreateRecord(Entity entity)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@LocationName",((Hospital)entity).LocationName.Replace("'", "''")},
                {"@HospitalName",((Hospital)entity).HospitalName.Replace("'", "''")},
                {"@IsPublic",(((Hospital)entity).IsPublic?"1":"0")}
            };
            const string query = "INSERT INTO Hospitals (locationName,hospitalName,isPublic) " +
                                 "VALUES (@LocationName,@HospitalName,@IsPublic)";

            return db.UpdateDb(query, parameters);
        }

        public bool DeleteRecord(int recordId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@Id",recordId}
            };

            const string query = "DELETE FROM Hospitals WHERE id = @Id";
            return db.UpdateDb(query, parameters);
        }

        public Entity? FindRecord(int recordId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@Id", recordId}
            };

            const string query = "SELECT * FROM Hospitals WHERE id = @Id";

            var res = db.ReadOneDb(query, parameters);
            if (res == null)
            {
                return null;
            }
            else
            {
                Entity Hospital = new Hospital();
                Hospital.TypeSort(res);

                return Hospital;
            }

                
        }

        public List<Entity> GetRecords()
        {
            const string query = "SELECT * FROM Hospitals";
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
                    Entity hospital = new Hospital();
                    hospital.TypeSort(record);

                    entities.Add(hospital);
                }
            }
            return entities;
        }

        public bool UpdateRecord(Entity entity)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@Id", entity.Id},
                {"@LocationName", ((Hospital)entity).LocationName.Replace("'","''")},
                {"@HospitalName", ((Hospital)entity).HospitalName.Replace("'","''")},
                {"@IsPublic", (((Hospital)entity).IsPublic?"1":"0")}
            };

            const string query = "UPDATE Hospitals SET " +
                "locationName = @LocationName" +
                "hospitalName = @Hospitalname" +
                "isPublic = @IsPublic" +
                "WHERE id = @Id";

            return db.UpdateDb(query, parameters);
        }

        #endregion
    }
}
