namespace App.Exceptions.CheckInExceptions
{
    public class SiteDoesNotExistException : CheckInException
    {
        public SiteDoesNotExistException() : base("Site does not exist")
        {
            
        }
    }
}
