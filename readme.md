# COMP2001 - Coursework Part 3
## _Projects API_

You are to create a web service that provides access through a RESTful API interface
following the specification provided below. You are to follow the specification provided in
Appendix A below to create your RESTful API providing the functionality for registering,
amending, validating and deleting. The specification provides information about the basics
for the API, you will need to extend and apply your own consideration in some areas.
Your API must be hosted on the web.socem.plymouth.ac.uk server and use the Microsoft
SQL Server database on socem1.uopnet.plymouth.ac.uk. Both of these are provided to
you for this module. The code for the API must be hosted in a subdirectory of your folder
labelled Auth. This has already been set up for you. In addition, ensure that your
database has an appropriate amount of sample data within it to demonstrate the
functionality.

## My API
- http://web.socem.plymouth.ac.uk/COMP2001/bjames/auth/api
- http://web.socem.plymouth.ac.uk/COMP2001/bjames/auth/api/projects
- http://web.socem.plymouth.ac.uk/COMP2001/bjames/auth/api/projects/{id}
- http://web.socem.plymouth.ac.uk/COMP2001/bjames/auth/api/accounts
- http://web.socem.plymouth.ac.uk/COMP2001/bjames/auth/api/accounts/renewtoken
- http://web.socem.plymouth.ac.uk/COMP2001/bjames/auth/api/accounts/login
- http://web.socem.plymouth.ac.uk/COMP2001/bjames/auth/api/accounts/{id}/projects


|  emailAddress |password   |
|---|---|
| test@test.com  |  Test123! |
| test1@test.com  |  Test123! |
| test2@test.com  | Test123!  |
| test3@test.com  |  Test123! |
| test4@test.com  |  Test123! |
| test5@test.com  |  Test123! |
| test6@test.com  | Test123!  |
| test7@test.com | Test123!  |
| test8@test.com | Test123!  |  
  
example body for /auth/api/accounts/login: 
```
{
    "emailAddress": "test5@test.com",
    "password": "Test123!"
}
```


## Views, Trigger, Stored Procedure
#### Trigger

```
CREATE TRIGGER tr_ProjectsAuditRecord ON [Projects]
AFTER UPDATE
AS
BEGIN
    INSERT INTO [ProjectsAudit]
        (Id, ApplicationUserId, Title, Description, Year, UpdatedOn)
        SELECT d.Id, d.ApplicationUserId, d.Title, d.Description, d.Year, GETDATE()
        FROM DELETED AS d
END
```
#### View

```
CREATE VIEW vw_LectureProgrammeProjectCount AS
    SELECT Count(*) as Count, ProgrammeId FROM [AspNetUsers]
    INNER JOIN [Projects]
    ON [AspNetUsers].[Id]=[Projects].[ApplicationUserId]
    GROUP BY ProgrammeId
```
Usage:
```SELECT * FROM vw_LectureProgrammeProjectCount where ProgrammeId = 1```  
```SELECT * FROM vw_LectureProgrammeProjectCount where ProgrammeId = 2```  
```SELECT * FROM vw_LectureProgrammeProjectCount where ProgrammeId = 3```  

### Stored procedures
##### Delete
```
CREATE PROCEDURE [dbo].[sp_DeleteProject]
    @ProjectId INT
    AS
    BEGIN
        SET NOCOUNT ON;
        DELETE FROM [dbo].[Projects] where [Projects].Id = @ProjectId; 
    END
```
##### Insert/Create
```
CREATE PROCEDURE [dbo].[sp_InsertProject]
    @StudentId INT,
    @Title NVARCHAR(30),
    @Description NVARCHAR(500),
    @Year NVARCHAR(30)  
    AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @ProjectId INT
        DECLARE @studentCount INT

        BEGIN TRANSACTION InsertProject
            SELECT @studentCount = COUNT(*) FROM [AspNetUsers]
            WHERE [AspNetUsers].Id = @StudentId

            IF @studentCount < 1 /* Checks if student exists*/
                BEGIN
                    ROLLBACK TRANSACTION InsertProject
                END
            
            INSERT INTO [Projects] (ApplicationUserId, Title, Description, Year)
            VALUES(@StudentId, @Title, @Description, @Year)                
            SET @ProjectId = SCOPE_IDENTITY()
            SELECT * FROM [dbo].[Projects] WHERE [Projects].Id = @ProjectId 
            COMMIT TRANSACTION                       
    END
```
##### Update
```
    CREATE PROCEDURE [dbo].[sp_UpdateProject]
    @StudentId INT,
    @Title NVARCHAR(30),
    @Description NVARCHAR(500),
    @Year NVARCHAR(30),
    @ProjectId INT
    AS
    BEGIN
        SET NOCOUNT ON;

        UPDATE [dbo].[Projects]
        SET ApplicationUserId = @StudentId,
        Title = @Title,
        Description = @Description,
        Year = @Year
        WHERE [Projects].Id = @ProjectId

        SELECT * FROM [Projects] WHERE [Projects].Id = @ProjectId
    END
```

## References

- MOQ used to make mock classes in unit tests : https://github.com/moq/moq4
