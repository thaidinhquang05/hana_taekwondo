using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface ITuitionRepository
{
    int AddNewTuition(Tuition tuition);
}