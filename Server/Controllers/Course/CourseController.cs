using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers.Course
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CourseController : ControllerBase
    {
        [HttpGet]
        [EnableQuery]
        //[Authorize]
        public IActionResult GetAllCourse()
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                var data = context.Courses.ToList();
                return Ok(data);

            }
        }
        [HttpGet("CourseCategory")]
        [EnableQuery]
        //[Authorize]
        public IActionResult GetAllCourseCategory()
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                var data = context.CourseCategories.ToList();
                return Ok(data);

            }
        }
        [HttpPost]
        [EnableQuery]
        //[Authorize]
        public IActionResult AddCategory(CourseCategory e)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                context.CourseCategories.Add(e);
                context.SaveChanges();
                return Ok(e);

            }
        }
    }
}
