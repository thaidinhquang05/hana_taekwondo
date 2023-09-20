using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
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

        public ApiResponseModel RemoveStudentFromClass(int _studentId, int _classId)
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
            var result = classList.Select((x, index) => new ClassInfoOutput
            {
                Index = index + 1,
                Id = x.Id,
                Name = x.Name,
                Desc = x.Desc,
                StartDate = $"{x.StartDate: yyyy-MM-dd}",
                DueDate = $"{x.DueDate: yyyy-MM-dd}"
            }).ToList();
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

        public List<ClassByDateItem> GetClassesByDate(DateTime date)
        {
            var classes = _classRepository.GetClassesByDate(date);
            var result = classes.Select((x, index) => new ClassByDateItem
                {
                    Index = index + 1,
                    Id = x.Id,
                    ClassName = x.Name,
                    Desc = x.Desc
                })
                .ToList();
            return result;
        }

        public List<StudentAttendanceOutput> GetStudentByClassAndDate(int classId, DateTime date)
        {
            var result = _classRepository.GetStudentByClassAndDate(classId, date);
            return result;
        }

        public ApiResponseModel TakeAttendance(int classId, DateTime date,
            List<StudentAttendanceInput> studentAttendanceInputs)
        {
            try
            {
                _classRepository.TakeAttendance(classId, date, studentAttendanceInputs);
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "take attendance successfully!",
                    IsSuccess = true
                };
            }
            catch
            {
                return new ApiResponseModel
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = "Have something wrong when take attendance!",
                    IsSuccess = false
                };
            }
        }
    }
}