namespace Blog.Data.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId) 
            : base($"User with id: {userId} not found.")
        {
        }
    }
}
