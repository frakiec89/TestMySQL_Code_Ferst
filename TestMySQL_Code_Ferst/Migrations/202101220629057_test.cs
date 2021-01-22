namespace TestMySQL_Code_Ferst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grups",
                c => new
                    {
                        GrupsId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.GrupsId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        GrupsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grups", t => t.GrupsId, cascadeDelete: true)
                .Index(t => t.GrupsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "GrupsId", "dbo.Grups");
            DropIndex("dbo.Users", new[] { "GrupsId" });
            DropTable("dbo.Users");
            DropTable("dbo.Grups");
        }
    }
}
