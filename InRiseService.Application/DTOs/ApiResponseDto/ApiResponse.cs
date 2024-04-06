namespace InRiseService.Application.DTOs.ApiResponseDto
{
    public class ApiResponse<T>
    {
        public int Status {get; set; }
        public T Data {get; private set; }

        public ApiResponse(int status, T data)
        {
            Status = status;
            Data = data;
        }

    }
}