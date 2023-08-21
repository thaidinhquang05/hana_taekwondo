using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<NewTuitionInput, Tuition>()
            .ForMember(des => des.CreatedAt,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(des => des.ModifiedAt,
                opt => opt.MapFrom(src => DateTime.Now));;
        
        CreateMap<NewStudentInput, Student>()
            .ForMember(des => des.CreatedAt,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(des => des.ModifiedAt,
                opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<Student, StudentInfoOutput>()
            .ForMember(des => des.Dob,
                opt => opt.MapFrom(src => $"{src.Dob:yyyy-MM-dd}"))
            .ForMember(des => des.Gender,
                opt => opt.MapFrom(src => src.Gender ? "Male" : "Female"));
        
        CreateMap<Student, StudentOutput>()
            .ForMember(des => des.Dob,
                opt => opt.MapFrom(src => $"{src.Dob:yyyy-MM-dd}"))
            .ForMember(des => des.Gender,
                opt => opt.MapFrom(src => src.Gender ? "Male" : "Female"));
    }
}