using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class WeekLessonController : Controller
    {
        private readonly HttpClient _client;

        public WeekLessonController()
        {
            _client = new HttpClient();
        }
        public IActionResult Index(int id)
        {
            List<Course> courseById = new List<Course>();
            Course c = null;
            HttpResponseMessage response1 = _client.GetAsync("https://localhost:7078/api/Course?$filter=courseId eq " + id).Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                courseById = JsonConvert.DeserializeObject<List<Course>>(data);
                if (courseById.Count > 0)
                {
                    c = courseById[0];
                }
            }
            if (c != null)
            {
                HttpResponseMessage response = _client.GetAsync("https://localhost:7078/api/WeekLesson?$filter=courseId eq " + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string dt = response.Content.ReadAsStringAsync().Result;
                    List<WeekLesson> ctg = JsonConvert.DeserializeObject<List<WeekLesson>>(dt);
                    ViewBag.weeklesson = ctg;
                }
            }
            return View(c);
        }
        public IActionResult Unenroll(int courseId)
        {
            string uid = HttpContext.Request.Cookies["loginId"];
            HttpResponseMessage response1 = _client.DeleteAsync("https://localhost:7078/api/Enroll?uid="+uid+"&cid="+ courseId).Result;
            if (response1.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Course");
            }
           
            return RedirectToAction("Index");
        }
    }
}
