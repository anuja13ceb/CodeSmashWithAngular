namespace CodeSmashWithAngular.Error
{
    public class ApiError
    {
        public ApiError(int error, string errorMessage, string errorDetails) {

            Error=error;
            ErrorMessage=errorMessage;
            ErrorDetails=errorDetails;
        }
        public int Error { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }


    }
}
