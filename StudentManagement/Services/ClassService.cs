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
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Add new student successfully!",
                    IsSuccess = true
                };
            }
            catch
            {
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = "Have something wrong when add new student!",
                    IsSuccess = false
                };
            }
        }

        public ApiResponseModel RemoveStudentFromClass(int _studentId,int _classId)
        {
            try
            {
                _classRepository.RemoveStudentFromClass(_studentId, _classId);
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "remove student successfully!",
                    IsSuccess = true
                };
            }
            catch
            {
                return new ApiResponseModel 
                {
                    Code = StatusCodes.Status409Conflict, 
                    Message = "Have something wrong when remove student!", 
                    IsSuccess = false 
                };
            }
        }

        public List<ClassInfoOutput> GetAllClasses()
        {
            var classList = _classRepository.GetAllClasses();
            var result = _mapper.Map<List<ClassInfoOutput>>(classList);
            return result;
        }

        public ClassInfoOutput GetClassById(int classId)
        {
            var classList = _classRepository.GetClassById(classId);
            var result = _mapper.Map<ClassInfoOutput>(classList);
            return result;
        }

        public ApiResponseModel DeleteClass(int classId)
        {
            try
            {
                _classRepository.DeleteClass(classId);
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "remove class successfully!",
                    IsSuccess = true
                };
            }
            catch
            {
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = "Have something wrong when remove class!",
                    IsSuccess = false
                };
            }
        }

        public ApiResponseModel AddNewClass(NewClassInput newClassInput)
        {
            try
            {
                _classRepository.AddNewClass(newClassInput);
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "add class successfully!",
                    IsSuccess = true
                };
            }
            catch
            {
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = "Have something wrong when add class!",
                    IsSuccess = false
                };
            }
        }
    }
}
