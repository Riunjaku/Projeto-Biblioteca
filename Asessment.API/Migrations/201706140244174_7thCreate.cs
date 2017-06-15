namespace Asessment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7thCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Autor", "DataNascimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Autor", "Email", c => c.String());
            AlterColumn("dbo.Autor", "Nome", c => c.String());
            AlterColumn("dbo.Autor", "Sobrenome", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Autor", "Sobrenome", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Autor", "Nome", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Autor", "Email");
            DropColumn("dbo.Autor", "DataNascimento");
        }
    }
}
