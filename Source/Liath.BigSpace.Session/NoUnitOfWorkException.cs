using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liath.BigSpace.Session
{
    public class NoUnitOfWorkException : Exception        
    {       
        public NoUnitOfWorkException()
            : base("There is no current UnitOfWork")
        {
        }
    }
}
