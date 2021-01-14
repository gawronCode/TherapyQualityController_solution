using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TherapyQualityController.Migrations
{
    public partial class SeedQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "W skali od 1 do 5 jak bardzo boli cię gardło?", 1 });
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "W skali od 1 do 5 jaki jest Twój poziom energii?", 1 });
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "Oceń swoje samopoczucie w skali od 1 do 5", 1 });
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "W skali od 1 do 5 jak silny jest kaszel?", 1 });
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "W skali od 1 do 5 jak silny jest katar?", 1 });
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "W skali od 1 do 5 jak silny jest ból głowy?", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 1);
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 1);
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 1);
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 1);
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 1);
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 1);
        }
    }
}
