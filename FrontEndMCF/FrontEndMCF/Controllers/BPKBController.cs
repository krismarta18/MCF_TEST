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
        public async Task<IActionResult> Index()
        {
            var IsLoggin = IsUserLoggedIn();
            if (IsLoggin)
            {
                var dropdown = await FetchDropdown();
                TempData["DropdownItems"] = dropdown;


                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
        public async Task<IActionResult> ListData()
        {
            if (ModelState.IsValid)
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
                if (token != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_apiUrl + "GetList", null);
                    var ss = response;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ServiceResponseSingle<List<ResDataBPKB>>>(result);
                        if (data.CODE == 1)
                        {

                            return View("/Views/BPKB/ListBPKB.cshtml",data.DATA);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = data.MESSAGE;
                            return RedirectToAction("ListData", "BPKB");
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = " data.MESSAGE";
                        return RedirectToAction("ListData", "BPKB");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }


            }
            return View("/Views/BPKB/ListBPKB.cshtml");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return View("/Views/BPKB/ListBPKB.cshtml");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var payload = new { id = id }; // Membungkus ID dalam objek
                        var jsonContent = JsonConvert.SerializeObject(payload);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        var response = await _httpClient.PostAsync(_apiUrl + "GetDataById", content);
                        var ss = response;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            var data = JsonConvert.DeserializeObject<ServiceResponseSingle<ResDataBPKB>>(result);
                            if (data.CODE == 1)
                            {
                                var dropdown = await FetchDropdown();
                                TempData["DropdownItems"] = dropdown;
                                return View("/Views/BPKB/EditBPKB.cshtml", data.DATA);
                            }
                            else
                            {
                                TempData["ErrorMessage"] = data.MESSAGE;
                                return RedirectToAction("Edit", "BPKB");
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = " data.MESSAGE";
                            return RedirectToAction("Edit", "BPKB");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }


                }
                return View("/Views/BPKB/EditBPKB.cshtml");
            }

        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "id Tidak ditemukan";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var payload = new { id = id }; // Membungkus ID dalam objek
                        var jsonContent = JsonConvert.SerializeObject(payload);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        var response = await _httpClient.PostAsync(_apiUrl + "DeleteData", content);
                        var ss = response;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            var data = JsonConvert.DeserializeObject<ServiceResponseSingle<ResDataBPKB>>(result);
                            if (data.CODE == 1)
                            {

                                TempData["SuccessMessage"] = data.MESSAGE;
                                return RedirectToAction("ListData", "BPKB");
                            }
                            else
                            {
                                TempData["ErrorMessage"] = data.MESSAGE;
                                return RedirectToAction("ListData", "BPKB");
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Terjadi kesalahan";
                            return RedirectToAction("ListData", "BPKB");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }


                }
            }
            return RedirectToAction("ListData", "BPKB");
        }



        public async Task<List<ResDropdown>> FetchDropdown()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiUrl + "DropdownStorage", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ServiceResponseSingle<List<ResDropdown>>>(result);
                    if (data.CODE == 1)
                    {
                        return data.DATA;
                    }
                    else
                    {
                        // Lempar exception atau tangani sesuai kebutuhan jika terjadi kesalahan pada response
                        throw new Exception(data.MESSAGE);
                    }
                }
                else
                {
                    // Lempar exception atau tangani jika terjadi kesalahan saat melakukan request ke API
                    throw new Exception("Failed to fetch data from the API.");
                }
            }
            else
            {
                // Lempar exception atau tangani sesuai kebutuhan jika token tidak ditemukan
                throw new UnauthorizedAccessException("Token is missing. Please log in.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitData(BPKBViewModel model)
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
                        return RedirectToAction("ListData", "BPKB");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = data.MESSAGE;
                        return RedirectToAction("ListData", "BPKB");
                    }

                }
                else
                {
                    TempData["ErrorMessage"] = " data.MESSAGE";
                    return RedirectToAction("ListData", "BPKB");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }



            return View("/Views/Login/index.cshtml");
        }
    }
}
