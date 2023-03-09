using SharedLibrary.Helpers;

namespace SharedLibrary.Models
{

    public class TDResponse
    {
        public bool HasError { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public int ErrorId { get; set; } = 0;

        public void SetError(string Message)
        {
            HasError = true;
            this.Message = Message;
        }
        public void SetError()
        {
            HasError = true;
            Message = OperationMessages.GeneralError;
        }

        public void SetSuccess(string Message)
        {
            HasError = false;
            this.Message = Message;
        }
        public void SetSuccess()
        {
            HasError = false;
            Message = OperationMessages.Success;
        }


    }
    public class TDResponse<T> : TDResponse
    {
        public T? Data { get; set; } = default;

    }

}