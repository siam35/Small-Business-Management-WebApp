namespace Signature.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseAndDetailsAddedFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchasesDetails", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PurchasesDetails", "Purchases_Id", "dbo.Purchases");
            DropIndex("dbo.PurchasesDetails", new[] { "Product_Id" });
            DropIndex("dbo.PurchasesDetails", new[] { "Purchases_Id" });
            RenameColumn(table: "dbo.PurchasesDetails", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.PurchasesDetails", name: "Purchases_Id", newName: "PurchasesId");
            AlterColumn("dbo.PurchasesDetails", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchasesDetails", "PurchasesId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchasesDetails", "ProductId");
            CreateIndex("dbo.PurchasesDetails", "PurchasesId");
            AddForeignKey("dbo.PurchasesDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchasesDetails", "PurchasesId", "dbo.Purchases", "Id", cascadeDelete: true);
            DropColumn("dbo.PurchasesDetails", "ProductsId");
            DropColumn("dbo.PurchasesDetails", "PurchaseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchasesDetails", "PurchaseId", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasesDetails", "ProductsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PurchasesDetails", "PurchasesId", "dbo.Purchases");
            DropForeignKey("dbo.PurchasesDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.PurchasesDetails", new[] { "PurchasesId" });
            DropIndex("dbo.PurchasesDetails", new[] { "ProductId" });
            AlterColumn("dbo.PurchasesDetails", "PurchasesId", c => c.Int());
            AlterColumn("dbo.PurchasesDetails", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.PurchasesDetails", name: "PurchasesId", newName: "Purchases_Id");
            RenameColumn(table: "dbo.PurchasesDetails", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.PurchasesDetails", "Purchases_Id");
            CreateIndex("dbo.PurchasesDetails", "Product_Id");
            AddForeignKey("dbo.PurchasesDetails", "Purchases_Id", "dbo.Purchases", "Id");
            AddForeignKey("dbo.PurchasesDetails", "Product_Id", "dbo.Products", "Id");
        }
    }
}
