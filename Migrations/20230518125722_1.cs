﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplom.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameFood = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealTimes",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsernameChangeLimit = table.Column<int>(type: "int", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_AspNetUsers_Id",
                        column: x => x.Id,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildHouse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildCount = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_User_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaults",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaultName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaults_User_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuFoods",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountPerUnit = table.Column<float>(type: "real", nullable: false),
                    Supply = table.Column<float>(type: "real", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    MealTimeId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuFoods_Meals_MealId",
                        column: x => x.MealId,
                        principalSchema: "Identity",
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFoods_MealTimes_MealTimeId",
                        column: x => x.MealTimeId,
                        principalSchema: "Identity",
                        principalTable: "MealTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFoods_Menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "Identity",
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFoods_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Identity",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaultNotes",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdVault = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaultNotes_Vaults_IdVault",
                        column: x => x.IdVault,
                        principalSchema: "Identity",
                        principalTable: "Vaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arrivals",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdFood = table.Column<int>(type: "int", nullable: false),
                    FoodCount = table.Column<double>(type: "float", nullable: true),
                    IdVaultNote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrivals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arrivals_Foods_IdFood",
                        column: x => x.IdFood,
                        principalSchema: "Identity",
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arrivals_VaultNotes_IdVaultNote",
                        column: x => x.IdVaultNote,
                        principalSchema: "Identity",
                        principalTable: "VaultNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChildHouses",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameHouse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildCount = table.Column<int>(type: "int", nullable: false),
                    IdVaultNote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildHouses_VaultNotes_IdVaultNote",
                        column: x => x.IdVaultNote,
                        principalSchema: "Identity",
                        principalTable: "VaultNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreviousBalances",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartBalance = table.Column<double>(type: "float", nullable: true),
                    EndBalance = table.Column<double>(type: "float", nullable: true),
                    IdVaultNote = table.Column<int>(type: "int", nullable: false),
                    IdFood = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreviousBalances_Foods_IdFood",
                        column: x => x.IdFood,
                        principalSchema: "Identity",
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreviousBalances_VaultNotes_IdVaultNote",
                        column: x => x.IdVaultNote,
                        principalSchema: "Identity",
                        principalTable: "VaultNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductConsumptions",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FoodCountChild = table.Column<float>(type: "real", nullable: true),
                    FoodCountKid = table.Column<float>(type: "real", nullable: true),
                    IdFood = table.Column<int>(type: "int", nullable: false),
                    IdVaultNote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductConsumptions_Foods_IdFood",
                        column: x => x.IdFood,
                        principalSchema: "Identity",
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductConsumptions_VaultNotes_IdVaultNote",
                        column: x => x.IdVaultNote,
                        principalSchema: "Identity",
                        principalTable: "VaultNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Foods",
                columns: new[] { "Id", "NameFood" },
                values: new object[,]
                {
                    { 1, "апельсины" },
                    { 53, "сахар" },
                    { 52, "ряженка" },
                    { 51, "рис" },
                    { 50, "пшено" },
                    { 49, "повидло" },
                    { 47, "печень говяжья" },
                    { 54, "свекла" },
                    { 46, "паста томатная" },
                    { 44, "мясо птицы" },
                    { 43, "мясо говядина на кости" },
                    { 42, "мясо говядина без кости" },
                    { 41, "мука пшеничная" },
                    { 40, "морковь" },
                    { 39, "молоко сухое" },
                    { 45, "огурцы соленые" },
                    { 38, "молоко сгущенное" },
                    { 55, "сельдь слабосоленая" },
                    { 57, "снежок (кг)" },
                    { 71, "яйцо" },
                    { 70, "яблоки" },
                    { 69, "шиповник" },
                    { 68, "чернослив" },
                    { 67, "чай" },
                    { 66, "хлеб ржаной" },
                    { 56, "сметана" },
                    { 65, "хлеб пшеничный" },
                    { 63, "творог" },
                    { 62, "сыр" },
                    { 61, "сушки" },
                    { 60, "соль йодированная" },
                    { 59, "с.м ягода" },
                    { 58, "сок фруктовый" },
                    { 64, "тушенка" },
                    { 37, "молоко свежее 2,5%" },
                    { 48, "печенье" },
                    { 35, "масло сливочное" },
                    { 16, "картофель" },
                    { 15, "капуста" },
                    { 14, "какао" },
                    { 13, "икра кабачковая" },
                    { 12, "изюм" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Foods",
                columns: new[] { "Id", "NameFood" },
                values: new object[,]
                {
                    { 11, "зефир" },
                    { 10, "дрожжи" },
                    { 9, "груши" },
                    { 8, "горошек консервированный" },
                    { 36, "минтай с/м б/г" },
                    { 6, "горбуша свежемороженная б/г" },
                    { 5, "геркулес" },
                    { 4, "вафли" },
                    { 3, "ванилин" },
                    { 2, "вермешель" },
                    { 17, "кефир" },
                    { 18, "кисель" },
                    { 7, "горох" },
                    { 20, "крахмал" },
                    { 19, "компотная смесь (сухофрукты)" },
                    { 34, "масло растительное" },
                    { 33, "макаронные изделия" },
                    { 31, "лимоны" },
                    { 30, "лавровый лист" },
                    { 29, "лимонная кислота" },
                    { 28, "кукуруза консервированная" },
                    { 32, "лук" },
                    { 26, "крупа пшеничная" },
                    { 25, "крупа перловая" },
                    { 21, "кофейный напиток" },
                    { 24, "крупа манная" },
                    { 23, "крупа кукурузная" },
                    { 22, "крупа гречневая" },
                    { 27, "крупа ячневая" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "MealTimes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Завтрак  " },
                    { 2, "2-ой завтрак" },
                    { 3, "Обед" },
                    { 5, "Ужин" },
                    { 4, "Полдник" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Meals",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 13, "Снежок" },
                    { 19, "Чай с сахаром" },
                    { 16, "Рагу овощное" },
                    { 15, "Котлета/биточек рыбный" },
                    { 14, "Ватрушка с повидлом" },
                    { 12, "Хлеб   ржаной" },
                    { 5, "Капуста припущенная" },
                    { 10, "Компот из смеси сухофруктов" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Meals",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 9, "Гречка отварная с маслом" },
                    { 8, "Соус красный основной" },
                    { 7, "Суфле из печени" },
                    { 6, "Борщ со свежей капустой, картофелем на м/к бульоне со сметаной" },
                    { 4, "Сок фруктовый" },
                    { 3, "Батон с маслом (20/5)" },
                    { 2, "Кофейный напиток с молоком" },
                    { 1, "Суп молочный с макаронными изделиями" },
                    { 11, "Хлеб  пшеничный" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Units",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Л" },
                    { 1, "Кг" },
                    { 3, "шт" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arrivals_IdFood",
                schema: "Identity",
                table: "Arrivals",
                column: "IdFood");

            migrationBuilder.CreateIndex(
                name: "IX_Arrivals_IdVaultNote",
                schema: "Identity",
                table: "Arrivals",
                column: "IdVaultNote");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChildHouses_IdVaultNote",
                schema: "Identity",
                table: "ChildHouses",
                column: "IdVaultNote");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoods_MealId",
                schema: "Identity",
                table: "MenuFoods",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoods_MealTimeId",
                schema: "Identity",
                table: "MenuFoods",
                column: "MealTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoods_MenuId",
                schema: "Identity",
                table: "MenuFoods",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoods_UnitId",
                schema: "Identity",
                table: "MenuFoods",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_IdUser",
                schema: "Identity",
                table: "Menus",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousBalances_IdFood",
                schema: "Identity",
                table: "PreviousBalances",
                column: "IdFood");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousBalances_IdVaultNote",
                schema: "Identity",
                table: "PreviousBalances",
                column: "IdVaultNote");

            migrationBuilder.CreateIndex(
                name: "IX_ProductConsumptions_IdFood",
                schema: "Identity",
                table: "ProductConsumptions",
                column: "IdFood");

            migrationBuilder.CreateIndex(
                name: "IX_ProductConsumptions_IdVaultNote",
                schema: "Identity",
                table: "ProductConsumptions",
                column: "IdVaultNote");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultNotes_IdVault",
                schema: "Identity",
                table: "VaultNotes",
                column: "IdVault");

            migrationBuilder.CreateIndex(
                name: "IX_Vaults_IdUser",
                schema: "Identity",
                table: "Vaults",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arrivals",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ChildHouses",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "MenuFoods",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "PreviousBalances",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ProductConsumptions",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Meals",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "MealTimes",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Foods",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "VaultNotes",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Vaults",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Identity");
        }
    }
}
