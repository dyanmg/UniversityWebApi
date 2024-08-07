using Application.Features;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(CourseFeature _courseFeature) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCourse()
        {
            var result = await _courseFeature.GetAllCourse();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var result = await _courseFeature.GetCourseById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCourse(string title)
        {
            var result = await _courseFeature.FindByTitle(title);
            return Ok(result);
        }
    }
}
