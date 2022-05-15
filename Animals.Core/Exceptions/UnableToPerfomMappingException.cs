using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Exceptions
{
    internal class UnableToPerfomMappingException : Exception
    {
        public UnableToPerfomMappingException() : base()
        {

        }

        public UnableToPerfomMappingException(string message) : base(message)
        {

        }

        public UnableToPerfomMappingException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
