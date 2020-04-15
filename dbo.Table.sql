CREATE TABLE [dbo].[Games]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(MAX) NOT NULL, 
    [set] NVARCHAR(50) NOT NULL, 
    [cpu_rate] REAL NOT NULL, 
    [gpu_rate] REAL NOT NULL, 
    [ram_rate] REAL NOT NULL, 
    [hdd_rate] REAL NOT NULL, 
    [ssd_rate] REAL NOT NULL
)
