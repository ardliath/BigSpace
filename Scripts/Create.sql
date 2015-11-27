create table Empires
(
	EmpireID int not null identity(1,1),
	Name nvarchar(255) not null,
	TS timestamp not null,
	constraint PK_Empires primary key (EmpireID)
)

GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 15/11/2015 15:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[UserAccountID] [int] IDENTITY(1,1) NOT NULL,
	[EmpireID] [int] NOT NULL,
	[Username] [nvarchar](255) NULL,
	[EmailAddress] [nvarchar](512) NOT NULL,
	[PasswordSalt] [varchar](32) NOT NULL,
	[PasswordHash] [binary](64) NOT NULL,
	[CreateTS] [datetime] NOT NULL,
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

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD  CONSTRAINT [FK_UserAccounts_Empires] FOREIGN KEY([EmpireID])
REFERENCES [dbo].[Empires] ([EmpireID])
GO
ALTER TABLE [dbo].[UserAccounts] CHECK CONSTRAINT [FK_UserAccounts_Empires]
GO


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

GO

create table Jobs
(
	JobID bigint not null identity(1,1),
	StartTime DateTime not null,
	Duration bigint not null,
	IsComplete bit not null,
	Description nvarchar(255) not null,
	TS timestamp not null
	constraint PK_Jobs primary key (JobID)
)

go

create table Journeys
(
	JobID bigint not null,
	StartSolarSystemID bigint not null,
	EndSolarSystemID bigint not null,
	constraint PK_Journeys primary key (JobID),
	constraint FK_Journeys_Jobs foreign key (JobID) references Jobs(JobID),
	constraint FK_Journeys_Start foreign key (StartSolarSystemID) references SolarSystems(SolarSystemID),
	constraint FK_Journeys_End foreign key (EndSolarSystemID) references SolarSystems(SolarSystemID)
)

create table Ships
(
	ShipID int not null identity(1,1),
	Name nvarchar(255) not null,
	SolarSystemID bigint null,
	JobID bigint null,
	UserAccountID int not null,
	EmpireID int not null,
	IsSelected bit not null,
	TS timestamp not null,
	constraint PK_Ships primary key (ShipID),
	constraint FK_Ships_SolarSystems foreign key (SolarSystemID) references SolarSystems(SolarSystemID),
	constraint FK_Ships_UserAccounts foreign key (UserAccountID) references UserAccounts(UserAccountID),
	constraint FK_Ships_Empires foreign key (EmpireID) references Empires(EmpireID)
)