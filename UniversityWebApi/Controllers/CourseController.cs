﻿using Application.Features;
using Domain.DTO;
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

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto createDto)
        {
            await _courseFeature.CreateCourse(createDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseFeature.RemoveCourseById(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CourseCreateDto courseDto)
        {
            var course = await _courseFeature.UpdateCourseById(id, courseDto);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }
    }
}
