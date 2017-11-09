using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITWEBExercise5.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_ComponentType_ComponentTypeId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentType_Category_CategoryId",
                table: "ComponentType");

            migrationBuilder.DropIndex(
                name: "IX_ComponentType_CategoryId",
                table: "ComponentType");

            migrationBuilder.DropIndex(
                name: "IX_Category_ComponentTypeId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ComponentType");

            migrationBuilder.DropColumn(
                name: "ComponentTypeId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "CategoryComponentType",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ComponentTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryComponentType", x => new { x.CategoryId, x.ComponentTypeId });
                    table.ForeignKey(
                        name: "FK_CategoryComponentType_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryComponentType_ComponentType_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentType",
                        principalColumn: "ComponentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryComponentType_ComponentTypeId",
                table: "CategoryComponentType",
                column: "ComponentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryComponentType");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ComponentType",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ComponentTypeId",
                table: "Category",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentType_CategoryId",
                table: "ComponentType",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ComponentTypeId",
                table: "Category",
                column: "ComponentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_ComponentType_ComponentTypeId",
                table: "Category",
                column: "ComponentTypeId",
                principalTable: "ComponentType",
                principalColumn: "ComponentTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentType_Category_CategoryId",
                table: "ComponentType",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
