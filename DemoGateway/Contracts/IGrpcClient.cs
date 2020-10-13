using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace DemoGateway.Contracts
{
    public interface IGrpcClient
    {
        GrpcChannel DemoChannel { get; }
    }
}
