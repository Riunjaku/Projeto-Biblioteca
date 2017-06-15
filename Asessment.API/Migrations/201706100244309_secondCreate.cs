namespace Asessment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livro", "Edicao", c => c.String());
            AddColumn("dbo.Livro", "Comentario", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Livro", "Comentario");
            DropColumn("dbo.Livro", "Edicao");
        }
    }
}
