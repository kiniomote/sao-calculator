using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcGreeterClient;
using Grpc.Net.Client;
using Grpc.Core;

namespace Lab1.Services
{
    public class CalculatorService
    {
        public GrpcCalculateData CalculateData { get; private set; }
        public GrpcLoginData LoginData { get; private set; }

        public CalculatorService(GrpcCalculateData calculateData, GrpcLoginData loginData)
        {
            CalculateData = calculateData;
            LoginData = loginData;
        }

        public async Task<double> Calculate()
        {
            using var channel = GrpcChannel.ForAddress("https://soa-calculator-grpc.herokuapp.com");
            var client = new GrpcCalculator.GrpcCalculatorClient(channel);

            string token = await LoginGrpc(client);
            double result = await CalculateGrpc(client, token);

            channel.ShutdownAsync().Wait();

            return result;
        }

        private async Task<string> LoginGrpc(GrpcCalculator.GrpcCalculatorClient client)
        {
            var reply = await client.LoginAsync(LoginData);
            return reply.Token;
        }

        private async Task<double> CalculateGrpc(GrpcCalculator.GrpcCalculatorClient client, string token)
        {
            var headers = new Metadata
            {
                { "Authorization", $"Bearer {token}" }
            };

            var reply = await client.CalculateAsync(CalculateData, headers);
            return reply.Result;
        }
    }
}
