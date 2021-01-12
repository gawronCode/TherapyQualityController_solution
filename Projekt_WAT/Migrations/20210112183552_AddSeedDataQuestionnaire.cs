using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TherapyQualityController.Migrations
{
    public partial class AddSeedDataQuestionnaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table:"Questionnaires", 
                                        columns: new[] {"Name","CreationDate"},
                                        values: new Object[]{"Ankieta testowa", DateTime.Now });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Questionnaires", "Name", "Ankieta testowa");
        }
    }
}
