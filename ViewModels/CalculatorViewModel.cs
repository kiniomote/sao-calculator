using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcGreeterClient;

namespace Lab1.ViewModels
{
    public class CalculatorViewModel
    {
        public GrpcCalculateData CalculateData { get; set; }
        public GrpcLoginData LoginData { get; set; }
    }
}
