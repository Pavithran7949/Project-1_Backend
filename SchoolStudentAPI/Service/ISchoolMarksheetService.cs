using SchoolStudentAPI.Dto;
using SchoolStudentAPI.Entities;

namespace SchoolStudentAPI.Service
{
    public interface ISchoolMarksheetService
    {
        List<ViewStudent> GetAllStudents();
        FullStudentDetails GetStudentById(int id);
        int AddStudent(AddStudentDTO dto);
        (int result, int total) UpdateMarksheet(int id, UpdateMarksheetDTO dto);
        List<Students> GetAllMarksheet();
        Students GetMarksheetById(int id);
        int UpdateStudentName(int id, UpdateStudentName dto);
        int DeleteStudent(int id);
        List<ViewStudent> GetRemovedStudents();
        int RestoreStudent(int id);
        List<Department> GetAllDepartments();
        int DeleteDepartment(int id);
        int AddDepartment(AddDepartment dto);
    }
}
