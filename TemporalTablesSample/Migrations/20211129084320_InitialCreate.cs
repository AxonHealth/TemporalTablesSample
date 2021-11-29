using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemporalTablesSample.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom"),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SettingHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[] { 1, "TaxRate", 10.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SettingHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");
        }
    }
}
