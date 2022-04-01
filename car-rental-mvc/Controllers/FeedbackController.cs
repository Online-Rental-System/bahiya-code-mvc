using car_rental_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace car_rental_mvc.Controllers
{
    public class FeedbackController : Controller
    {


        string Baseurl = "https://localhost:44331/";

        public async Task<ActionResult> Index()

        {
            List<Feedback> PInfo = new List<Feedback>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllFeedback using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Feedbacks");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Feedback>>(Response);
                }
                //returning the employee list to view
                return View(PInfo);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/Feedbacks";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        TempData["Feedback"] = JsonConvert.SerializeObject(feedback);

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not add feedback");
                        return View();

                    }
                }

            }
        }
        [HttpGet]
        public IActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Feedback PInfo = new Feedback();
             using (HttpClient client = new HttpClient())
            {
                
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
               
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
                HttpResponseMessage Res = await client.GetAsync("api/Feedbacks/"+id);
            
                if (Res.IsSuccessStatusCode)
                {
               
                    var Response = Res.Content.ReadAsStringAsync().Result;
                 
                    PInfo = JsonConvert.DeserializeObject<Feedback>(Response);
                }
           
                return View(PInfo);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id, Feedback feedback)
        {


            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/Feedbacks/"+id;

                using (var Response = await client.DeleteAsync(endpoint))
                {
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        TempData["Feedback"] = JsonConvert.SerializeObject(feedback);

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not delete feedback");
                        return View();

                    }
                }

            }
        }


    }
}
    




   

