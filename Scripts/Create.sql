
CREATE TABLE [UserAccounts](
	[UserAccountID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](255) NULL,
	[X] [bigint] NOT NULL,
	[Y] [bigint] NOT NULL,
	[Z] [bigint] NOT NULL,
	[TS] [timestamp] NOT NULL,
 CONSTRAINT [PK_UserAccounts] PRIMARY KEY CLUSTERED 
(
	[UserAccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


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
	SolarSystemID bigint null,
	UserAccountID int not null,
	IsSelected bit not null,
	TS timestamp not null,
	constraint PK_Ships primary key (ShipID),
	constraint FK_Ships_SolarSystems foreign key (SolarSystemID) references SolarSystems(SolarSystemID),
	constraint FK_Ships_UserAccounts foreign key (UserAccountID) references UserAccounts(UserAccountID)
)
