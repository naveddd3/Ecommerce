using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public class Response
    {
        public ResponseStatus StatusCode { get; set; }
        public string ResponseText { get; set; }
    }
    public enum ResponseStatus
    {
        Failed = -1,
        Success = 1,
    }
    public class Response<T> : IResponse<T>
    {

        public ResponseStatus StatusCode { get; set; }
        public string ResponseText { get; set; }
        public T Result { get; set; }
        public Response()
        {
            StatusCode = ResponseStatus.Failed;
            ResponseText = ResponseStatus.Failed.ToString();
        }
    }

    public interface IResponse<T>
    {
        ResponseStatus StatusCode { get; set; }
        string ResponseText { get; set; }
        T Result { get; set; }
    }
}
