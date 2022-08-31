CREATE TABLE [dbo].[Employee]
(
  [Id] UNIQUEIDENTIFIER NOT NULL,
  [Document] VARCHAR(20) NOT NULL,
  [FullName] VARCHAR(60) NOT NULL,
  [SocialName] VARCHAR(60) NULL,
  [Sex] CHAR(1) NOT NULL,
  [MaritalStatus] INT NOT NULL,
  [EducationLevel] INT NOT NULL,
  [BirthDate] DATE NOT NULL,
  [Phone] VARCHAR(20) NOT NULL,
  [Email] VARCHAR(255) NOT NULL,
  [CreatedAt] DATETIME NOT NULL,
  [UpdatedAt] DATETIME NULL,

  CONSTRAINT PK_Employee_Id PRIMARY KEY ([Id])
)

GO

CREATE UNIQUE INDEX UQ_Employee_Document ON [dbo].[Employee]([Document])
