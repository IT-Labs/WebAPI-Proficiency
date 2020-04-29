using System.Collections.Generic;
using System.Net;

namespace WebApplication.Contracts
{
   
        //  Definition of the Response
        /// </summary>
        public interface IResponse
        {
            /// <summary>
            /// <summary>
            ///  Gets or sets https status of the response
            /// </summary>
            HttpStatusCode Status { get; set; }

            /// <summary>
            ///     Gets or sets list of messages of the response
            /// </summary>
            List<ResponseMessage> Messages { get; set; }
        }
    

    public class ResponseMessage
    {
        public ResponseMessage()
        {

        }
        public string Code { get; set; }

        /// <summary>
        ///  Gets or sets the message
        /// </summary>
        public string Message { get; set; }

    }
}
