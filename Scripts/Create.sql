create table SolarSystems
(
	SolarSystemID bigint not null identity(1,1),
	Name nvarchar(255),
	X bigint not null,
	Y bigint not null,
	Z bigint not null,
	TS timestamp not null
	constraint PK_SolarSystems primary key (SolarSystemID)
)

create table Ships
(
	ShipID int not null identity(1,1),
	Name nvarchar(255) not null,
	SolarSystemID int null,
	TS timestamp not null,
	constraint PK_Ships primary key (ShipID),
	constraint FK_Ships_SolarSystems foreign key (SolarSystemID) references SolarSystems(SolarSystemID)
)
