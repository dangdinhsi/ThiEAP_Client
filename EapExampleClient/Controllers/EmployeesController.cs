using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EapExampleClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace EapExampleClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly string SERVER = "https://eapexample20181227104104.azurewebsites.net/";
        private readonly string SERVER_EMPLOYEE_URI = "api/employees";

        private static List<Employee> list;
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVER);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(SERVER_EMPLOYEE_URI);
                list = await response.Content.ReadAsAsync<List<Employee>>();

            }
            return View(list);
        }
        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,Email")] Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVER);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync(SERVER_EMPLOYEE_URI, employee);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,Email")] Employee employee)
        {
            return View(employee);
        }
        public async Task<IActionResult> Delete(int? id)
        {

            return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            return RedirectToAction(nameof(Index));
        }
    }
}