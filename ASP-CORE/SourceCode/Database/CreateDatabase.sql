-- Create an empty SQL Server database first, then configure DefaultConnection.
-- The EF Core migration creates Identity and OFFSTEP tables:
-- dotnet ef migrations add InitialCreate
-- dotnet ef database update
CREATE DATABASE OFFSTEP;
GO
