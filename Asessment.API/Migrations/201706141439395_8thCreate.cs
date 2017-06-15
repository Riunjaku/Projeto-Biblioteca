namespace Asessment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8thCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emprestimo", "LivroId", "dbo.Livro");
            RenameColumn(table: "dbo.Emprestimo", name: "LivroId", newName: "livro_LivroId");
            RenameIndex(table: "dbo.Emprestimo", name: "IX_LivroId", newName: "IX_livro_LivroId");
            AddForeignKey("dbo.Emprestimo", "livro_LivroId", "dbo.Livro", "LivroId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emprestimo", "livro_LivroId", "dbo.Livro");
            RenameIndex(table: "dbo.Emprestimo", name: "IX_livro_LivroId", newName: "IX_LivroId");
            RenameColumn(table: "dbo.Emprestimo", name: "livro_LivroId", newName: "LivroId");
            AddForeignKey("dbo.Emprestimo", "LivroId", "dbo.Livro", "LivroId");
        }
    }
}
