using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectsAPI.Migrations
{
    public partial class spcreateproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[CreateProject]
                        @Student_ID INT,
                        @Title NVARCHAR(30),
                        @Description NVARCHAR(500),
                        @Year NVARCHAR(30),
                        @ResponseMessage INT OUTPUT
                    AS
                    BEGIN
                    DECLARE @studentCount INT
                    SET NOCOUNT ON;

                    SELECT @studentCount = COUNT(*) FROM [dbo].[AspNetUsers]
                    WHERE [dbo].[AspNetUsers].Id = @Student_ID

                    IF @studentCount > 1
                        BEGIN
                            INSERT INTO [dbo].[Projects] (ApplicationUserId, Title, Description, Year) VALUES(@Student_ID, @Title, @Description, @Year)
                            SET @ResponseMessage = SCOPE_IDENTITY()
                        END
                    ELSE
                        BEGIN
                            SET @ResponseMessage = -1
                        END
                    END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}