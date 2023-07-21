using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Infracstructure.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_Category_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Role_Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Book_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Book_Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Book_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Book_Price = table.Column<float>(type: "real", nullable: false),
                    Book_Quantity = table.Column<int>(type: "int", nullable: false),
                    Book_Year_Public = table.Column<int>(type: "int", nullable: false),
                    Book_ISBN = table.Column<int>(type: "int", nullable: true),
                    Is_Book_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Book_Id);
                    table.ForeignKey(
                        name: "FK_Book_Category_Category_Id",
                        column: x => x.Category_Id,
                        principalTable: "Category",
                        principalColumn: "Category_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false),
                    User_Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_User_Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_User_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_Id);
                    table.ForeignKey(
                        name: "FK_User_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Image_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Image_Id);
                    table.ForeignKey(
                        name: "FK_Image_Book_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Book",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Request_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Request_Image_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Request_Book_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Request_Quantity = table.Column<int>(type: "int", nullable: false),
                    Request_Price = table.Column<float>(type: "real", nullable: false),
                    Request_Amount = table.Column<float>(type: "real", nullable: false),
                    Request_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request_Date_Done = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Request_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Request_Id);
                    table.ForeignKey(
                        name: "FK_Request_Book_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Book",
                        principalColumn: "Book_Id");
                });

            migrationBuilder.CreateTable(
                name: "Importation",
                columns: table => new
                {
                    Import_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Import_Date_Done = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Import_Quantity = table.Column<int>(type: "int", nullable: false),
                    Import_Amount = table.Column<float>(type: "real", nullable: false),
                    Is_Import_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Importation", x => x.Import_Id);
                    table.ForeignKey(
                        name: "FK_Importation_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Inventory_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inventory_Quantity = table.Column<int>(type: "int", nullable: false),
                    Inventory_Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inventory_Date_Into = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Is_Inventory_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Inventory_Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Book_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Book",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventory_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Order_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order_Quantity = table.Column<int>(type: "int", nullable: false),
                    Order_Amount = table.Column<float>(type: "real", nullable: false),
                    Order_Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_Customer_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_Customer_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_Order_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Order_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportationDetail",
                columns: table => new
                {
                    Import_Detail_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Import_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Import_Detail_Quantity = table.Column<int>(type: "int", nullable: false),
                    Import_Detail_Price = table.Column<float>(type: "real", nullable: false),
                    Import_Detail_Amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportationDetail", x => x.Import_Detail_Id);
                    table.ForeignKey(
                        name: "FK_ImportationDetail_Book_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Book",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportationDetail_Importation_Import_Id",
                        column: x => x.Import_Id,
                        principalTable: "Importation",
                        principalColumn: "Import_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Order_Detail_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order_Detail_Quantity = table.Column<int>(type: "int", nullable: false),
                    Order_Detail_Amount = table.Column<float>(type: "real", nullable: false),
                    Order_Detail_Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Order_Detail_Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Book_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Book",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Order",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Category_Id",
                table: "Book",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Book_Id",
                table: "Image",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Importation_User_Id",
                table: "Importation",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImportationDetail_Book_Id",
                table: "ImportationDetail",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImportationDetail_Import_Id",
                table: "ImportationDetail",
                column: "Import_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Book_Id",
                table: "Inventory",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_User_Id",
                table: "Inventory",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_User_Id",
                table: "Order",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_Book_Id",
                table: "OrderDetail",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_Order_Id",
                table: "OrderDetail",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Book_Id",
                table: "Request",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_Id",
                table: "User",
                column: "Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "ImportationDetail");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Importation");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
