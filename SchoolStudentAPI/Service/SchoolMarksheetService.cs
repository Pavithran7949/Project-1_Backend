using AutoMapper;
using SchoolStudentAPI.Dto;
using SchoolStudentAPI.Entities;
using SchoolStudentAPI.Repository;

namespace SchoolStudentAPI.Service
{
    public class SchoolMarksheetService : ISchoolMarksheetService
    {
        private readonly ISchoolMarksheetRepo _schoolmarksheetrepo;
        private readonly IMapper _mapper;

        public SchoolMarksheetService(ISchoolMarksheetRepo schoolmarksheetrepo, IMapper mapper)
        {
            _schoolmarksheetrepo = schoolmarksheetrepo;
            _mapper = mapper;
        }

        public List<ViewStudent> GetAllStudents()
        {
            return _schoolmarksheetrepo.GetAllStudents();
        }

        public int AddStudent(AddStudentDTO dto)
        {
            ViewStudent entity = _mapper.Map<ViewStudent>(dto);
            int result = _schoolmarksheetrepo.AddStudent(entity);
            return result;
        }

        public int UpdateStudentName(int id, UpdateStudentName dto)
        {
            ViewStudent entity = _mapper.Map<ViewStudent>(dto);
            int result = _schoolmarksheetrepo.UpdateStudentName(id, entity);
            return result;
        }

        public (int result, int total) UpdateMarksheet(int id, UpdateMarksheetDTO dto)
        {
            Students entity = _mapper.Map<Students>(dto);
            int result = _schoolmarksheetrepo.UpdateMarksheet(id, entity);
            int totalMarks = dto.Maths + dto.Science + dto.English + dto.Social + dto.Tamil;
            return (result,totalMarks);
        }

        public List<Students> GetAllMarksheet()
        {
            return _schoolmarksheetrepo.GetAllMarksheet();
        }

        public Students GetMarksheetById(int id)
        {
            return _schoolmarksheetrepo.GetMarksheetById(id);
        }

        public int DeleteStudent(int id)
        {
            return _schoolmarksheetrepo.DeleteStudent(id);
        }

        public List<ViewStudent> GetRemovedStudents()
        {
            return _schoolmarksheetrepo.GetRemovedStudents();
        }

        public int RestoreStudent(int id)
        {
            return _schoolmarksheetrepo.RestoreStudent(id);
        }

        public List<Department> GetAllDepartments()
        {
            return _schoolmarksheetrepo.GetAllDepartments();
        }

        public int DeleteDepartment(int id)
        {
            return _schoolmarksheetrepo.DeleteDepartment(id);
        }

        public int AddDepartment(AddDepartment dto)
        {
            Department entity = _mapper.Map<Department>(dto);
            int result = _schoolmarksheetrepo.AddDepartment(entity);
            return result;
        }

        public FullStudentDetails GetStudentById(int id)
        {
            return _schoolmarksheetrepo.GetStudentById(id);
        }
    }
}
