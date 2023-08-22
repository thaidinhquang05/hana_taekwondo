using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public ClassService(IClassRepository classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }

        public ApiResponseModel AddNewStudentToClass(List<int> _studentId, int _classId)
        {
            try
            {
                _classRepository.AddStudentToClass(_studentId, _classId);
            }
            catch
            {
                new Exception("Have something wrong when add new student!");
            }

            return new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Add new student successfully!",
                IsSuccess = true
            };
        }

        public ApiResponseModel RemoveStudentFromClass(Student _student)
        {
            try
            {
                _classRepository.RemoveStudentFromClass(_student);
            }
            catch
            {
                new Exception("Have something wrong when remove new student!");
            }

            return new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "remove student successfully!",
                Data = _student,
                IsSuccess = true
            };
        }

        public List<ClassInfoOutput> GetAllClasses()
        {
            var classList = _classRepository.GetAllClasses();
            var result = _mapper.Map<List<ClassInfoOutput>>(classList);
            return result;
        }

    }
}
