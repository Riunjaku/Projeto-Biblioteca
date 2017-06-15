namespace Asessment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emprestimo", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Emprestimo", "DataRetirada", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Emprestimo", "DataEntrega", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emprestimo", "DataEntrega", c => c.String());
            AlterColumn("dbo.Emprestimo", "DataRetirada", c => c.String());
            DropColumn("dbo.Emprestimo", "Price");
        }
    }
}
