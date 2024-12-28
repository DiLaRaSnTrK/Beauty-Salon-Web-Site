using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WEB3.Migrations
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
                    adminid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.adminid);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customerid);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    serviceid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    servicename = table.Column<string>(type: "text", nullable: false),
                    serviceduration = table.Column<int>(type: "integer", nullable: false),
                    serviceprice = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.serviceid);
                });

            migrationBuilder.CreateTable(
                name: "appointmentstatus",
                columns: table => new
                {
                    approvalstatus = table.Column<string>(type: "text", nullable: false),
                    adminid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointmentstatus", x => x.approvalstatus);
                    table.ForeignKey(
                        name: "FK_appointmentstatus_admin_adminid",
                        column: x => x.adminid,
                        principalTable: "admin",
                        principalColumn: "adminid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    appointmentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeid = table.Column<int>(type: "integer", nullable: false),
                    serviceid = table.Column<int>(type: "integer", nullable: false),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    appointmentdatetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    totalprice = table.Column<int>(type: "integer", nullable: false),
                    process = table.Column<int>(type: "integer", nullable: false),
                    approvalstatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.appointmentid);
                    table.ForeignKey(
                        name: "FK_appointments_customer_customerid",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointments_services_serviceid",
                        column: x => x.serviceid,
                        principalTable: "services",
                        principalColumn: "serviceid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsServices",
                columns: table => new
                {
                    appointmentsappointmentid = table.Column<int>(type: "integer", nullable: false),
                    servicesserviceid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsServices", x => new { x.appointmentsappointmentid, x.servicesserviceid });
                    table.ForeignKey(
                        name: "FK_AppointmentsServices_appointments_appointmentsappointmentid",
                        column: x => x.appointmentsappointmentid,
                        principalTable: "appointments",
                        principalColumn: "appointmentid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentsServices_services_servicesserviceid",
                        column: x => x.servicesserviceid,
                        principalTable: "services",
                        principalColumn: "serviceid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsEmployees",
                columns: table => new
                {
                    appointmentsappointmentid = table.Column<int>(type: "integer", nullable: false),
                    employeesemployeeid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsEmployees", x => new { x.appointmentsappointmentid, x.employeesemployeeid });
                    table.ForeignKey(
                        name: "FK_AppointmentsEmployees_appointments_appointmentsappointmentid",
                        column: x => x.appointmentsappointmentid,
                        principalTable: "appointments",
                        principalColumn: "appointmentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customerappointments",
                columns: table => new
                {
                    customerappointmentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    appointmentid = table.Column<int>(type: "integer", nullable: false),
                    approvalstatus = table.Column<string>(type: "text", nullable: false),
                    serviceid = table.Column<int>(type: "integer", nullable: false),
                    employeeid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerappointments", x => x.customerappointmentid);
                    table.ForeignKey(
                        name: "FK_customerappointments_appointments_appointmentid",
                        column: x => x.appointmentid,
                        principalTable: "appointments",
                        principalColumn: "appointmentid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customerappointments_customer_customerid",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customerappointments_services_serviceid",
                        column: x => x.serviceid,
                        principalTable: "services",
                        principalColumn: "serviceid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeavailability",
                columns: table => new
                {
                    availabilityid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeid = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeavailability", x => x.availabilityid);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employeeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    expertise = table.Column<string>(type: "text", nullable: true),
                    dailyearnings = table.Column<int>(type: "integer", nullable: false),
                    skills = table.Column<string>(type: "text", nullable: true),
                    serviceid = table.Column<int>(type: "integer", nullable: false),
                    prolificacy = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.employeeid);
                    table.ForeignKey(
                        name: "FK_employees_employeeavailability_serviceid",
                        column: x => x.serviceid,
                        principalTable: "employeeavailability",
                        principalColumn: "availabilityid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employees_services_serviceid",
                        column: x => x.serviceid,
                        principalTable: "services",
                        principalColumn: "serviceid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_customerid",
                table: "appointments",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_employeeid",
                table: "appointments",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_serviceid",
                table: "appointments",
                column: "serviceid");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsEmployees_employeesemployeeid",
                table: "AppointmentsEmployees",
                column: "employeesemployeeid");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsServices_servicesserviceid",
                table: "AppointmentsServices",
                column: "servicesserviceid");

            migrationBuilder.CreateIndex(
                name: "IX_appointmentstatus_adminid",
                table: "appointmentstatus",
                column: "adminid");

            migrationBuilder.CreateIndex(
                name: "IX_customerappointments_appointmentid",
                table: "customerappointments",
                column: "appointmentid");

            migrationBuilder.CreateIndex(
                name: "IX_customerappointments_customerid",
                table: "customerappointments",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_customerappointments_employeeid",
                table: "customerappointments",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_customerappointments_serviceid",
                table: "customerappointments",
                column: "serviceid");

            migrationBuilder.CreateIndex(
                name: "IX_employeeavailability_employeeid",
                table: "employeeavailability",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_employees_serviceid",
                table: "employees",
                column: "serviceid");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_employees_employeeid",
                table: "appointments",
                column: "employeeid",
                principalTable: "employees",
                principalColumn: "employeeid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsEmployees_employees_employeesemployeeid",
                table: "AppointmentsEmployees",
                column: "employeesemployeeid",
                principalTable: "employees",
                principalColumn: "employeeid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_customerappointments_employees_employeeid",
                table: "customerappointments",
                column: "employeeid",
                principalTable: "employees",
                principalColumn: "employeeid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employeeavailability_employees_employeeid",
                table: "employeeavailability",
                column: "employeeid",
                principalTable: "employees",
                principalColumn: "employeeid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employeeavailability_employees_employeeid",
                table: "employeeavailability");

            migrationBuilder.DropTable(
                name: "AppointmentsEmployees");

            migrationBuilder.DropTable(
                name: "AppointmentsServices");

            migrationBuilder.DropTable(
                name: "appointmentstatus");

            migrationBuilder.DropTable(
                name: "customerappointments");

            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "employeeavailability");

            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
