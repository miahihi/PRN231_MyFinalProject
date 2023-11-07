using Client.Models;
using Client.Paging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Xml.Linq;

namespace Client.Controllers
{
    public class CourseController : Controller
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;


        public CourseController(IConfiguration configuration)
        {
            _client = new HttpClient(); 
            _configuration = configuration;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Course> cs { get; set; }


        //[HttpGet]
        //public async Task<IActionResult> IndexAsync(int? pageIndex)
        //{
        //    string token = HttpContext.Request.Cookies["token"];
        //    string uid = HttpContext.Request.Cookies["loginId"];
        //    List<Course> courseList = new List<Course>();
        //    List<CourseCategory> categories = new List<CourseCategory>();
        //    List<Enrollment> e = new List<Enrollment>();
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    HttpResponseMessage response1 = _client.GetAsync("https://localhost:7078/api/Course").Result;
        //    HttpResponseMessage response2 = _client.GetAsync("https://localhost:7078/api/Course/CourseCategory").Result;
        //    HttpResponseMessage response3 = _client.GetAsync("https://localhost:7078/api/Enroll?$filter=userId eq "+uid).Result;
        //    if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
        //    {
        //        string data1 = response1.Content.ReadAsStringAsync().Result;
        //        courseList = JsonConvert.DeserializeObject<List<Course>>(data1);
        //        string data2 = response2.Content.ReadAsStringAsync().Result;
        //        categories = JsonConvert.DeserializeObject<List<CourseCategory>>(data2);
        //        string data3 = response3.Content.ReadAsStringAsync().Result;
        //        e = JsonConvert.DeserializeObject<List<Enrollment>>(data3);
        //        ViewBag.course = courseList;
        //        ViewBag.categories = categories;
        //    }
        //    List<Course> a = new List<Course>();
        //    if (e.Count!=0)
        //    {
        //        foreach (var item in e)
        //        {
        //            foreach (var cou in courseList)
        //            {
        //                if (item.CourseId == cou.CourseId)
        //                {
        //                    a.Add(cou);
        //                }
        //            }
        //        }
        //    }
        //    ViewBag.enrollList = a;
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> IndexAsync(int? pageIndex)
        {
            string token = HttpContext.Request.Cookies["token"];
            string uid = HttpContext.Request.Cookies["loginId"];
            List<Course> courseList = new List<Course>();
            List<CourseCategory> categories = new List<CourseCategory>();
            List<Enrollment> enrollments = new List<Enrollment>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Fetch courses and categories
            HttpResponseMessage response1 = _client.GetAsync("https://localhost:7078/api/Course").Result;
            HttpResponseMessage response2 = _client.GetAsync("https://localhost:7078/api/Course/CourseCategory").Result;

            if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                string data1 = response1.Content.ReadAsStringAsync().Result;
                courseList = JsonConvert.DeserializeObject<List<Course>>(data1);

                string data2 = response2.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<CourseCategory>>(data2);

                ViewBag.course = courseList;
                ViewBag.categories = categories;
            }

            // Fetch enrollments for the current user
            HttpResponseMessage response3 = _client.GetAsync("https://localhost:7078/api/Enroll?$filter=userId eq " + uid).Result;

            if (response3.IsSuccessStatusCode)
            {
                string data3 = response3.Content.ReadAsStringAsync().Result;
                enrollments = JsonConvert.DeserializeObject<List<Enrollment>>(data3);
            }

            // Apply pagination to enrollments
            int pageSize = 2; // Adjust this value to set the desired page size
            int currentPage = pageIndex ?? 1;

            List<Enrollment> paginatedEnrollments = enrollments
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Filter enrollments based on enrolled courses
            List<Course> enrolledCourses = new List<Course>();

            if (paginatedEnrollments.Count != 0)
            {
                foreach (var enrollment in paginatedEnrollments)
                {
                    foreach (var course in courseList)
                    {
                        if (enrollment.CourseId == course.CourseId)
                        {
                            enrolledCourses.Add(course);
                        }
                    }
                }
            }

            // Set viewbag data
            ViewBag.enrollList = enrolledCourses;
            ViewBag.currentPage = currentPage;
            ViewBag.totalPages = Math.Ceiling((decimal)enrollments.Count / pageSize);

            return View();
        }

        [HttpGet]
        [Route("Course/GetCourseByCategory/{id}")]
        public IActionResult GetCourseByCategory(int id)
        {
            List<Course> courseListByCid = new List<Course>();
            courseListByCid = null;
            HttpResponseMessage response = _client.GetAsync("https://localhost:7078/api/Course?$filter=categoryId eq "+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                courseListByCid = JsonConvert.DeserializeObject<List<Course>>(data1);
                ViewBag.courseListByCid = courseListByCid;
                return View(courseListByCid);
            }
            return View();
        }

        [HttpGet]
        public IActionResult SearchCourse(string searchname)
        {
            string sname = searchname.ToLower();
            List<Course> courseSearchList = new List<Course>();
            courseSearchList = null;
            HttpResponseMessage response = _client.GetAsync("https://localhost:7078/api/Course?$filter=contains(tolower(code), '"+ sname + "') or contains(tolower(name), '"+ sname + "')").Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                courseSearchList = JsonConvert.DeserializeObject<List<Course>>(data1);
                ViewBag.courseSearchList = courseSearchList;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult GetCourseLesson(int id)
        {
            List<Enrollment> enrollment = new List<Enrollment>();
            bool isEnroll = false;
            string uid = HttpContext.Request.Cookies["loginId"];
            HttpResponseMessage response = _client.GetAsync("https://localhost:7078/api/Enroll?$filter=userId eq "+uid+" and courseId eq "+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                enrollment = JsonConvert.DeserializeObject<List<Enrollment>>(data1);
                if (enrollment.Count==1)
                {
                    isEnroll = true;
                }
            }
            if (isEnroll)
            {
                return RedirectToAction("Index", "WeekLesson", new { id = enrollment[0].CourseId });
            }
            else
            {
                return RedirectToAction("Index", "Enroll", new { id = id });
            }
        }
    }
}
