using System.Net;

namespace Limbo.DataAccess.Services.Models {
    /// <inheritdoc/>
    public class ServiceResponse<T> : IServiceResponse<T> where T : class {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="reponse"></param>
        public ServiceResponse(HttpStatusCode statusCode, T? reponse) {
            StatusCode = statusCode;
            ResponseValue = reponse;
        }

        /// <inheritdoc/>
        public HttpStatusCode StatusCode { get; set; }

        /// <inheritdoc/>
        public T? ResponseValue { get; set; }
    }
}
