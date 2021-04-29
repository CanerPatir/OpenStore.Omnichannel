using System;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace OpenStore.Omnichannel.Panel.Services
{
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, ErrorReadModel error, string rawErrorContent, bool isInvalidToken)
        {
            StatusCode = statusCode;
            Error = error;
            RawErrorContent = rawErrorContent;
            IsInvalidToken = isInvalidToken;
        }

        public HttpStatusCode StatusCode { get; }
        public ErrorReadModel Error { get; }
        public string RawErrorContent { get; }
        public bool IsInvalidToken { get; }
        public bool IsUnauthorized => StatusCode == HttpStatusCode.Unauthorized;

        public string GetMessageKey() => Error?.Message;

        public string GetAggregatedErrorMessage()
        {
            if (Error == null)
            {
                // return RawErrorContent;
                return string.Empty;
            }

            var sb = new StringBuilder();

            if (!Error.Errors.Any()) sb.Append(Error.Message.Trim());

            foreach (var errorError in Error.Errors)
            {
                sb.AppendLine();
                sb.Append(errorError.Trim());
            }

            return sb.ToString().Trim();
        }
    }
}