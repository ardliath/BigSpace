insert into Empires (name) values ('Starfleet')
declare @empire int = scope_identity()

insert into solarsystems(name, x, y, z) values ('Terran', 1, 5, 0)
declare @earth int = scope_identity()
insert into solarsystems(name, x, y, z) values ('Proxima Century', 2, 3, 0)

insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 0, 'Mercury', 'Mercury.png', 0, 0)
insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 1, 'Venus', 'Venus.png', 0, 0)
insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 2, 'Earth', 'Earth.png', 6000000000, 9000000000)
insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 3, 'Mars', 'Mars.png', 0, 4000000000)
insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 4, 'Jupiter', 'Jupiter.png', 0, 0)
insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 5, 'Saturn', 'Saturn.png', 0, 0)
insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 6, 'Uranus', 'Uranus.png', 0, 0)
insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 7, 'Neptune', 'Neptune.png', 0, 0)
insert into Planets(solarsystemid, positionindex, name, image, population, maxpopulation) values (@earth, 8, 'Pluto', 'Pluto.png', 0, 0)

insert into UserAccounts(empireid, username, emailaddress, passwordsalt, passwordhash, createts, x, y, z) values (@empire, 'Adam', 'a@a.com', '123', 0x02E7675855098EC77B69D7B03F8EE6797EAEE04C, getdate(), 0, 0, 0)
declare @me int = scope_identity()

insert into ships(name, solarsystemid, useraccountid, EmpireID, IsSelected) values ('USS Enterprise', @earth, @me, @Empire, 0)

select * from ships
select * from solarsystems
select * from useraccounts