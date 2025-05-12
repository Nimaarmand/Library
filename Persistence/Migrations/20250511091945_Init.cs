using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "BookCategories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    bookId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCategories_BookCategories_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "dbo",
                        principalTable: "BookCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookCategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookCategories_BookCategoriesId",
                        column: x => x.BookCategoriesId,
                        principalSchema: "dbo",
                        principalTable: "BookCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Books_BookId",
                        column: x => x.BookId,
                        principalSchema: "dbo",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationId = table.Column<long>(type: "bigint", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Books_BookId",
                        column: x => x.BookId,
                        principalSchema: "dbo",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "dbo",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryStatus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryState = table.Column<bool>(type: "bit", nullable: false),
                    BookBack = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationId1 = table.Column<long>(type: "bigint", nullable: true),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryStatus_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalSchema: "dbo",
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryStatus_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "dbo",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryStatus_Reservations_ReservationId1",
                        column: x => x.ReservationId1,
                        principalSchema: "dbo",
                        principalTable: "Reservations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_ParentId",
                schema: "dbo",
                table: "BookCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCategoriesId",
                schema: "dbo",
                table: "Books",
                column: "BookCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_BookId",
                schema: "dbo",
                table: "Deliveries",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ReservationId",
                schema: "dbo",
                table: "Deliveries",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryStatus_DeliveryId",
                schema: "dbo",
                table: "DeliveryStatus",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryStatus_ReservationId",
                schema: "dbo",
                table: "DeliveryStatus",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryStatus_ReservationId1",
                schema: "dbo",
                table: "DeliveryStatus",
                column: "ReservationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BookId",
                schema: "dbo",
                table: "Reservations",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Deliveries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BookCategories",
                schema: "dbo");
        }
    }
}
