using AutoMapper;
using SchoolStudentAPI.Dto;
using SchoolStudentAPI.Entities;

namespace SchoolStudentAPI.Mapping
{
    public class StudentProfile : Profile

    {
        public StudentProfile()
        {
            CreateMap<AddStudentDTO, ViewStudent>();

            CreateMap<UpdateMarksheetDTO, Students>()
                .ForMember(dest => dest.Student_Id, opt => opt.Ignore())
                .ForMember(dest => dest.Student_Name, opt => opt.Ignore())
                .ForMember(dest => dest.Total_Marks, opt => opt.Ignore());

            CreateMap<UpdateStudentName, ViewStudent>()
                .ForMember(dest => dest.Student_Id, opt => opt.Ignore());

            CreateMap<AddDepartment, Department>()
                .ForMember(dest => dest.Dept_Id, opt => opt.Ignore());
        }
    }
}
