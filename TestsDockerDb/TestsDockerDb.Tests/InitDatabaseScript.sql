CREATE TABLE Users(
	id uniqueidentifier NOT NULL UNIQUE,
	[name] varchar(50) NOT NULL,
	[email] varchar(50) NOT NULL UNIQUE
);