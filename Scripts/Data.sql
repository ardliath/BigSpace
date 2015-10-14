insert into solarsystems(name, x, y, z) values ('Sol', 1, 5, 0)
declare @earth int = scope_identity()
insert into solarsystems(name, x, y, z) values ('Proxima Century', 2, 3, 0)

insert into ships(name, solarsystemid) values ('USS Enterprise', @earth)

select * from ships
select * from solarsystems
