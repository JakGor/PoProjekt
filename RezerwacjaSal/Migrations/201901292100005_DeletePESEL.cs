namespace RezerwacjaSal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePESEL : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rezerwacjas", name: "Pesel", newName: "Najem_Pesel");
            RenameIndex(table: "dbo.Rezerwacjas", name: "IX_Pesel", newName: "IX_Najem_Pesel");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rezerwacjas", name: "IX_Najem_Pesel", newName: "IX_Pesel");
            RenameColumn(table: "dbo.Rezerwacjas", name: "Najem_Pesel", newName: "Pesel");
        }
    }
}
