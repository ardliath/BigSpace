using Liath.BigSpace.DataAccess.Implementations;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain.Jobs;
using Liath.BigSpace.Domain.DataAccessDefinitions.Jobs;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.DataAccess.Implementations.Jobs;

namespace Liath.BigSpace.DataAccess.Definitions.Jobs
{
    public class JobRepository : DataAccessBase, IJobRepository
    {
        IEnumerable<IJobChildRepository> _childJobRepositories;

        public JobRepository(ISessionManager sessionManager, IShips ships)
            : base(sessionManager)
        {
            _childJobRepositories = new IJobChildRepository[] {
                //new BuildShipRepository(),
                new JourneyRepository(sessionManager, ships)
            };
        }

        public IEnumerable<Job> LoadExpiredButIncompleteJobs()
        {
            var jobs = new List<Job>();

            var whereAddition = string.Concat("IsComplete = 0 AND DATEADD(second, (Duration / ", TimeSpan.TicksPerSecond, "), StartTime ) < @Now");

            var query = this.CreateQuery(whereAddition);
            using(var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("Now", DbType.DateTime, DateTime.UtcNow);
                using(var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        var childRepository = this.LoadChildRepository(dr);
                        var job = childRepository.Create(dr, this.CreateAlias(childRepository));
                        this.LoadCoreProperties(job, dr);
                        jobs.Add(job);
                    }
                }
            }

            return jobs;
        }

        private void LoadCoreProperties(Job job, IDataReader dr)
        {
            job.JobID = dr.GetInt64("JobID");
            job.StartTime = dr.GetDateTime("StartTime");
            job.Duration = new TimeSpan(dr.GetInt64("Duration"));
            job.IsComplete = dr.GetBoolean("IsComplete");
            job.Desciption = dr.GetString("Description");
        }

        private string CreateAlias(IJobChildRepository childRepository)
        {
            var index = 0;
            for(int i = 0; i < _childJobRepositories.Count(); i++)
            {
                if(_childJobRepositories.ElementAt(i).Equals(childRepository))
                {
                    index = i;
                    break;
                }
            }

            return string.Concat("c", index);
        }

        private IJobChildRepository LoadChildRepository(IDataReader dr)
        {
            foreach(var childRepository in _childJobRepositories)
            {
                var pkColumn = string.Concat(this.CreateAlias(childRepository), "_", childRepository.PrimaryKeyColumnName);
                if(!dr.IsDBNull(pkColumn))
                {
                    return childRepository;
                }
            }

            return null;
        }

        private string CreateQuery(string filter = null)
        {
            var selectSB = new StringBuilder();
            var joinSB = new StringBuilder();

            selectSB.Append("SELECT b.JobID, b.StartTime, b.Duration");

            foreach(var childRepository in _childJobRepositories)
            {
                var alias = this.CreateAlias(childRepository);
                selectSB.AppendFormat(", {0}.{1} as {0}_{1}", alias, childRepository.PrimaryKeyColumnName);
                foreach(var field in childRepository.ColumnNames)
                {
                    selectSB.AppendFormat(", {0}.{1} as {0}_{1}", alias, field);
                }

                joinSB.AppendLine();
                joinSB.AppendFormat("JOIN {0} {1} ON b.JobID = {1}.{2}", childRepository.TableName, alias, childRepository.PrimaryKeyColumnName);
            }

            joinSB.AppendLine();
            selectSB.Append(" FROM Jobs b ");

            var filterQuery = filter == null
                ? null
                : string.Concat(" WHERE ", filter);
            return string.Concat(selectSB, joinSB, filterQuery);
        }


        public void MarkJobComplete(Job job)
        {
            using(var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE Jobs SET IsComplete = @Complete WHERE JobID = @ID"))
            {
                cmd.AddParameter("Complete", DbType.Boolean, true);
                cmd.AddParameter("ID", DbType.Int64, job.JobID);
                cmd.ExecuteNonQuery();

                job.IsComplete = true;
            }
        }
    }    
}
