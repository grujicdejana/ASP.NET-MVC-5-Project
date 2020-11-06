namespace VidlyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5f7ca409-66af-454b-9200-b34e9c5605ef', N'admin@vidly.com', 0, N'ACMybPr7Jw80xkh9+NZyC9zKQctnax5ZN7t0bdkb26DX/RLaLjTVSQXsNKU9bQOu+g==', N'd55cf103-6a03-4edb-961c-6b0ddef720f8', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd4fdf27d-0252-440d-bdaa-d540c0744d76', N'guest@vidly.com', 0, N'AJqw7VOkijJ+fua4TP7aS5j+AcSdYGkRtlFsbXQWdNi6BJijbsPC+oOQBC9CLEaFQg==', N'8a6ef015-2a12-4d20-b2b1-f4efdff30acc', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1d07ba5e-e36c-4dad-820e-fb082ced0f76', N'CanManageMovie')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'89d87c52-3d69-44cf-b570-98c6e0ea29a2', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5f7ca409-66af-454b-9200-b34e9c5605ef', N'89d87c52-3d69-44cf-b570-98c6e0ea29a2')

");
        }
        
        public override void Down()
        {
        }
    }
}
