using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Exceptions
{
    public class UnableToUpdateRecordException : Exception
    {
        public UnableToUpdateRecordException() : base()
        {

        }

        public UnableToUpdateRecordException(string message) : base(message)
        {

        }

        public UnableToUpdateRecordException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
