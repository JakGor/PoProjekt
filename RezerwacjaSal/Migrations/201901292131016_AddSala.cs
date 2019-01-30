namespace RezerwacjaSal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSala : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Salas",
                c => new
                    {
                        Nazwa = c.String(nullable: false, maxLength: 128),
                        Typ = c.Int(nullable: false),
                        Pojemność = c.Int(nullable: false),
                        LiczbaRezerwacji = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Nazwa);
            
            AddColumn("dbo.Rezerwacjas", "Sala_Nazwa", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rezerwacjas", "Sala_Nazwa");
            AddForeignKey("dbo.Rezerwacjas", "Sala_Nazwa", "dbo.Salas", "Nazwa");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rezerwacjas", "Sala_Nazwa", "dbo.Salas");
            DropIndex("dbo.Rezerwacjas", new[] { "Sala_Nazwa" });
            DropColumn("dbo.Rezerwacjas", "Sala_Nazwa");
            DropTable("dbo.Salas");
        }
    }
}
