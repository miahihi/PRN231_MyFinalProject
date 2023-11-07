using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Server.Models;

namespace Server.Controllers.Enrollments
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollController : ControllerBase
    {
        [HttpGet]
        [EnableQuery]
        //[Authorize]
        public IActionResult GetEnrollment()
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                var data = context.Enrollments.ToList();
                return Ok(data);

            }
        }

        [HttpPost]
        [EnableQuery]
        //[Authorize]
        public IActionResult SubmitEnroll(Enrollment e)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                Enrollment data = context.Enrollments.FirstOrDefault(c => c.CourseId == e.CourseId && c.UserId == e.UserId);
                if (data==null)
                {
                    //data = new Enrollment();
                    //data.CourseId = e.CourseId;
                    //data.UserId=e.UserId;
                    //data.EnrollTime = DateTime.Now;
                    context.Enrollments.Add(e);
                    context.SaveChanges();
                    return Ok(e);
                }
                return BadRequest();
            }
        }
        [HttpDelete]
        [EnableQuery]
        //[Authorize]
        public IActionResult Unenroll(int uid, int cid)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                Enrollment data = context.Enrollments.FirstOrDefault(c => c.CourseId == cid && c.UserId == uid);
                if (data == null)
                {
                    return NotFound();
                }
                context.Remove(data);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
