namespace Asessment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4thCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autor",
                c => new
                    {
                        AutorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Sobrenome = c.String(nullable: false, maxLength: 150),
                        Selecionado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AutorId);
            
            CreateTable(
                "dbo.LivroAutor",
                c => new
                    {
                        Livro_LivroId = c.Int(nullable: false),
                        Autor_AutorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Livro_LivroId, t.Autor_AutorId })
                .ForeignKey("dbo.Livro", t => t.Livro_LivroId, cascadeDelete: true)
                .ForeignKey("dbo.Autor", t => t.Autor_AutorId, cascadeDelete: true)
                .Index(t => t.Livro_LivroId)
                .Index(t => t.Autor_AutorId);
            
            AddColumn("dbo.Livro", "AutorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LivroAutor", "Autor_AutorId", "dbo.Autor");
            DropForeignKey("dbo.LivroAutor", "Livro_LivroId", "dbo.Livro");
            DropIndex("dbo.LivroAutor", new[] { "Autor_AutorId" });
            DropIndex("dbo.LivroAutor", new[] { "Livro_LivroId" });
            DropColumn("dbo.Livro", "AutorId");
            DropTable("dbo.LivroAutor");
            DropTable("dbo.Autor");
        }
    }
}
