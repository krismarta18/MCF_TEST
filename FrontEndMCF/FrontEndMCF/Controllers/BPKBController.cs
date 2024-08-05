using FrontEndMCF.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FrontEndMCF.Controllers
{
    public class BPKBController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7118/api/Transaction/";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BPKBController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var IsLoggin = IsUserLoggedIn();
            if (IsLoggin)
            {
                FetchDropdown();
                ViewBag.DropdownItems = TempData["DropdownItems"];
                if (TempData["DropdownItems"] == null)
                {
                    FetchDropdown();
                    ViewBag.DropdownItems = TempData["DropdownItems"];
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> FetchDropdown()
        {

            if (ModelState.IsValid)
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
                if (token != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var content = new StringContent(string.Empty, Encoding.UTF8, "application/json"); 
                    var response = await _httpClient.PostAsync(_apiUrl + "DropdownStorage", null);
                    var ss = response;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ServiceResponseSingle<List<ResDropdown>>>(result);
                        if (data.CODE == 1)
                        {
                           
                            TempData["DropdownItems"] = data.DATA;
                            return View();
                        }
                        else
                        {
                            TempData["ErrorMessage"] = data.MESSAGE;
                            return RedirectToAction("Index", "BPKB");
                        }

                      
                    }
                    else
                    {
                        TempData["ErrorMessage"] =" data.MESSAGE";
                        return RedirectToAction("Index", "BPKB");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }

               

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View("/Views/Login/index.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitData(BPKBViewModel model)
        {
            if (ModelState.IsValid)
            {

                var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
                if (token != null)
                {
                    var OO = JsonConvert.SerializeObject(model);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_apiUrl + "AddDataBPKB", content);
                    var ss = response;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ServiceResponseSingle<string>>(result);
                        if (data.CODE == 1)
                        {

                            TempData["SuccessMessage"] = data.MESSAGE;
                            return RedirectToAction("Index", "BPKB");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = data.MESSAGE;
                            return RedirectToAction("Index", "BPKB");
                        }

                    }
                    else
                    {
                        TempData["ErrorMessage"] = " data.MESSAGE";
                        return RedirectToAction("Index", "BPKB");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }



                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View("/Views/Login/index.cshtml");
        }
    }
}
