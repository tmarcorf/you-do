using System.Text.Json.Serialization;

namespace YouDo.API.Responses
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("code")]
        public string Code { get; private set; }

        [JsonPropertyName("data")]
        public T Data { get; private set; }

        [JsonPropertyName("is_success")]
        public bool IsSuccess { get; private set; }

        [JsonPropertyName("error")]
        public string Error { get; private set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp => DateTime.UtcNow;

        public ApiResponse(string code, T data, bool isSuccess, string error = null)
        {
            Code = code;
            Data = data;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static ApiResponse<T> Success(T data, string code = "200")
        {
            return new ApiResponse<T>(code, data, true);
        }

        public static ApiResponse<T> Failure(string error, string code = "400")
        {
            return new ApiResponse<T>(code, default, false, error);
        }
    }
}
