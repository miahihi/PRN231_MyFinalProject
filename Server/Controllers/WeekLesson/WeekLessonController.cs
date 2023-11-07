using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Server.Models;

namespace Server.Controllers.WeekLesson
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekLessonController : ControllerBase
    {
        private IConfiguration _config;

        public WeekLessonController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        [EnableQuery]
        //[Authorize]
        public IActionResult GetWeekLesson()
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                var data = context.WeekLessons.ToList();
                return Ok(data);

            }
        }
    }
}
