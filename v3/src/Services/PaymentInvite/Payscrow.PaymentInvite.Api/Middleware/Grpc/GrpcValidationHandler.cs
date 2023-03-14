using Grpc.Core;
using Grpc.Core.Interceptors;
using Payscrow.PaymentInvite.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Api.Middleware.Grpc
{
    public class GrpcValidationHandler : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            //logger.Debug($"{Environment.NewLine}GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Formatting.Indented)}");


            try
            {
                var response = await base.UnaryServerHandler(request, context, continuation);
                return response;
            }
            catch (ValidationException e)
            {
                var m = e.Failures.Count;
                context.Status = new Status(StatusCode.InvalidArgument, e.Message);
                foreach (var err in e.Failures)
                {
                    context.ResponseTrailers.Add(err.Key, err.Value.FirstOrDefault());
                }
                //context.ResponseTrailers.
                throw;

            }
            

            //logger.Debug($"{Environment.NewLine}GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(response, Formatting.Indented)}");

            //return response;
        }
    }
}
