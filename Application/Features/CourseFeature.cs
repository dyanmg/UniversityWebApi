using Application.Repositories;
using Domain.Entities;

namespace Application.Features
{
    public class CourseFeature(IRepository<Course> _courseRepository)
    {
        public async Task<List<Course>> GetAllCourse()
        {
            return await _courseRepository.GetAll();
        }

        public async Task<Course?> GetCourseById(int id)
        {
            return await _courseRepository.GetById(id);
        }

        public async Task<List<Course>> FindByTitle(string name)
        {
            return await _courseRepository.Get(c => c.Title!.Contains(name));
        }
    }
}
