﻿using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Domain.UserAccountDomain;
using Liath.BigSpace.Domain.SolarSystems;

namespace Liath.BigSpace.DataAccess.Implementations
{
    public class Ships : DataAccessBase, IShips
    {
        public Ships(ISessionManager sessionManager)
            : base(sessionManager)
		{
		}

        public IEnumerable<Ship> ListShipsAtSolarSystem(long solarSystemID)
        {
            var ships = new List<Ship>();
            var query = this.CreateSelectQuery("SolarSystemID = @ID");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("ID", DbType.Int64, solarSystemID);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ships.Add(this.InflateShip(dr));
                    }
                }
            }

            return ships;
        }


        public Ship GetShip(int shipID)
        {
            var query = this.CreateSelectQuery("ShipID = @ID");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("ID", DbType.Int32, shipID);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return this.InflateShip(dr);
                    }

                    return null;
                }
            }
        }

	    public ShipWithCurrentStatus GetShipWithCurrentStatus(int shipID)
	    {
			var query = CreateQueryForShipWithCurrentStatus("s.ShipID = @ShipID");			
			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
			{
				cmd.AddParameter("ShipID", DbType.Int32, shipID);

				using (var dr = cmd.ExecuteReader())
				{
					if(dr.Read())
					{
						return InflateShipWithCurrentStatus(dr);						
					}
				}
			}

			return null;
		}

	    private Ship InflateShip(IDataReader dr)
        {
            var ship = new Ship();
            this.PopulateCoreFields(ship, dr);
            return ship;
        }

        private void PopulateCoreFields(Ship ship, IDataReader dr)
        {
            ship.ShipID = dr.GetInt32("ShipID");
            ship.UserAccountID = dr.GetInt32("UserAccountID");
            ship.Name = dr.GetString("Name");
            ship.IsSelected = dr.GetBoolean("IsSelected");
            ship.SolarSystemID = dr.GetNullableInt64("SolarSystemID");
            ship.JobID = dr.GetNullableInt64("JobID");
        }

        public static string[] RequiredFields = new string[] { "ShipID", "Name", "SolarSystemID", "UserAccountID", "IsSelected", "JobID", "EmpireID" };

        private string CreateSelectQuery(string filter = null)
        {
            var sb = new StringBuilder("SELECT ShipID, ");
            sb.Append(string.Join(", ", RequiredFields));
            sb.Append(" FROM Ships");
            if(filter != null)
            {
                sb.AppendFormat(" WHERE {0}", filter);
            }

            return sb.ToString();
        }

        public void Save(Ship ship)
        {
            if (ship == null) throw new ArgumentNullException("ship");

            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE Ships SET Name = @Name, IsSelected = @IsSelected WHERE ShipID = @ID"))
            {
                cmd.AddParameter("ID", DbType.Int32, ship.ShipID);
                cmd.AddParameter("Name", DbType.String, ship.Name);
                cmd.AddParameter("IsSelected", DbType.Boolean, ship.IsSelected);

                cmd.ExecuteNonQuery();
            }
        }


        public IEnumerable<Ship> ListShipsDoingJob(long jobID)
        {
            var ships = new List<Ship>();
            var query = this.CreateSelectQuery("JobID = @JobID");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("JobID", DbType.Int64, jobID);

                using(var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        ships.Add(this.InflateShip(dr));
                    }
                }
            }

            return ships;
        }


        public void SetShipLocation(int shipID, Int64? solarSystemID)
        {
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE Ships SET SolarSystemID = @SolarSystemID WHERE ShipID = @ShipID"))
            {
                cmd.AddParameter("ShipID", DbType.Int32, shipID);
                cmd.AddParameter("SolarSystemID", DbType.Int64, (object)solarSystemID ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void SetShipJob(int shipID, Int64? jobID)
        {
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE Ships SET JobID = @JobID WHERE ShipID = @ShipID"))
            {
                cmd.AddParameter("ShipID", DbType.Int32, shipID);
                cmd.AddParameter("JobID", DbType.Int64, (object)jobID ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }


        public IEnumerable<Ship> ListSelectedShips(int userAccountID)
        {
            var ships = new List<Ship>();
            var query = this.CreateSelectQuery("IsSelected = 1 and UserAccountID = @UserAccountID");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("UserAccountID", DbType.Int32, userAccountID);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ships.Add(this.InflateShip(dr));
                    }
                }
            }

            return ships;
        }


        public Ship CreateShip(SolarSystem located, UserAccount owner, string name)
        {
            int id;
            var query = string.Concat("INSERT INTO Ships (", string.Join(", ", RequiredFields), ") VALUES (", string.Join(", ", RequiredFields.Select(f => string.Concat("@", f))), ") SELECT Scope_Identity()");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("UserAccountID", DbType.Int32, owner.UserAccountID);
                cmd.AddParameter("Name", DbType.String, name);
                cmd.AddParameter("SolarSystemID", DbType.Int64, located.SolarSystemID);
                cmd.AddParameter("IsSelected", DbType.Boolean, 0);
                cmd.AddParameter("JobID", DbType.Int64, DBNull.Value);
                cmd.AddParameter("EmpireID", DbType.Int32, owner.EmpireID);

                id = (int)(decimal)cmd.ExecuteScalar();                
            }

            return this.GetShip(id);
        }


        public IEnumerable<ShipWithCurrentStatus> ListAllShipsInEmpire(int empireID)
        {
						var query = CreateQueryForShipWithCurrentStatus("s.EmpireID = @EmpireID");
            var ships = new List<ShipWithCurrentStatus>();
            using(var cmd =this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("EmpireID", DbType.Int32, empireID);

                using(var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
	                    var ship = InflateShipWithCurrentStatus(dr);

	                    ships.Add(ship);
                    }
                }
            }

            return ships;
        }

	    public IEnumerable<Command> ListAllCommands()
	    {
		    var commands = new List<Command>();
		    using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("SELECT CommandID, Code, Description FROM Commands"))
		    {
			    using (var dr = cmd.ExecuteReader())
			    {
				    while (dr.Read())
				    {
					    commands.Add(this.InflateCommand(dr));
				    }
			    }
		    }

		    return commands;
	    }

	    private Command InflateCommand(IDataReader dr)
	    {
		    return new Command
		    {
			    CommandID = dr.GetInt32("CommandID"),
			    Code = dr.GetString("Code"),
			    Description = dr.GetString("Description")
		    };
	    }

	    public IEnumerable<Command> ListCommandsForShip(int shipID)
	    {
		    var commands = new List<Command>();
		    var query =
					"SELECT c.CommandID, c.Code, c.Description FROM Commands c JOIN ShipCommands sc on c.CommandID = sc.CommandID and sc.ShipID = @ShipID";
		    using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
		    {
			    cmd.AddParameter("ShipID", DbType.Int32, shipID);
			    using (var dr = cmd.ExecuteReader())
			    {
				    while (dr.Read())
				    {
					    commands.Add(this.InflateCommand(dr));
				    }
			    }
		    }

		    return commands;
	    }

	    public void GiveCommand(int shipId, int commandID)
	    {
		    using (var cmd = this.SessionManager.GetCurrentUnitOfWork()
			    .CreateCommand("INSERT INTO ShipCommands (ShipID, CommandID) VALUES (@ShipID, @CommandID)"))
		    {
			    cmd.AddParameter("ShipID", DbType.Int32, shipId);
			    cmd.AddParameter("CommandID", DbType.Int32, commandID);

			    cmd.ExecuteNonQuery();
		    }
	    }

	    public void CancelCommand(int shipId, int commandID)
	    {
			using (var cmd = this.SessionManager.GetCurrentUnitOfWork()
				.CreateCommand("DELETE FROM ShipCommands WHERE ShipID = @ShipID AND CommandID = @CommandID"))
			{
				cmd.AddParameter("ShipID", DbType.Int32, shipId);
				cmd.AddParameter("CommandID", DbType.Int32, commandID);

				cmd.ExecuteNonQuery();
			}
		}

	    private ShipWithCurrentStatus InflateShipWithCurrentStatus(IDataReader dr)
	    {
		    var ship = new ShipWithCurrentStatus();
		    this.PopulateCoreFields(ship, dr);
		    ship.SolarSystemID = dr.GetNullableInt64("SolarSystemID");
		    ship.SolarSystemName = dr.GetNullableString("SolarSystemName");
		    ship.JobID = dr.GetNullableInt64("JobID");
		    ship.JobDescription = dr.GetNullableString("JobDescription");
		    return ship;
	    }

	    private string CreateQueryForShipWithCurrentStatus(string filter = null)
	    {
		    return string.Concat("SELECT ", string.Join(", ", RequiredFields.Select(f => string.Concat("s.", f))),
			    @",ss.SolarSystemID, ss.Name SolarSystemName, j.JobID, j.Description JobDescription FROM Ships s
LEFT JOIN SolarSystems ss on s.SolarSystemID = ss.SolarSystemID
LEFT JOIN Jobs j on s.JobID = j.JobID",
					filter == null ? null : " WHERE ",
					filter);
	    }
    }
}
