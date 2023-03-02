using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    admin_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fisrt_name = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    last_name = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    email = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    password = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admin__43AA4141D396DC5C", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "banner",
                columns: table => new
                {
                    banner_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false),
                    text = table.Column<string>(type: "text", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__banner__10373C341AC474CF", x => x.banner_id);
                });

            migrationBuilder.CreateTable(
                name: "cms_page",
                columns: table => new
                {
                    csm_page_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cms_page__44CC3BCBC27FB954", x => x.csm_page_id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    country_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ISO = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__country__7E8CD055605D1A23", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "mission_theme",
                columns: table => new
                {
                    mission_theme_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValueSql: "((1))"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission___4925C5ACAB231112", x => x.mission_theme_id);
                });

            migrationBuilder.CreateTable(
                name: "skill",
                columns: table => new
                {
                    skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skill_name = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValueSql: "((1))"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__skill__FBBA8379DF4A0FA3", x => x.skill_id);
                });

            migrationBuilder.CreateTable(
                name: "user_token",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    UserToken = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Generated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    Used = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_token", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    city_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__city__031491A85C6EEDB1", x => x.city_id);
                    table.ForeignKey(
                        name: "FK__city__country_id__1C873BEC",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "country_id");
                });

            migrationBuilder.CreateTable(
                name: "mission",
                columns: table => new
                {
                    mission_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    theme_id = table.Column<long>(type: "bigint", nullable: false),
                    city_id = table.Column<long>(type: "bigint", nullable: false),
                    country_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    short_description = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    mission_type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    organization_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    organization_details = table.Column<string>(type: "text", nullable: true),
                    availability = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission__B5419AB2B0F1C163", x => x.mission_id);
                    table.ForeignKey(
                        name: "FK__mission__city_id__41B8C09B",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "city_id");
                    table.ForeignKey(
                        name: "FK__mission__country__42ACE4D4",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "country_id");
                    table.ForeignKey(
                        name: "FK__mission__theme_i__40C49C62",
                        column: x => x.theme_id,
                        principalTable: "mission_theme",
                        principalColumn: "mission_theme_id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    last_name = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    email = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    avatar = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true),
                    why_i_volunteer = table.Column<string>(type: "text", nullable: true),
                    employee_id = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    department = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    city_id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "((1))"),
                    country_id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "((1))"),
                    profile_text = table.Column<string>(type: "text", nullable: true),
                    linked_in_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__B9BE370FCD73FD03", x => x.user_id);
                    table.ForeignKey(
                        name: "FK__users__city_id__4959E263",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "city_id");
                    table.ForeignKey(
                        name: "FK__users__country_i__4A4E069C",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "country_id");
                });

            migrationBuilder.CreateTable(
                name: "goal_mission",
                columns: table => new
                {
                    goal_mission_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    goal_objective_text = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    goal_value = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__goal_mis__358E02C75DE3B800", x => x.goal_mission_id);
                    table.ForeignKey(
                        name: "FK__goal_miss__missi__603D47BB",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                });

            migrationBuilder.CreateTable(
                name: "mission_document",
                columns: table => new
                {
                    mission_document_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    document_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    document_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    document_path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission___E80E0CC8A78482DD", x => x.mission_document_id);
                    table.ForeignKey(
                        name: "FK__mission_d__missi__6D9742D9",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                });

            migrationBuilder.CreateTable(
                name: "mission_media",
                columns: table => new
                {
                    mission_media_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    media_name = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true),
                    media_type = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: true),
                    media_path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    media_default = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission___848A78E865CB8954", x => x.mission_media_id);
                    table.ForeignKey(
                        name: "FK__mission_m__missi__7CD98669",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                });

            migrationBuilder.CreateTable(
                name: "mission_skill",
                columns: table => new
                {
                    mission_skill_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skill_id = table.Column<int>(type: "int", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission___82712008E8ADA4DF", x => x.mission_skill_id);
                    table.ForeignKey(
                        name: "FK__mission_s__missi__4CF5691D",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__mission_s__skill__4C0144E4",
                        column: x => x.skill_id,
                        principalTable: "skill",
                        principalColumn: "skill_id");
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    comment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    approval_status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValueSql: "('PENDING')"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comment__E7957687F236B288", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK__comment__mission__52E34C9D",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__comment__user_id__51EF2864",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "favorite_mission",
                columns: table => new
                {
                    favourite_mission_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__favorite__94E4D8CAD44FBBA8", x => x.favourite_mission_id);
                    table.ForeignKey(
                        name: "FK__favorite___missi__5A846E65",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__favorite___user___59904A2C",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "mission_applicatoin",
                columns: table => new
                {
                    mission_application_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    applied_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    approval_status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission___DF92838A809146CF", x => x.mission_application_id);
                    table.ForeignKey(
                        name: "FK__mission_a__missi__67DE6983",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__mission_a__user___66EA454A",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "mission_invite",
                columns: table => new
                {
                    mission_invite_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    from_user_id = table.Column<long>(type: "bigint", nullable: false),
                    to_user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission___A97ED158BA1BD986", x => x.mission_invite_id);
                    table.ForeignKey(
                        name: "FK__mission_i__from___73501C2F",
                        column: x => x.from_user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__mission_i__missi__753864A1",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__mission_i__to_us__74444068",
                        column: x => x.to_user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "mission_rating",
                columns: table => new
                {
                    mission_rating_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission___320E5172235F45E7", x => x.mission_rating_id);
                    table.ForeignKey(
                        name: "FK__mission_r__missi__047AA831",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__mission_r__user___038683F8",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "story",
                columns: table => new
                {
                    story_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValueSql: "('DRAFT')"),
                    published_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__story__66339C56E50F7247", x => x.story_id);
                    table.ForeignKey(
                        name: "FK__story__mission_i__220B0B18",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__story__user_id__2116E6DF",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "timesheet",
                columns: table => new
                {
                    timesheet_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    time = table.Column<TimeSpan>(type: "time", nullable: true),
                    action = table.Column<int>(type: "int", nullable: true),
                    date_volunteered = table.Column<DateTime>(type: "datetime", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValueSql: "('SUBMIT_FOR_APPROVAL')"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__timeshee__7BBF506820EDF912", x => x.timesheet_id);
                    table.ForeignKey(
                        name: "FK__timesheet__missi__37FA4C37",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__timesheet__user___370627FE",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_skill",
                columns: table => new
                {
                    user_skill_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    skill_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_ski__FD3B576B9A4E246A", x => x.user_skill_id);
                    table.ForeignKey(
                        name: "FK__user_skil__skill__567ED357",
                        column: x => x.skill_id,
                        principalTable: "skill",
                        principalColumn: "skill_id");
                    table.ForeignKey(
                        name: "FK__user_skil__user___558AAF1E",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "story_invite",
                columns: table => new
                {
                    story_invite_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    story_id = table.Column<long>(type: "bigint", nullable: false),
                    from_user_id = table.Column<long>(type: "bigint", nullable: false),
                    to_user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__story_in__044978678FFE745F", x => x.story_invite_id);
                    table.ForeignKey(
                        name: "FK__story_inv__from___27C3E46E",
                        column: x => x.from_user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__story_inv__story__29AC2CE0",
                        column: x => x.story_id,
                        principalTable: "story",
                        principalColumn: "story_id");
                    table.ForeignKey(
                        name: "FK__story_inv__to_us__28B808A7",
                        column: x => x.to_user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "story_media",
                columns: table => new
                {
                    story_media_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    story_id = table.Column<long>(type: "bigint", nullable: false),
                    media_type = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    media_path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__story_me__29BD053CEDFDD8AD", x => x.story_media_id);
                    table.ForeignKey(
                        name: "FK__story_med__story__2F650636",
                        column: x => x.story_id,
                        principalTable: "story",
                        principalColumn: "story_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_city_country_id",
                table: "city",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_mission_id",
                table: "comment",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_user_id",
                table: "comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_favorite_mission_mission_id",
                table: "favorite_mission",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_favorite_mission_user_id",
                table: "favorite_mission",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_goal_mission_mission_id",
                table: "goal_mission",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_city_id",
                table: "mission",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_country_id",
                table: "mission",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_theme_id",
                table: "mission",
                column: "theme_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_applicatoin_mission_id",
                table: "mission_applicatoin",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_applicatoin_user_id",
                table: "mission_applicatoin",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_document_mission_id",
                table: "mission_document",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_invite_from_user_id",
                table: "mission_invite",
                column: "from_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_invite_mission_id",
                table: "mission_invite",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_invite_to_user_id",
                table: "mission_invite",
                column: "to_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_media_mission_id",
                table: "mission_media",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_rating_mission_id",
                table: "mission_rating",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_rating_user_id",
                table: "mission_rating",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_skill_mission_id",
                table: "mission_skill",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_skill_skill_id",
                table: "mission_skill",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_story_mission_id",
                table: "story",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_story_user_id",
                table: "story",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_story_invite_from_user_id",
                table: "story_invite",
                column: "from_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_story_invite_story_id",
                table: "story_invite",
                column: "story_id");

            migrationBuilder.CreateIndex(
                name: "IX_story_invite_to_user_id",
                table: "story_invite",
                column: "to_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_story_media_story_id",
                table: "story_media",
                column: "story_id");

            migrationBuilder.CreateIndex(
                name: "IX_timesheet_mission_id",
                table: "timesheet",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_timesheet_user_id",
                table: "timesheet",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_skill_skill_id",
                table: "user_skill",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_skill_user_id",
                table: "user_skill",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_city_id",
                table: "users",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_country_id",
                table: "users",
                column: "country_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "banner");

            migrationBuilder.DropTable(
                name: "cms_page");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "favorite_mission");

            migrationBuilder.DropTable(
                name: "goal_mission");

            migrationBuilder.DropTable(
                name: "mission_applicatoin");

            migrationBuilder.DropTable(
                name: "mission_document");

            migrationBuilder.DropTable(
                name: "mission_invite");

            migrationBuilder.DropTable(
                name: "mission_media");

            migrationBuilder.DropTable(
                name: "mission_rating");

            migrationBuilder.DropTable(
                name: "mission_skill");

            migrationBuilder.DropTable(
                name: "story_invite");

            migrationBuilder.DropTable(
                name: "story_media");

            migrationBuilder.DropTable(
                name: "timesheet");

            migrationBuilder.DropTable(
                name: "user_skill");

            migrationBuilder.DropTable(
                name: "user_token");

            migrationBuilder.DropTable(
                name: "story");

            migrationBuilder.DropTable(
                name: "skill");

            migrationBuilder.DropTable(
                name: "mission");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "mission_theme");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
