using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController(IConfiguration config)
        {
            _client = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(string email, string password)
        {
            User u = new User();
            u.Email = email;
            u.Password = password;
            List<User> userlist = new List<User>();

            string data = JsonConvert.SerializeObject(u);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync("https://localhost:7078/api/User/Login", content).Result;
            HttpResponseMessage response1 = _client.GetAsync("https://localhost:7078/api/User?$filter=tolower(email) eq '"+email.ToLower()+"' and password eq '"+password+"'").Result;

            if (response.IsSuccessStatusCode && response1.IsSuccessStatusCode)
            {
                string token = response.Content.ReadAsStringAsync().Result;
                string data1 = response1.Content.ReadAsStringAsync().Result;
                userlist = JsonConvert.DeserializeObject<List<User>>(data1);
                User a = userlist[0];
                Response.Cookies.Append("token", token);
                Response.Cookies.Append("loginId", a.UserId.ToString());

                return RedirectToAction("Index", "Course");
            }
            return RedirectToAction("Login", "Home");
        }

        
    }
}

