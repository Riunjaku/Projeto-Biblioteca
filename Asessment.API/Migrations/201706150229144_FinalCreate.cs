namespace Asessment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmprestimoLivro", "Emprestimo_EmprestimoId", "dbo.Emprestimo");
            DropForeignKey("dbo.EmprestimoLivro", "Livro_LivroId", "dbo.Livro");
            DropIndex("dbo.EmprestimoLivro", new[] { "Emprestimo_EmprestimoId" });
            DropIndex("dbo.EmprestimoLivro", new[] { "Livro_LivroId" });
            DropTable("dbo.Emprestimo");
            DropTable("dbo.EmprestimoLivro");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmprestimoLivro",
                c => new
                    {
                        Emprestimo_EmprestimoId = c.Int(nullable: false),
                        Livro_LivroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Emprestimo_EmprestimoId, t.Livro_LivroId });
            
            CreateTable(
                "dbo.Emprestimo",
                c => new
                    {
                        EmprestimoId = c.Int(nullable: false, identity: true),
                        DataRetirada = c.DateTime(nullable: false),
                        DataEntrega = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EmprestimoId);
            
            CreateIndex("dbo.EmprestimoLivro", "Livro_LivroId");
            CreateIndex("dbo.EmprestimoLivro", "Emprestimo_EmprestimoId");
            AddForeignKey("dbo.EmprestimoLivro", "Livro_LivroId", "dbo.Livro", "LivroId", cascadeDelete: true);
            AddForeignKey("dbo.EmprestimoLivro", "Emprestimo_EmprestimoId", "dbo.Emprestimo", "EmprestimoId", cascadeDelete: true);
        }
    }
}
