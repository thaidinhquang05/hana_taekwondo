using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<TimetableInput, Timetable>()
            .ForMember(des => des.Id, 
                opt => opt.MapFrom(src => src.TimetableId));

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

        CreateMap<Student, StudentInfoOutput>();
    }
}