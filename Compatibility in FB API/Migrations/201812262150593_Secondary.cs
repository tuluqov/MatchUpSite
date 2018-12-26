namespace MatchUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Secondary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecondaryAbilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SelfEsteem = c.Int(nullable: false),
                        Moneymaking = c.Int(nullable: false),
                        Talent = c.Int(nullable: false),
                        Tenacity = c.Int(nullable: false),
                        DomesticBliss = c.Int(nullable: false),
                        Stability = c.Int(nullable: false),
                        Spirituality = c.Int(nullable: false),
                        Temperament = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.People", "SecondaryAbilitiesId", c => c.Int());
            AddColumn("dbo.Stars", "SecondaryAbilitiesId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "SecondaryAbilitiesId", c => c.Int());
            CreateIndex("dbo.People", "SecondaryAbilitiesId");
            CreateIndex("dbo.Stars", "SecondaryAbilitiesId");
            CreateIndex("dbo.AspNetUsers", "SecondaryAbilitiesId");
            AddForeignKey("dbo.People", "SecondaryAbilitiesId", "dbo.SecondaryAbilities", "Id");
            AddForeignKey("dbo.Stars", "SecondaryAbilitiesId", "dbo.SecondaryAbilities", "Id");
            AddForeignKey("dbo.AspNetUsers", "SecondaryAbilitiesId", "dbo.SecondaryAbilities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "SecondaryAbilitiesId", "dbo.SecondaryAbilities");
            DropForeignKey("dbo.Stars", "SecondaryAbilitiesId", "dbo.SecondaryAbilities");
            DropForeignKey("dbo.People", "SecondaryAbilitiesId", "dbo.SecondaryAbilities");
            DropIndex("dbo.AspNetUsers", new[] { "SecondaryAbilitiesId" });
            DropIndex("dbo.Stars", new[] { "SecondaryAbilitiesId" });
            DropIndex("dbo.People", new[] { "SecondaryAbilitiesId" });
            DropColumn("dbo.AspNetUsers", "SecondaryAbilitiesId");
            DropColumn("dbo.Stars", "SecondaryAbilitiesId");
            DropColumn("dbo.People", "SecondaryAbilitiesId");
            DropTable("dbo.SecondaryAbilities");
        }
    }
}
