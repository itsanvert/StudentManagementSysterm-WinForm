CREATE DATABASE SMDB
GO
CREATE TABLE tblFaculty (
    facultyId INT IDENTITY NOT NULL PRIMARY KEY,
    facultyName NVARCHAR(50) NULL,
    facultyDes NVARCHAR(50) NULL
);
GO

CREATE TABLE tblStudent (
    studentId INT IDENTITY NOT NULL PRIMARY KEY,
    firstName NVARCHAR(50) NULL,
    lastName NVARCHAR(50) NULL,
    latinName NVARCHAR(50) NULL,
    Sex NVARCHAR(10) NULL,
    DOB DATETIME NULL,
    POB NVARCHAR(MAX) NULL,
    Address_ NVARCHAR(50) NULL,
    phoneNumber NVARCHAR(50) NULL,
    photo IMAGE NULL,
    email NVARCHAR(50) NULL
);
GO

CREATE TABLE tblRegister (
    registerId INT IDENTITY NOT NULL PRIMARY KEY,
    registerName NVARCHAR(50) NULL,
    registerDate NVARCHAR(50) NULL,
    facultyId INT FOREIGN KEY REFERENCES tblFaculty(facultyId),
    studentId INT NOT NULL,
    amount MONEY NULL,
    deposit MONEY NULL,
    balance MONEY NULL,
    FOREIGN KEY (studentId) REFERENCES tblStudent(studentId)
);

