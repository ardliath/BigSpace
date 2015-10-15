insert into solarsystems(name, x, y, z) values ('Sol', 1, 5, 0)
declare @earth int = scope_identity()
insert into solarsystems(name, x, y, z) values ('Proxima Century', 2, 3, 0)

insert into UserAccounts(username, x, y, z) values ('viao\adam', 0, 0, 0)
declare @me int = scope_identity()

insert into ships(name, solarsystemid, useraccountid) values ('USS Enterprise', @earth, @me)

select * from ships
select * from solarsystems
select * from useraccounts

