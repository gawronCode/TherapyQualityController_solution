using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TherapyQualityController.Migrations
{
    public partial class AddSeedDataQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "Czy lubisz placki?", 2 });
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "Tak czy nie?", 2 });
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "Jak się czujesz?", 2 });
            migrationBuilder.InsertData(table: "Questions",
                columns: new[] { "Contents", "QuestionnaireId" },
                values: new Object[] { "Jaki masz poziom energii?", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 2);
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 2);
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 2);
            migrationBuilder.DeleteData("Questions", "QuestionnaireId", 2);
        }
    }
}
