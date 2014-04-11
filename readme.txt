www.listings.lifetrap.net

SQL:

DROP TABLE DBUser;
DROP TABLE Organization;
CREATE TABLE DBUser (UserName varchar(30) NOT NULL, Password varchar(30) NOT NULL;
CREATE TABLE Organization (Name varchar(30)NOT NULL, [Contact fName] varchar(30) NOT NULL, [Contact lName] varchar(30) NOT NULL, Phone varchar(30) NOT NULL, [Street Address] varchar(100) NOT NULL, City varchar(30) NOT NULL, Province varchar(30) NOT NULL, Country varchar(30) NOT NULL, [Postal Code] varchar(30) NOT NULL, Longitude varchar(30) NOT NULL, Latitude varchar(30) NOT NULL, ID int NOT NULL);
ALTER TABLE DBUser
ADD PRIMARY KEY (UserName)
ALTER TABLE Organization
ADD PRIMARY KEY (ID)


