using car_rental_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace car_rental_mvc.Controllers
{
    public class UpdaterentController : Controller
    {
        string Baseurl = "https://localhost:44331/";
        public async Task<ActionResult> Index()

        {
            List<UpdateRent> PInfo = new List<UpdateRent>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllFeedback using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Cars");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the cars list
                    PInfo = JsonConvert.DeserializeObject<List<UpdateRent>>(Response);
                }
                //returning the cars list to view
                return View(PInfo);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Update(byte id)
        {
            UpdateRent PInfo = new UpdateRent();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Cars/"+id);

                if (Res.IsSuccessStatusCode)
                {

                    var Response = Res.Content.ReadAsStringAsync().Result;

                    PInfo = JsonConvert.DeserializeObject<UpdateRent>(Response);
                }

                return View(PInfo);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Update(byte id, UpdateRent updaterent)
        {


            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(updaterent), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl + "api/Cars/" + id;

                using (var Response = await client.PutAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["UpdateRent"] = JsonConvert.SerializeObject(updaterent);

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not edit");
                        return View();

                    }
                }

            }
        }

    }
}