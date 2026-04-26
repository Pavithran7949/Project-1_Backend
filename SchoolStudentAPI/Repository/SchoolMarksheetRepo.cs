using Dapper;
using Microsoft.Data.SqlClient;
using SchoolStudentAPI.Dto;
using SchoolStudentAPI.Entities;
using SchoolStudentAPI.Helper;

namespace SchoolStudentAPI.Repository
{
    public class SchoolMarksheetRepo : ISchoolMarksheetRepo
    {
        private readonly TestDbContext _dbcontext;

        public SchoolMarksheetRepo(TestDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<ViewStudent> GetAllStudents()
        {
            List<ViewStudent> studentsList = new List<ViewStudent>();

            using SqlConnection con = _dbcontext.GetConnection();

            string query = @"
SELECT 
    s.Student_Id, 
    s.Student_Name,
    s.Address,
    s.Gender,
    s.Blood_Group,
    s.Mobile_Number,
    d.Dept_Name as Dept
FROM StudentMarksheet s
FULL JOIN Department_Details d ON d.Dept_Id = s.Dept_Id
WHERE s.IsActive = 1";

            return con.Query<ViewStudent>(query).ToList();
        }

        public int AddStudent(ViewStudent entity)
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = @"
        INSERT INTO StudentMarksheet (Student_Name, Dept_Id, IsActive, Address, Gender, Blood_Group, Mobile_Number)
        VALUES (@Student_Name, @Dept_Id, 1, @Address, @Gender, @Blood_Group, @Mobile_Number);

        SELECT CAST(SCOPE_IDENTITY() AS INT);
    ";
            int newId = con.QuerySingle<int>(query, entity);

            return newId;
        }

        public int UpdateStudentName(int id, ViewStudent entity)
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = @"
UPDATE StudentMarksheet
SET Student_Name = @Student_Name, Dept_Id = @Dept_Id, Address = @Address, Gender = @Gender, Blood_Group = @Blood_Group, Mobile_Number = @Mobile_Number
WHERE Student_Id = @Student_Id";

            return con.Execute(query, new
            {
                Student_Id = id,
                Student_Name = entity.Student_Name,
                Dept_Id = entity.Dept_Id,
                Address = entity.Address,
                Gender = entity.Gender,
                Blood_Group = entity.Blood_Group,
                Mobile_Number = entity.Mobile_Number
            });


        }


        public int UpdateMarksheet(int id, Students entity)
        {
            using SqlConnection con = _dbcontext.GetConnection();

            entity.Student_Id = id;

            entity.Total_Marks = entity.Maths + entity.Science + entity.English + entity.Social + entity.Tamil;

            string query = @"
    UPDATE StudentMarksheet 
    SET Maths = @Maths,
        Science = @Science,
        English = @English,
        Social = @Social,
        Tamil = @Tamil
    WHERE Student_Id = @Student_Id and IsActive = 1";

            return con.Execute(query, entity);
        }

        public List<Students> GetAllMarksheet()
        {
            List<Students> marksheetList = new List<Students>();

            using SqlConnection con = _dbcontext.GetConnection();

            string query = "SELECT s.Student_Id, s.Student_Name, s.Maths, s.English, s.Tamil, s.Social, s.Science, s.Total_Marks, d.Dept_Name as Dept FROM StudentMarksheet s join Department_Details d on s.Dept_Id = d.Dept_Id where IsActive = 1";

            return con.Query<Students>(query).ToList();
        }

        public Students GetMarksheetById(int id)
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = "Select * from StudentMarksheet where Student_Id = @Student_Id and IsActive = 1";

            return con.QueryFirstOrDefault<Students>(query, new { Student_Id = id });
        }

        public int DeleteStudent(int id)
        {
            using SqlConnection con = _dbcontext.GetConnection();
            string query = "update StudentMarksheet set IsActive = 0 WHERE Student_Id = @Student_Id and IsActive = 1";
            return con.Execute(query, new { Student_Id = id });
        }

        public List<ViewStudent> GetRemovedStudents()
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = "SELECT s.Student_Id, s.Student_Name, d.Dept_Name as Dept FROM StudentMarksheet s join Department_Details d on s.Dept_Id = d.Dept_Id where IsActive = 0";

            return con.Query<ViewStudent>(query).ToList();
        }

        public int RestoreStudent(int id)
        {
            using SqlConnection con = _dbcontext.GetConnection();
            string query = "update StudentMarksheet set IsActive = 1 WHERE Student_Id = @Student_Id and IsActive = 0";
            return con.Execute(query, new { Student_Id = id });
        }

        public List<Department> GetAllDepartments()
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = "SELECT * FROM Department_Details";

            return con.Query<Department>(query).ToList();
        }

        public int DeleteDepartment(int id)
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = "DELETE FROM Department_Details WHERE Dept_Id = @Dept_Id";

            return con.Execute(query, new { Dept_Id = id });
        }

        public int AddDepartment(Department entity)
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = @"INSERT INTO Department_Details (Dept_Code, Dept_Name)
        VALUES (@Dept_Code, @Dept_Name)
SELECT CAST(SCOPE_IDENTITY() AS INT);
";

            int newId = con.QuerySingle<int>(query, entity);

            return newId;
        }

        public FullStudentDetails GetStudentById(int id)
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = @"
        SELECT 
            s.Student_Id,
            s.Student_Name,
            s.Dept_Id,
            d.Dept_Name AS Dept,
            s.Address,
            s.Gender,
            s.Blood_Group,
            s.Mobile_Number
        FROM StudentMarksheet s
        INNER JOIN Department_Details d ON s.Dept_Id = d.Dept_Id
        WHERE s.Student_Id = @Id AND s.IsActive = 1";

            return con.QueryFirstOrDefault<FullStudentDetails>(query,new { Id = id });
        }
    }
}
