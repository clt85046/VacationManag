namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Holiday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Holidays", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Holidays", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Holidays", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Holidays", "Date");
        }
    }
}
