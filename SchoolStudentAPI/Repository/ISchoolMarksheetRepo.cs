using SchoolStudentAPI.Dto;
using SchoolStudentAPI.Entities;

namespace SchoolStudentAPI.Repository
{
    public interface ISchoolMarksheetRepo
    {
        List<ViewStudent> GetAllStudents();
        FullStudentDetails GetStudentById(int id);
        int AddStudent(ViewStudent entity);
        int UpdateMarksheet(int id, Students entity);
        List<Students> GetAllMarksheet();
        Students GetMarksheetById(int id);
        int UpdateStudentName(int id, ViewStudent entity);
        int DeleteStudent(int id);
        List<ViewStudent> GetRemovedStudents();
        int RestoreStudent(int id);
        List<Department> GetAllDepartments();
        int DeleteDepartment(int id);
        int AddDepartment(Department entity);
    }
}
