-- Script Date: 13-12-2019 09:24  - ErikEJ.SqlCeScripting version 3.5.2.85
CREATE TABLE [Category] (
  [Id] nvarchar(100) NOT NULL
, [Name] nvarchar(100) DEFAULT ('<No Name>') NOT NULL
, [CreatedDate] datetime DEFAULT (GETDATE()) NOT NULL
, [UpdatedDate] datetime NOT NULL
, [IsActive] bit DEFAULT (1) NOT NULL
);
GO
ALTER TABLE [Category] ADD CONSTRAINT [PK_Category] PRIMARY KEY ([Id]);
GO

-- Script Date: 13-12-2019 09:32  - ErikEJ.SqlCeScripting version 3.5.2.85
CREATE TABLE [Item] (
  [Id] nvarchar(100) NOT NULL
, [CategoryId] nvarchar(100) NOT NULL
, [Name] nvarchar(200) DEFAULT ('<No Name>') NOT NULL
, [Type] nvarchar(10) NOT NULL
, [Path] nvarchar(200) NOT NULL
, [Size] nvarchar(10) NOT NULL
, [CreatedDate] datetime DEFAULT (GETDATE()) NOT NULL
, [UpdatedDate] datetime NOT NULL
, [IsActive] bit DEFAULT (1) NOT NULL
);
GO
ALTER TABLE [Item] ADD CONSTRAINT [PK_Item] PRIMARY KEY ([Id]);
GO
ALTER TABLE [Item] ADD CONSTRAINT [FK_Item_Category] FOREIGN KEY ([Id]) REFERENCES Category(Id);
GO