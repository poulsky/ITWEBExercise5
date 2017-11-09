using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITWEBExercise5.Migrations
{
    public partial class ManyToManySecondTry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponentType_Category_CategoryId",
                table: "CategoryComponentType");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponentType_ComponentType_ComponentTypeId",
                table: "CategoryComponentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryComponentType",
                table: "CategoryComponentType");

            migrationBuilder.RenameTable(
                name: "CategoryComponentType",
                newName: "CategoryComponentTypes");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryComponentType_ComponentTypeId",
                table: "CategoryComponentTypes",
                newName: "IX_CategoryComponentTypes_ComponentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryComponentTypes",
                table: "CategoryComponentTypes",
                columns: new[] { "CategoryId", "ComponentTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComponentTypes_Category_CategoryId",
                table: "CategoryComponentTypes",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComponentTypes_ComponentType_ComponentTypeId",
                table: "CategoryComponentTypes",
                column: "ComponentTypeId",
                principalTable: "ComponentType",
                principalColumn: "ComponentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponentTypes_Category_CategoryId",
                table: "CategoryComponentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponentTypes_ComponentType_ComponentTypeId",
                table: "CategoryComponentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryComponentTypes",
                table: "CategoryComponentTypes");

            migrationBuilder.RenameTable(
                name: "CategoryComponentTypes",
                newName: "CategoryComponentType");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryComponentTypes_ComponentTypeId",
                table: "CategoryComponentType",
                newName: "IX_CategoryComponentType_ComponentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryComponentType",
                table: "CategoryComponentType",
                columns: new[] { "CategoryId", "ComponentTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComponentType_Category_CategoryId",
                table: "CategoryComponentType",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComponentType_ComponentType_ComponentTypeId",
                table: "CategoryComponentType",
                column: "ComponentTypeId",
                principalTable: "ComponentType",
                principalColumn: "ComponentTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
