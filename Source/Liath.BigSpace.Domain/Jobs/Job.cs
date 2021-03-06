﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.Jobs
{
    public abstract class Job
    {
        public Int64 JobID { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsComplete { get; set; }
        public string Desciption { get; set; }

        public abstract void Complete();
    }
}
