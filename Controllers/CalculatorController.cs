using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Services;
using Lab1.ViewModels;
using GrpcGreeterClient;

namespace Lab1.Controllers
{
    [Route("calculator")]
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculatorViewModel calculateModel)
        {
            CalculatorService calculator = new CalculatorService(calculateModel.CalculateData, calculateModel.LoginData);
            var response = await calculator.Calculate();
            return Json(response);
        }
    }
}
