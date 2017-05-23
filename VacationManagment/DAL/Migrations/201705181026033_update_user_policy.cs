namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user_policy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PaidDayOffs", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "PaidSickness", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UnPaidDayOffs", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UnPaidSickness", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UnPaidSickness");
            DropColumn("dbo.Users", "UnPaidDayOffs");
            DropColumn("dbo.Users", "PaidSickness");
            DropColumn("dbo.Users", "PaidDayOffs");
        }
    }
}
