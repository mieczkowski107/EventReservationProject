using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChaningNamingConevntionForTablesAndColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_AspNetUsers_UserId",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Session_SessionId",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Event_EventId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionLimit_Session_SessionId",
                table: "SessionLimit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionLimit",
                table: "SessionLimit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session",
                table: "Session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registration",
                table: "Registration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "SessionLimit",
                newName: "SESSIONLIMIT");

            migrationBuilder.RenameTable(
                name: "Session",
                newName: "SESSION");

            migrationBuilder.RenameTable(
                name: "Registration",
                newName: "REGISTRATION");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "EVENT");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "ASPNETUSERTOKENS");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "ASPNETUSERS");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "ASPNETUSERROLES");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "ASPNETUSERLOGINS");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "ASPNETUSERCLAIMS");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "ASPNETROLES");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "ASPNETROLECLAIMS");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "SESSIONLIMIT",
                newName: "VERSION");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "SESSIONLIMIT",
                newName: "UPDATEDAT");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "SESSIONLIMIT",
                newName: "SESSIONID");

            migrationBuilder.RenameColumn(
                name: "MaxParticipants",
                table: "SESSIONLIMIT",
                newName: "MAXPARTICIPANTS");

            migrationBuilder.RenameColumn(
                name: "CurrentReserved",
                table: "SESSIONLIMIT",
                newName: "CURRENTRESERVED");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "SESSIONLIMIT",
                newName: "CREATEDAT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SESSIONLIMIT",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_SessionLimit_SessionId",
                table: "SESSIONLIMIT",
                newName: "IX_SESSIONLIMIT_SESSIONID");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "SESSION",
                newName: "VERSION");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "SESSION",
                newName: "UPDATEDAT");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "SESSION",
                newName: "STARTTIME");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SESSION",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "SESSION",
                newName: "EVENTID");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "SESSION",
                newName: "DURATION");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "SESSION",
                newName: "DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "SESSION",
                newName: "CREATEDAT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SESSION",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Session_EventId",
                table: "SESSION",
                newName: "IX_SESSION_EVENTID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "REGISTRATION",
                newName: "USERID");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "REGISTRATION",
                newName: "UPDATEDAT");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "REGISTRATION",
                newName: "SESSIONID");

            migrationBuilder.RenameColumn(
                name: "RegistrationStatus",
                table: "REGISTRATION",
                newName: "REGISTRATIONSTATUS");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "REGISTRATION",
                newName: "CREATEDAT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "REGISTRATION",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Registration_UserId",
                table: "REGISTRATION",
                newName: "IX_REGISTRATION_USERID");

            migrationBuilder.RenameIndex(
                name: "IX_Registration_SessionId",
                table: "REGISTRATION",
                newName: "IX_REGISTRATION_SESSIONID");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "EVENT",
                newName: "VERSION");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "EVENT",
                newName: "UPDATEDAT");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "EVENT",
                newName: "STARTTIME");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EVENT",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "EVENT",
                newName: "LOCATION");

            migrationBuilder.RenameColumn(
                name: "IsOverLappingAllowed",
                table: "EVENT",
                newName: "ISOVERLAPPINGALLOWED");

            migrationBuilder.RenameColumn(
                name: "EventSessionStatus",
                table: "EVENT",
                newName: "EVENTSESSIONSTATUS");

            migrationBuilder.RenameColumn(
                name: "EventEmail",
                table: "EVENT",
                newName: "EVENTEMAIL");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "EVENT",
                newName: "ENDTIME");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "EVENT",
                newName: "DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EVENT",
                newName: "CREATEDAT");

            migrationBuilder.RenameColumn(
                name: "CoordinatorSurname",
                table: "EVENT",
                newName: "COORDINATORSURNAME");

            migrationBuilder.RenameColumn(
                name: "CoordinatorPhone",
                table: "EVENT",
                newName: "COORDINATORPHONE");

            migrationBuilder.RenameColumn(
                name: "CoordinatorName",
                table: "EVENT",
                newName: "COORDINATORNAME");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EVENT",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ASPNETUSERTOKENS",
                newName: "VALUE");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ASPNETUSERTOKENS",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "ASPNETUSERTOKENS",
                newName: "LOGINPROVIDER");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ASPNETUSERTOKENS",
                newName: "USERID");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "ASPNETUSERS",
                newName: "USERNAME");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "ASPNETUSERS",
                newName: "TWOFACTORENABLED");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "ASPNETUSERS",
                newName: "SECURITYSTAMP");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "ASPNETUSERS",
                newName: "PHONENUMBERCONFIRMED");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "ASPNETUSERS",
                newName: "PHONENUMBER");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "ASPNETUSERS",
                newName: "PASSWORDHASH");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "ASPNETUSERS",
                newName: "NORMALIZEDUSERNAME");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "ASPNETUSERS",
                newName: "NORMALIZEDEMAIL");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "ASPNETUSERS",
                newName: "LOCKOUTEND");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "ASPNETUSERS",
                newName: "LOCKOUTENABLED");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ASPNETUSERS",
                newName: "LASTNAME");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "ASPNETUSERS",
                newName: "FIRSTNAME");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "ASPNETUSERS",
                newName: "EMAILCONFIRMED");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ASPNETUSERS",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "ASPNETUSERS",
                newName: "COUNTRY");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "ASPNETUSERS",
                newName: "CONCURRENCYSTAMP");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "ASPNETUSERS",
                newName: "ACCESSFAILEDCOUNT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ASPNETUSERS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "ASPNETUSERROLES",
                newName: "ROLEID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ASPNETUSERROLES",
                newName: "USERID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "ASPNETUSERROLES",
                newName: "IX_ASPNETUSERROLES_ROLEID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ASPNETUSERLOGINS",
                newName: "USERID");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "ASPNETUSERLOGINS",
                newName: "PROVIDERDISPLAYNAME");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "ASPNETUSERLOGINS",
                newName: "PROVIDERKEY");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "ASPNETUSERLOGINS",
                newName: "LOGINPROVIDER");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "ASPNETUSERLOGINS",
                newName: "IX_ASPNETUSERLOGINS_USERID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ASPNETUSERCLAIMS",
                newName: "USERID");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "ASPNETUSERCLAIMS",
                newName: "CLAIMVALUE");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "ASPNETUSERCLAIMS",
                newName: "CLAIMTYPE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ASPNETUSERCLAIMS",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "ASPNETUSERCLAIMS",
                newName: "IX_ASPNETUSERCLAIMS_USERID");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "ASPNETROLES",
                newName: "NORMALIZEDNAME");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ASPNETROLES",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "ASPNETROLES",
                newName: "CONCURRENCYSTAMP");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ASPNETROLES",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "ASPNETROLECLAIMS",
                newName: "ROLEID");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "ASPNETROLECLAIMS",
                newName: "CLAIMVALUE");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "ASPNETROLECLAIMS",
                newName: "CLAIMTYPE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ASPNETROLECLAIMS",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "ASPNETROLECLAIMS",
                newName: "IX_ASPNETROLECLAIMS_ROLEID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SESSIONLIMIT",
                table: "SESSIONLIMIT",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SESSION",
                table: "SESSION",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_REGISTRATION",
                table: "REGISTRATION",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EVENT",
                table: "EVENT",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ASPNETUSERTOKENS",
                table: "ASPNETUSERTOKENS",
                columns: new[] { "USERID", "LOGINPROVIDER", "NAME" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ASPNETUSERS",
                table: "ASPNETUSERS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ASPNETUSERROLES",
                table: "ASPNETUSERROLES",
                columns: new[] { "USERID", "ROLEID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ASPNETUSERLOGINS",
                table: "ASPNETUSERLOGINS",
                columns: new[] { "LOGINPROVIDER", "PROVIDERKEY" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ASPNETUSERCLAIMS",
                table: "ASPNETUSERCLAIMS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ASPNETROLES",
                table: "ASPNETROLES",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ASPNETROLECLAIMS",
                table: "ASPNETROLECLAIMS",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ASPNETUSERS",
                column: "NORMALIZEDUSERNAME",
                unique: true,
                filter: "\"NORMALIZEDUSERNAME\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ASPNETROLES",
                column: "NORMALIZEDNAME",
                unique: true,
                filter: "\"NORMALIZEDNAME\" IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ASPNETROLECLAIMS_ASPNETROLES_ROLEID",
                table: "ASPNETROLECLAIMS",
                column: "ROLEID",
                principalTable: "ASPNETROLES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ASPNETUSERCLAIMS_ASPNETUSERS_USERID",
                table: "ASPNETUSERCLAIMS",
                column: "USERID",
                principalTable: "ASPNETUSERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ASPNETUSERLOGINS_ASPNETUSERS_USERID",
                table: "ASPNETUSERLOGINS",
                column: "USERID",
                principalTable: "ASPNETUSERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ASPNETUSERROLES_ASPNETROLES_ROLEID",
                table: "ASPNETUSERROLES",
                column: "ROLEID",
                principalTable: "ASPNETROLES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ASPNETUSERROLES_ASPNETUSERS_USERID",
                table: "ASPNETUSERROLES",
                column: "USERID",
                principalTable: "ASPNETUSERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ASPNETUSERTOKENS_ASPNETUSERS_USERID",
                table: "ASPNETUSERTOKENS",
                column: "USERID",
                principalTable: "ASPNETUSERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRATION_ASPNETUSERS_USERID",
                table: "REGISTRATION",
                column: "USERID",
                principalTable: "ASPNETUSERS",
                principalColumn: "INT_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRATION_SESSION_SESSIONID",
                table: "REGISTRATION",
                column: "SESSIONID",
                principalTable: "SESSION",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SESSION_EVENT_EVENTID",
                table: "SESSION",
                column: "EVENTID",
                principalTable: "EVENT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SESSIONLIMIT_SESSION_SESSIONID",
                table: "SESSIONLIMIT",
                column: "SESSIONID",
                principalTable: "SESSION",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASPNETROLECLAIMS_ASPNETROLES_ROLEID",
                table: "ASPNETROLECLAIMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ASPNETUSERCLAIMS_ASPNETUSERS_USERID",
                table: "ASPNETUSERCLAIMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ASPNETUSERLOGINS_ASPNETUSERS_USERID",
                table: "ASPNETUSERLOGINS");

            migrationBuilder.DropForeignKey(
                name: "FK_ASPNETUSERROLES_ASPNETROLES_ROLEID",
                table: "ASPNETUSERROLES");

            migrationBuilder.DropForeignKey(
                name: "FK_ASPNETUSERROLES_ASPNETUSERS_USERID",
                table: "ASPNETUSERROLES");

            migrationBuilder.DropForeignKey(
                name: "FK_ASPNETUSERTOKENS_ASPNETUSERS_USERID",
                table: "ASPNETUSERTOKENS");

            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRATION_ASPNETUSERS_USERID",
                table: "REGISTRATION");

            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRATION_SESSION_SESSIONID",
                table: "REGISTRATION");

            migrationBuilder.DropForeignKey(
                name: "FK_SESSION_EVENT_EVENTID",
                table: "SESSION");

            migrationBuilder.DropForeignKey(
                name: "FK_SESSIONLIMIT_SESSION_SESSIONID",
                table: "SESSIONLIMIT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SESSIONLIMIT",
                table: "SESSIONLIMIT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SESSION",
                table: "SESSION");

            migrationBuilder.DropPrimaryKey(
                name: "PK_REGISTRATION",
                table: "REGISTRATION");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EVENT",
                table: "EVENT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ASPNETUSERTOKENS",
                table: "ASPNETUSERTOKENS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ASPNETUSERS",
                table: "ASPNETUSERS");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "ASPNETUSERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ASPNETUSERROLES",
                table: "ASPNETUSERROLES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ASPNETUSERLOGINS",
                table: "ASPNETUSERLOGINS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ASPNETUSERCLAIMS",
                table: "ASPNETUSERCLAIMS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ASPNETROLES",
                table: "ASPNETROLES");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "ASPNETROLES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ASPNETROLECLAIMS",
                table: "ASPNETROLECLAIMS");

            migrationBuilder.RenameTable(
                name: "SESSIONLIMIT",
                newName: "SessionLimit");

            migrationBuilder.RenameTable(
                name: "SESSION",
                newName: "Session");

            migrationBuilder.RenameTable(
                name: "REGISTRATION",
                newName: "Registration");

            migrationBuilder.RenameTable(
                name: "EVENT",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "ASPNETUSERTOKENS",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "ASPNETUSERS",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ASPNETUSERROLES",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "ASPNETUSERLOGINS",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "ASPNETUSERCLAIMS",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "ASPNETROLES",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "ASPNETROLECLAIMS",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameColumn(
                name: "VERSION",
                table: "SessionLimit",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "UPDATEDAT",
                table: "SessionLimit",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "SESSIONID",
                table: "SessionLimit",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "MAXPARTICIPANTS",
                table: "SessionLimit",
                newName: "MaxParticipants");

            migrationBuilder.RenameColumn(
                name: "CURRENTRESERVED",
                table: "SessionLimit",
                newName: "CurrentReserved");

            migrationBuilder.RenameColumn(
                name: "CREATEDAT",
                table: "SessionLimit",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SessionLimit",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_SESSIONLIMIT_SESSIONID",
                table: "SessionLimit",
                newName: "IX_SessionLimit_SessionId");

            migrationBuilder.RenameColumn(
                name: "VERSION",
                table: "Session",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "UPDATEDAT",
                table: "Session",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "STARTTIME",
                table: "Session",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "Session",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "EVENTID",
                table: "Session",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "DURATION",
                table: "Session",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "DESCRIPTION",
                table: "Session",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CREATEDAT",
                table: "Session",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Session",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_SESSION_EVENTID",
                table: "Session",
                newName: "IX_Session_EventId");

            migrationBuilder.RenameColumn(
                name: "USERID",
                table: "Registration",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UPDATEDAT",
                table: "Registration",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "SESSIONID",
                table: "Registration",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "REGISTRATIONSTATUS",
                table: "Registration",
                newName: "RegistrationStatus");

            migrationBuilder.RenameColumn(
                name: "CREATEDAT",
                table: "Registration",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Registration",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_REGISTRATION_USERID",
                table: "Registration",
                newName: "IX_Registration_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_REGISTRATION_SESSIONID",
                table: "Registration",
                newName: "IX_Registration_SessionId");

            migrationBuilder.RenameColumn(
                name: "VERSION",
                table: "Event",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "UPDATEDAT",
                table: "Event",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "STARTTIME",
                table: "Event",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "Event",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LOCATION",
                table: "Event",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "ISOVERLAPPINGALLOWED",
                table: "Event",
                newName: "IsOverLappingAllowed");

            migrationBuilder.RenameColumn(
                name: "EVENTSESSIONSTATUS",
                table: "Event",
                newName: "EventSessionStatus");

            migrationBuilder.RenameColumn(
                name: "EVENTEMAIL",
                table: "Event",
                newName: "EventEmail");

            migrationBuilder.RenameColumn(
                name: "ENDTIME",
                table: "Event",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "DESCRIPTION",
                table: "Event",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CREATEDAT",
                table: "Event",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "COORDINATORSURNAME",
                table: "Event",
                newName: "CoordinatorSurname");

            migrationBuilder.RenameColumn(
                name: "COORDINATORPHONE",
                table: "Event",
                newName: "CoordinatorPhone");

            migrationBuilder.RenameColumn(
                name: "COORDINATORNAME",
                table: "Event",
                newName: "CoordinatorName");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Event",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "VALUE",
                table: "AspNetUserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "AspNetUserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LOGINPROVIDER",
                table: "AspNetUserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "USERID",
                table: "AspNetUserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "USERNAME",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "TWOFACTORENABLED",
                table: "AspNetUsers",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "SECURITYSTAMP",
                table: "AspNetUsers",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "PHONENUMBERCONFIRMED",
                table: "AspNetUsers",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "PHONENUMBER",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PASSWORDHASH",
                table: "AspNetUsers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "NORMALIZEDUSERNAME",
                table: "AspNetUsers",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "NORMALIZEDEMAIL",
                table: "AspNetUsers",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "LOCKOUTEND",
                table: "AspNetUsers",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "LOCKOUTENABLED",
                table: "AspNetUsers",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "LASTNAME",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FIRSTNAME",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "EMAILCONFIRMED",
                table: "AspNetUsers",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                table: "AspNetUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "COUNTRY",
                table: "AspNetUsers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "CONCURRENCYSTAMP",
                table: "AspNetUsers",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "ACCESSFAILEDCOUNT",
                table: "AspNetUsers",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AspNetUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ROLEID",
                table: "AspNetUserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "USERID",
                table: "AspNetUserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ASPNETUSERROLES_ROLEID",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "USERID",
                table: "AspNetUserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "PROVIDERDISPLAYNAME",
                table: "AspNetUserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "PROVIDERKEY",
                table: "AspNetUserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "LOGINPROVIDER",
                table: "AspNetUserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "IX_ASPNETUSERLOGINS_USERID",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "USERID",
                table: "AspNetUserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CLAIMVALUE",
                table: "AspNetUserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "CLAIMTYPE",
                table: "AspNetUserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AspNetUserClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ASPNETUSERCLAIMS_USERID",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "NORMALIZEDNAME",
                table: "AspNetRoles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "AspNetRoles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CONCURRENCYSTAMP",
                table: "AspNetRoles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AspNetRoles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ROLEID",
                table: "AspNetRoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "CLAIMVALUE",
                table: "AspNetRoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "CLAIMTYPE",
                table: "AspNetRoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AspNetRoleClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ASPNETROLECLAIMS_ROLEID",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionLimit",
                table: "SessionLimit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session",
                table: "Session",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registration",
                table: "Registration",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "\"NormalizedUserName\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "\"NormalizedName\" IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_AspNetUsers_UserId",
                table: "Registration",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "INT_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Session_SessionId",
                table: "Registration",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Event_EventId",
                table: "Session",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionLimit_Session_SessionId",
                table: "SessionLimit",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
