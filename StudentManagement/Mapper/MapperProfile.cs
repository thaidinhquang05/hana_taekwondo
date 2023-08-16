using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.Models;

namespace StudentManagement.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<TimetableInput, Timetable>();
        
        CreateMap<NewStudentInput, Student>()
            .ForMember(des => des.CreatedAt,
                opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(des => des.ModifiedAt,
                opt => opt.MapFrom(src => DateTime.Now));
    }
}