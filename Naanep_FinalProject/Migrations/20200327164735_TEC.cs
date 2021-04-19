using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class TEC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    CandidateNumber = table.Column<string>(nullable: false),
                    CandidateFirstName = table.Column<string>(nullable: true),
                    CandidateMiddleName = table.Column<string>(nullable: true),
                    CandidateLastName = table.Column<string>(nullable: true),
                    CandidateFullName = table.Column<string>(nullable: true),
                    CandidateGender = table.Column<string>(nullable: true),
                    CandidateAddress = table.Column<string>(nullable: true),
                    CandidateBirthDate = table.Column<DateTime>(nullable: false),
                    CandidateContactNumber = table.Column<string>(nullable: true),
                    CandidateEmail = table.Column<string>(nullable: true),
                    CandidateAge = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.CandidateNumber);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<string>(nullable: false),
                    CourseName = table.Column<string>(nullable: true),
                    CourseDescription = table.Column<string>(nullable: true),
                    CourseHours = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    DayId = table.Column<string>(nullable: false),
                    Acronym = table.Column<string>(nullable: true),
                    DayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    QualificationId = table.Column<string>(nullable: false),
                    QualificationCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.QualificationId);
                });

            migrationBuilder.CreateTable(
                name: "JobHistory",
                columns: table => new
                {
                    HistoryId = table.Column<string>(nullable: false),
                    WorkedHours = table.Column<double>(nullable: true),
                    WorkedFrom = table.Column<DateTime>(nullable: false),
                    WorkedTo = table.Column<DateTime>(nullable: true),
                    CandidateNumber = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHistory", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_JobHistory_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "CandidateNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobHistory_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSession",
                columns: table => new
                {
                    SessionId = table.Column<string>(nullable: false),
                    CourseId = table.Column<string>(nullable: true),
                    SessionFee = table.Column<double>(nullable: false),
                    SessionCapacity = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSession", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_TrainingSession_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    CertificationId = table.Column<string>(nullable: false),
                    CertificationDate = table.Column<DateTime>(nullable: false),
                    CandidateNumber = table.Column<string>(nullable: true),
                    QualificationId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.CertificationId);
                    table.ForeignKey(
                        name: "FK_Certification_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "CandidateNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certification_Qualification_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Opening",
                columns: table => new
                {
                    OpeningNumber = table.Column<string>(nullable: false),
                    StartingDate = table.Column<DateTime>(nullable: false),
                    AnticipatedEndDate = table.Column<DateTime>(nullable: false),
                    HourlyPay = table.Column<double>(nullable: false),
                    QualificationId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opening", x => x.OpeningNumber);
                    table.ForeignKey(
                        name: "FK_Opening_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opening_Qualification_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prerequisite",
                columns: table => new
                {
                    PrerequisiteId = table.Column<string>(nullable: false),
                    CourseId = table.Column<string>(nullable: true),
                    QualificationId = table.Column<string>(nullable: true),
                    RequirementFor = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequisite", x => x.PrerequisiteId);
                    table.ForeignKey(
                        name: "FK_Prerequisite_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerequisite_Qualification_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredCandidate",
                columns: table => new
                {
                    RegistrationId = table.Column<string>(nullable: false),
                    RegisteredDate = table.Column<DateTime>(nullable: false),
                    CandidateNumber = table.Column<string>(nullable: true),
                    SessionId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredCandidate", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_RegisteredCandidate_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "CandidateNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisteredCandidate_TrainingSession_SessionId",
                        column: x => x.SessionId,
                        principalTable: "TrainingSession",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<string>(nullable: false),
                    SessionId = table.Column<string>(nullable: true),
                    DayId = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedule_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_TrainingSession_SessionId",
                        column: x => x.SessionId,
                        principalTable: "TrainingSession",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Placement",
                columns: table => new
                {
                    PlacementId = table.Column<string>(nullable: false),
                    PlacementStatus = table.Column<string>(nullable: true),
                    CandidateNumber = table.Column<string>(nullable: true),
                    OpeningNumber = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placement", x => x.PlacementId);
                    table.ForeignKey(
                        name: "FK_Placement_Candidate_CandidateNumber",
                        column: x => x.CandidateNumber,
                        principalTable: "Candidate",
                        principalColumn: "CandidateNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Placement_Opening_OpeningNumber",
                        column: x => x.OpeningNumber,
                        principalTable: "Opening",
                        principalColumn: "OpeningNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certification_CandidateNumber",
                table: "Certification",
                column: "CandidateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_QualificationId",
                table: "Certification",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistory_CandidateNumber",
                table: "JobHistory",
                column: "CandidateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistory_CompanyId",
                table: "JobHistory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Opening_CompanyId",
                table: "Opening",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Opening_QualificationId",
                table: "Opening",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Placement_CandidateNumber",
                table: "Placement",
                column: "CandidateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Placement_OpeningNumber",
                table: "Placement",
                column: "OpeningNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_CourseId",
                table: "Prerequisite",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_QualificationId",
                table: "Prerequisite",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCandidate_CandidateNumber",
                table: "RegisteredCandidate",
                column: "CandidateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCandidate_SessionId",
                table: "RegisteredCandidate",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_DayId",
                table: "Schedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_SessionId",
                table: "Schedule",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSession_CourseId",
                table: "TrainingSession",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "JobHistory");

            migrationBuilder.DropTable(
                name: "Placement");

            migrationBuilder.DropTable(
                name: "Prerequisite");

            migrationBuilder.DropTable(
                name: "RegisteredCandidate");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Opening");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "TrainingSession");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
