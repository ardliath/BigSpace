using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Exceptions
{
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException(int id)
            : base(string.Format("Entity of type '{0}' with ID {1} could not be found", typeof(T).FullName, id))
        {
            this.ID = id;
        }

        public EntityNotFoundException(long id)
            : base(string.Format("Entity of type '{0}' with ID {1} could not be found", typeof(T).FullName, id))
        {
            this.LongID = id;
        }

        public int ID { get; set; }

        public long LongID { get; set; }
    }
}
