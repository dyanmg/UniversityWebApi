using Application.Repositories;
using Domain.DTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Features
{
    public class CourseFeature(IRepository<Course> _courseRepository,
        IUnitOfWork _unitOfWork,
        ILogger<CourseFeature> _logger)
    {
        public async Task<List<Course>> GetAllCourse()
        {
            return await _courseRepository.GetAll();
        }

        public async Task<Course?> GetCourseById(int id)
        {
            var result = await _courseRepository.GetById(id);

            if (result == null)
            {
                _logger.LogWarning("Nonexistent course id queried!");
            }

            return result;
        }

        public async Task<List<Course>> FindByTitle(string name)
        {
            return await _courseRepository.Get(c => c.Title!.Contains(name));
        }

        public async Task CreateCourse(CourseCreateDto createDto)
        {
            var course = new Course
            {
                Title = createDto.Title,
                Credits = createDto.Credits,
            };

            await _courseRepository.Add(course);
            await _unitOfWork.SaveChanges();
        }

        public async Task<bool> RemoveCourseById(int courseId)
        {
            var course = await _courseRepository.GetById(courseId);

            if (course == null)
            {
                return false;
            }

            await _courseRepository.Remove(course);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<Course?> UpdateCourseById(int courseId, CourseCreateDto courseDto)
        {
            var course = await _courseRepository.GetById(courseId);

            if (course == null)
            {
                return null;
            }

            course.Title = courseDto.Title;
            course.Credits = courseDto.Credits;

            await _courseRepository.Update(course);
            await _unitOfWork.SaveChanges();
            return course;
        }
    }
}
