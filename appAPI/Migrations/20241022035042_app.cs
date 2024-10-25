using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class taodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Parent_Id = table.Column<int>(type: "int", nullable: false),
                    Dept = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feature_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Template = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    In_Order = table.Column<int>(type: "int", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Short_title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Parent_id = table.Column<long>(type: "bigint", nullable: false),
                    Dept = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
<<<<<<< HEAD:appAPI/Migrations/20241018185522_taodatabase.cs
                    Color_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
=======
                    Color_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
>>>>>>> 25-10 Dis, Phân Trang.:appAPI/Migrations/20241022035042_app.cs
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designer",
                columns: table => new
                {
                    id_Designer = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    short_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image_library = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    meta_data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designer", x => x.id_Designer);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type_of_promotion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Mine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Ext = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Optin_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Option_value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Q_As",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Q_As", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "seo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Post_Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Textile_Technologies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Textile_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EmailVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RememberToken = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxDiscountValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
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
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activity_history",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Log_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_history_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
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
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menuitems",
<<<<<<< HEAD:appAPI/Migrations/20241018185522_taodatabase.cs
=======
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Parent = table.Column<long>(type: "bigint", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuitems", x => x.id);
                    table.ForeignKey(
                        name: "FK_menuitems_menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_post",
>>>>>>> 25-10 Dis, Phân Trang.:appAPI/Migrations/20241022035042_app.cs
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
<<<<<<< HEAD:appAPI/Migrations/20241018185522_taodatabase.cs
                    Label = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Parent = table.Column<long>(type: "bigint", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuitems", x => x.id);
                    table.ForeignKey(
                        name: "FK_menuitems_menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_post",
=======
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    product_video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Short_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_library = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feature_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Post_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ColorId = table.Column<long>(type: "bigint", nullable: true),
                    MaterialId = table.Column<long>(type: "bigint", nullable: true),
                    SizeId = table.Column<long>(type: "bigint", nullable: true),
                    StyleId = table.Column<long>(type: "bigint", nullable: true),
                    Textile_technologyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_post_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Designer_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Designer",
                        principalColumn: "id_Designer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_post_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Styles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Styles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Textile_Technologies_Textile_technologyId",
                        column: x => x.Textile_technologyId,
                        principalTable: "Textile_Technologies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Address",
>>>>>>> 25-10 Dis, Phân Trang.:appAPI/Migrations/20241022035042_app.cs
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
<<<<<<< HEAD:appAPI/Migrations/20241018185522_taodatabase.cs
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    product_video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Short_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_library = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feature_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Post_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ColorId = table.Column<long>(type: "bigint", nullable: true),
                    MaterialId = table.Column<long>(type: "bigint", nullable: true),
                    SizeId = table.Column<long>(type: "bigint", nullable: true),
                    StyleId = table.Column<long>(type: "bigint", nullable: true),
                    Textile_technologyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_post_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Designer_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Designer",
                        principalColumn: "id_Designer",
=======
                    User_Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Steet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward_commune = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province_city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Set_as_default = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carts_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
>>>>>>> 25-10 Dis, Phân Trang.:appAPI/Migrations/20241022035042_app.cs
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_post_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Styles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Styles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Textile_Technologies_Textile_technologyId",
                        column: x => x.Textile_technologyId,
                        principalTable: "Textile_Technologies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
<<<<<<< HEAD:appAPI/Migrations/20241018185522_taodatabase.cs
                    User_Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward_commune = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Set_as_default = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
=======
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    User_id = table.Column<long>(type: "bigint", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Phone_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
>>>>>>> 25-10 Dis, Phân Trang.:appAPI/Migrations/20241022035042_app.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_users_User_id",
                        column: x => x.User_id,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "wishlist",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<long>(type: "bigint", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wishlist_users_User_id",
                        column: x => x.User_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    User_id = table.Column<long>(type: "bigint", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Phone_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_users_User_id",
                        column: x => x.User_id,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "wishlist",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<long>(type: "bigint", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wishlist_users_User_id",
                        column: x => x.User_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userVouchers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    VoucherId = table.Column<long>(type: "bigint", nullable: false),
                    IsApplied = table.Column<bool>(type: "bit", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userVouchers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userVouchers_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "banners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Meta_data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductPostId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_banners_product_post_ProductPostId",
                        column: x => x.ProductPostId,
                        principalTable: "product_post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_id = table.Column<long>(type: "bigint", nullable: false),
                    User_id = table.Column<long>(type: "bigint", nullable: false),
                    User_admin_id = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author_IP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Parent = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_product_post_Post_id",
                        column: x => x.Post_id,
                        principalTable: "product_post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_users_User_id",
                        column: x => x.User_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_Id = table.Column<long>(type: "bigint", nullable: false),
                    Category_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_categories_categories_Category_Id",
                        column: x => x.Category_Id,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_categories_product_post_Post_Id",
                        column: x => x.Post_Id,
                        principalTable: "product_post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tag_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_tags_product_post_Post_Id",
                        column: x => x.Post_Id,
                        principalTable: "product_post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_tags_tags_Tag_Id",
                        column: x => x.Tag_Id,
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_variants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_Id = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Sku = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Regular_price = table.Column<long>(type: "bigint", nullable: true),
                    Sale_price = table.Column<long>(type: "bigint", nullable: true),
                    Stock_quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color_id = table.Column<long>(type: "bigint", nullable: false),
                    Textile_technology_id = table.Column<long>(type: "bigint", nullable: false),
                    Material_id = table.Column<long>(type: "bigint", nullable: false),
                    Size_id = table.Column<long>(type: "bigint", nullable: false),
                    Style_id = table.Column<long>(type: "bigint", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AttributesId = table.Column<long>(type: "bigint", nullable: true),
                    Designerid_Designer = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_variants", x => x.Id);
<<<<<<< HEAD:appAPI/Migrations/20241018185522_taodatabase.cs
                    table.ForeignKey(
                        name: "FK_product_variants_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attributes",
                        principalColumn: "Id");
                    table.ForeignKey(
=======
                    table.ForeignKey(
                        name: "FK_product_variants_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attributes",
                        principalColumn: "Id");
                    table.ForeignKey(
>>>>>>> 25-10 Dis, Phân Trang.:appAPI/Migrations/20241022035042_app.cs
                        name: "FK_product_variants_Color_Color_id",
                        column: x => x.Color_id,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_variants_Designer_Designerid_Designer",
                        column: x => x.Designerid_Designer,
                        principalTable: "Designer",
                        principalColumn: "id_Designer");
                    table.ForeignKey(
                        name: "FK_product_variants_Materials_Material_id",
                        column: x => x.Material_id,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_variants_product_post_Post_Id",
                        column: x => x.Post_Id,
                        principalTable: "product_post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_variants_Sizes_Size_id",
                        column: x => x.Size_id,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_variants_Styles_Style_id",
                        column: x => x.Style_id,
                        principalTable: "Styles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_variants_Textile_Technologies_Textile_technology_id",
                        column: x => x.Textile_technology_id,
                        principalTable: "Textile_Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_trackings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_trackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_trackings_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderVouchers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    VoucherId = table.Column<long>(type: "bigint", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderVouchers_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderVouchers_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cartdetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_id = table.Column<long>(type: "bigint", nullable: false),
                    Cart_id = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartdetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartdetails_carts_Cart_id",
                        column: x => x.Cart_id,
                        principalTable: "carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cartdetails_product_variants_Product_id",
                        column: x => x.Product_id,
                        principalTable: "product_variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductVariants_Id = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RegularPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingStockQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_details_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_details_product_variants_ProductVariants_Id",
                        column: x => x.ProductVariants_Id,
                        principalTable: "product_variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "p_Variants_Discounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P_variants_Id = table.Column<long>(type: "bigint", nullable: false),
                    Discount_Id = table.Column<long>(type: "bigint", nullable: false),
                    Old_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    New_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_p_Variants_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_p_Variants_Discounts_Discount_Discount_Id",
                        column: x => x.Discount_Id,
                        principalTable: "Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_p_Variants_Discounts_product_variants_P_variants_Id",
                        column: x => x.P_variants_Id,
                        principalTable: "product_variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_attributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Variants_Id = table.Column<long>(type: "bigint", nullable: false),
                    Attribute_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_attributes_Attributes_Attribute_Id",
                        column: x => x.Attribute_Id,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_attributes_product_variants_Product_Variants_Id",
                        column: x => x.Product_Variants_Id,
                        principalTable: "product_variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Variants_Wishlists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_variants_id = table.Column<long>(type: "bigint", nullable: false),
                    Wishlist_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Variants_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Variants_Wishlists_product_variants_Product_variants_id",
                        column: x => x.Product_variants_id,
                        principalTable: "product_variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Variants_Wishlists_wishlist_Wishlist_id",
                        column: x => x.Wishlist_id,
                        principalTable: "wishlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_history_AccountId",
                table: "Activity_history",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_banners_ProductPostId",
                table: "banners",
                column: "ProductPostId",
                unique: true,
                filter: "[ProductPostId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cartdetails_Cart_id",
                table: "Cartdetails",
                column: "Cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cartdetails_Product_id",
                table: "Cartdetails",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_UserId",
                table: "carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Post_id",
                table: "Comments",
                column: "Post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_User_id",
                table: "Comments",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_menuitems_MenuId",
                table: "menuitems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_OrderId",
                table: "order_details",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_ProductVariants_Id",
                table: "order_details",
                column: "ProductVariants_Id");

            migrationBuilder.CreateIndex(
                name: "IX_order_trackings_OrderId",
                table: "order_trackings",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_User_id",
                table: "orders",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVouchers_OrderId",
                table: "OrderVouchers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVouchers_VoucherId",
                table: "OrderVouchers",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_p_Variants_Discounts_Discount_Id",
                table: "p_Variants_Discounts",
                column: "Discount_Id");

            migrationBuilder.CreateIndex(
                name: "IX_p_Variants_Discounts_P_variants_Id",
                table: "p_Variants_Discounts",
                column: "P_variants_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_categories_Category_Id",
                table: "post_categories",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_categories_Post_Id",
                table: "post_categories",
                column: "Post_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_tags_Post_Id",
                table: "post_tags",
                column: "Post_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_tags_Tag_Id",
                table: "post_tags",
                column: "Tag_Id");

            migrationBuilder.CreateIndex(
                name: "IX_product_attributes_Attribute_Id",
                table: "product_attributes",
                column: "Attribute_Id");

            migrationBuilder.CreateIndex(
                name: "IX_product_attributes_Product_Variants_Id",
                table: "product_attributes",
                column: "Product_Variants_Id");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_AccountId",
                table: "product_post",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_AuthorId",
                table: "product_post",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_ColorId",
                table: "product_post",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_MaterialId",
                table: "product_post",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_SizeId",
                table: "product_post",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_StyleId",
                table: "product_post",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_Textile_technologyId",
                table: "product_post",
                column: "Textile_technologyId");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_AttributesId",
                table: "product_variants",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Color_id",
                table: "product_variants",
                column: "Color_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Designerid_Designer",
                table: "product_variants",
                column: "Designerid_Designer");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Material_id",
                table: "product_variants",
                column: "Material_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Post_Id",
                table: "product_variants",
                column: "Post_Id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Size_id",
                table: "product_variants",
                column: "Size_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Style_id",
                table: "product_variants",
                column: "Style_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Textile_technology_id",
                table: "product_variants",
                column: "Textile_technology_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Variants_Wishlists_Product_variants_id",
                table: "Product_Variants_Wishlists",
                column: "Product_variants_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Variants_Wishlists_Wishlist_id",
                table: "Product_Variants_Wishlists",
                column: "Wishlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_userVouchers_UserId",
                table: "userVouchers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userVouchers_VoucherId",
                table: "userVouchers",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_wishlist_User_id",
                table: "wishlist",
                column: "User_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity_history");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "banners");

            migrationBuilder.DropTable(
                name: "Cartdetails");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "menuitems");

            migrationBuilder.DropTable(
                name: "options");

            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "order_trackings");

            migrationBuilder.DropTable(
                name: "OrderVouchers");

            migrationBuilder.DropTable(
                name: "p_Variants_Discounts");

            migrationBuilder.DropTable(
                name: "post_categories");

            migrationBuilder.DropTable(
                name: "post_tags");

            migrationBuilder.DropTable(
                name: "product_attributes");

            migrationBuilder.DropTable(
                name: "Product_Variants_Wishlists");

            migrationBuilder.DropTable(
                name: "Q_As");

            migrationBuilder.DropTable(
                name: "seo");

            migrationBuilder.DropTable(
                name: "userVouchers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "product_variants");

            migrationBuilder.DropTable(
                name: "wishlist");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "product_post");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Designer");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropTable(
                name: "Textile_Technologies");
        }
    }
}
