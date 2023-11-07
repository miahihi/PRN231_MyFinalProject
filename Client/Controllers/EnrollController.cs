using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Client.Controllers
{
    public class EnrollController : Controller
    {
        private readonly HttpClient _client;

        public EnrollController()
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
                if (courseById.Count>0)
                {
                    c = courseById[0];
                }
            }
            if (c != null)
            {
                HttpResponseMessage response = _client.GetAsync("https://localhost:7078/api/Course/CourseCategory?$filter=categoryId eq " + c.CategoryId).Result;
                if(response.IsSuccessStatusCode)
                {
                    string dt = response.Content.ReadAsStringAsync().Result;
                    List<CourseCategory> ctg = JsonConvert.DeserializeObject<List<CourseCategory>>(dt);
                    ViewData["CategoryName"] = ctg[0].CategoryName;
                }
            }
            return View(c);
        }
        [HttpPost]
        public IActionResult SubmitEnroll(string courseId)
        {
            string uid = HttpContext.Request.Cookies["loginId"];

            //Course c1 = null;
            //User u1 = null;
            //List<Course> courses = null;
            //HttpResponseMessage response1 = _client.GetAsync("https://localhost:7078/api/Course?$filter=courseId eq " + courseId).Result;
            //if (response1.IsSuccessStatusCode)
            //{
            //    string c = response1.Content.ReadAsStringAsync().Result;
            //    courses = JsonConvert.DeserializeObject<List<Course>>(c);
            //    if (courses.Count > 0)
            //    {
            //        c1 = courses[0];
            //    }
            //}

            //List<User> users = null;
            //HttpResponseMessage response2 = _client.GetAsync("https://localhost:7078/api/Course?$filter=courseId eq " + courseId).Result;
            //if (response2.IsSuccessStatusCode)
            //{
            //    string u = response2.Content.ReadAsStringAsync().Result;
            //    users = JsonConvert.DeserializeObject<List<User>>(u);
            //    if (users.Count > 0)
            //    {
            //        u1 = users[0];
            //    }
            //}

            Enrollment enrollment = new Enrollment();
            enrollment.EnrollTime = DateTime.Now;
            enrollment.CourseId = int.Parse(courseId);
            enrollment.UserId = int.Parse(uid);
            //enrollment.Course = c1;
            //enrollment.User = u1;

            string data = JsonConvert.SerializeObject(enrollment);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync("https://localhost:7078/api/Enroll", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "WeekLesson", new { id = courseId });
            }
            return View();
        }
    }
}
