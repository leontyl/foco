using System;

namespace App.Exceptions.CheckInExceptions
{
    public abstract class CheckInException : Exception
    {
        protected CheckInException(string msg) : base(msg)
        {
        }
    }
}
