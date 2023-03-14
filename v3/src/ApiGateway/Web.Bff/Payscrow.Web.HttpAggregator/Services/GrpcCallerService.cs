using Grpc.Core;
using Grpc.Net.Client;
using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Payscrow.Web.HttpAggregator.Services
{
    public static class GrpcCallerService
    {
        public static async Task<TResponse> CallService<TResponse>(string urlGrpc, Func<GrpcChannel, Task<TResponse>> func)
        {
            //var proxy = new WebProxy
            //{
            //    Address = new Uri(proxyServer),
            //    UseDefaultCredentials = true,
            //    BypassProxyOnLocal = true,
            //    Credentials = CredentialCache.DefaultNetworkCredentials
            //};

            //proxy.BypassArrayList.Add(serviceUrl);

            //var httpClientHandler = new HttpClientHandler
            //{
            //    Proxy = proxy,
            //    DefaultProxyCredentials = CredentialCache.DefaultNetworkCredentials,
            //    UseDefaultCredentials = true,
            //    UseProxy = true,
            //};
            //var client = new HttpClient(httpClientHandler)
            //{
            //    DefaultRequestVersion = HttpVersion.Version20
            //};

            //using var channel = GrpcChannel.ForAddress(serviceUrl, new GrpcChannelOptions
            //{
            //    HttpClient = client
            //});




            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            var channel = GrpcChannel.ForAddress(urlGrpc);

            /*
            using var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            */


            Log.Information("Creating grpc client base address urlGrpc ={@urlGrpc}, BaseAddress={@BaseAddress} ", urlGrpc, channel.Target);

            try
            {
                return await func(channel);
            }
            catch (RpcException e)
            {
                Log.Error("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
                return default;
            }
            finally
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", false);
            }
        }

        public static async Task CallService(string urlGrpc, Func<GrpcChannel, Task> func)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            /*
            using var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            */

            var channel = GrpcChannel.ForAddress(urlGrpc);

            Log.Debug("Creating grpc client base address {@httpClient.BaseAddress} ", channel.Target);

            try
            {
                await func(channel);
            }
            catch (RpcException e)
            {
                Log.Error("Error calling via grpc: {Status} - {Message}", e.Status.StatusCode, e.Message);
            }
            finally
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", false);
            }
        }
    }
}
