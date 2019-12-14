namespace Signature.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBUpdatedd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchasesDetails", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PurchasesDetails", "Purchases_Id", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.PurchasesDetails", new[] { "Product_Id" });
            DropIndex("dbo.PurchasesDetails", new[] { "Purchases_Id" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropTable("dbo.PurchasesDetails");
            DropTable("dbo.Purchases");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Bill = c.String(),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchasesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductsId = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                        ManufacturedDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        MRP = c.Double(nullable: false),
                        Remarks = c.String(),
                        Product_Id = c.Int(),
                        Purchases_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Purchases", "SupplierId");
            CreateIndex("dbo.PurchasesDetails", "Purchases_Id");
            CreateIndex("dbo.PurchasesDetails", "Product_Id");
            AddForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchasesDetails", "Purchases_Id", "dbo.Purchases", "Id");
            AddForeignKey("dbo.PurchasesDetails", "Product_Id", "dbo.Products", "Id");
        }
    }
}
