namespace RezerwacjaSal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Najemcas",
                c => new
                    {
                        Pesel = c.String(nullable: false, maxLength: 128),
                        Imię = c.String(),
                        Płeć = c.Int(nullable: false),
                        Nazwisko = c.String(),
                    })
                .PrimaryKey(t => t.Pesel);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Najemcas");
        }
    }
}
