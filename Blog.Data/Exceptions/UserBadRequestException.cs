namespace Blog.Data.Exceptions
{
    public class UserBadRequestException : BadRequestException
    {
        public UserBadRequestException() 
            : base("Some issue happened, please try again.")
        {
        }
    }
}
