namespace EscuelaWebAPI.DTO.General
{
    public class ResponseDTO
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = String.Empty;
        public object? ResultData { get; set; }
    }
}
