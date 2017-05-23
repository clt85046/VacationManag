namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class policies_init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Policies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinYearsOfOffice = c.Int(nullable: false),
                        MaxYearsOfOffice = c.Int(nullable: false),
                        PaidDayOffs = c.Int(nullable: false),
                        PaidSickness = c.Int(nullable: false),
                        UnPaidDayOffs = c.Int(nullable: false),
                        UnPaidSickness = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Policies");
        }
    }
}
