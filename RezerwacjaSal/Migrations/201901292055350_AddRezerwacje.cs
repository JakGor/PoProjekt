namespace RezerwacjaSal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRezerwacje : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rezerwacjas",
                c => new
                    {
                        Numer = c.Int(nullable: false, identity: true),
                        Pesel = c.String(maxLength: 128),
                        Dzień = c.DateTime(nullable: false),
                        Godz_pocz_serializacja = c.Long(nullable: false),
                        Godzina_końcowa_serializacja = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Numer)
                .ForeignKey("dbo.Najemcas", t => t.Pesel)
                .Index(t => t.Pesel);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rezerwacjas", "Pesel", "dbo.Najemcas");
            DropIndex("dbo.Rezerwacjas", new[] { "Pesel" });
            DropTable("dbo.Rezerwacjas");
        }
    }
}
