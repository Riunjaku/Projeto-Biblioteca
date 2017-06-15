namespace Asessment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9thCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emprestimo", "livro_LivroId", "dbo.Livro");
            DropIndex("dbo.Emprestimo", new[] { "livro_LivroId" });
            CreateTable(
                "dbo.EmprestimoLivro",
                c => new
                    {
                        Emprestimo_EmprestimoId = c.Int(nullable: false),
                        Livro_LivroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Emprestimo_EmprestimoId, t.Livro_LivroId })
                .ForeignKey("dbo.Emprestimo", t => t.Emprestimo_EmprestimoId, cascadeDelete: true)
                .ForeignKey("dbo.Livro", t => t.Livro_LivroId, cascadeDelete: true)
                .Index(t => t.Emprestimo_EmprestimoId)
                .Index(t => t.Livro_LivroId);
            
            DropColumn("dbo.Emprestimo", "livro_LivroId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emprestimo", "livro_LivroId", c => c.Int(nullable: false));
            DropForeignKey("dbo.EmprestimoLivro", "Livro_LivroId", "dbo.Livro");
            DropForeignKey("dbo.EmprestimoLivro", "Emprestimo_EmprestimoId", "dbo.Emprestimo");
            DropIndex("dbo.EmprestimoLivro", new[] { "Livro_LivroId" });
            DropIndex("dbo.EmprestimoLivro", new[] { "Emprestimo_EmprestimoId" });
            DropTable("dbo.EmprestimoLivro");
            CreateIndex("dbo.Emprestimo", "livro_LivroId");
            AddForeignKey("dbo.Emprestimo", "livro_LivroId", "dbo.Livro", "LivroId", cascadeDelete: true);
        }
    }
}
