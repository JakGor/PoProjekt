namespace RezerwacjaSal
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    /// <summary>
    /// Klasa uzywana w podejœciu Code First - odwzorowuje strukturê kodu w now¹ bazê danych
    /// </summary>
    public class ModelContext : DbContext
    {
        // Your context has been configured to use a 'ModelContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'RezerwacjaSal.ModelContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelContext' 
        // connection string in the application configuration file.
        public ModelContext()
            : base("name=ModelContext")
        {
        }
        /// <summary>
        /// Tabela zawieraj¹ca najemców
        /// </summary>
        public DbSet<Najemca> Najemcy { get; set; }
        /// <summary>
        /// Tabela zawieraj¹ca Rezerwacje
        /// </summary>
        public DbSet<Rezerwacja> Rezerwacje { get; set; }
        /// <summary>
        /// Tabela zawieraj¹ca sale
        /// </summary>
        public DbSet<Sala> Sale { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}