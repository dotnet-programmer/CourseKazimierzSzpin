namespace SendEmail.WebApp.NetFramework.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class ChangeInEmailSent : DbMigration
	{
		public override void Up()
		{
			AlterColumn("dbo.EmailSents", "SenderName", c => c.String());
			AlterColumn("dbo.EmailSents", "SenderEmail", c => c.String());
		}

		public override void Down()
		{
			AlterColumn("dbo.EmailSents", "SenderEmail", c => c.String(nullable: false));
			AlterColumn("dbo.EmailSents", "SenderName", c => c.String(nullable: false));
		}
	}
}