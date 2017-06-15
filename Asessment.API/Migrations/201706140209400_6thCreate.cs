namespace Asessment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6thCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Livro", "Autor");
            DropColumn("dbo.Livro", "AutorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Livro", "AutorId", c => c.Int(nullable: false));
            AddColumn("dbo.Livro", "Autor", c => c.String());
        }
    }
}
