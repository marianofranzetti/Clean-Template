namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public CodeErrorResponse(int statusCode, string message = null!)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "los parametros enviados no son correctos",
                401 => "Usuario no autorizado para el recursos solicitado",
                404 => "No se encontro el recurso solicitado",
                500 => "Se produjo un error en el servidor",
                _ => string.Empty
            };
        }
    }
}
