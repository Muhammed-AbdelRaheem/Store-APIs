namespace Store.APIs.Errors
{
    public class ApiErrorResponse
    {
      

        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public ApiErrorResponse(int statusCode, string? errorMessage=null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage??GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {
                400=>"A bad Request , You Have Made",
                401=>"Authorized !! , You Are Not",
                404=> "Resource Is Not Found",
                500=>"Server Error",
                _=> null
            };

            return message;
        }




    }
}
