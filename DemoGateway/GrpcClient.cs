using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemoGateway.Contracts;
using Grpc.Net.Client;

namespace DemoGateway
{
    public class GrpcClient : IGrpcClient
    {
        public GrpcChannel DepartementChannel { get; }

        public GrpcClient()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            HttpClient httpClient = new HttpClient(httpClientHandler);

            DepartementChannel = GrpcChannel.ForAddress("https://localhost:5000", new GrpcChannelOptions { HttpClient = httpClient });
        }
    }
}
