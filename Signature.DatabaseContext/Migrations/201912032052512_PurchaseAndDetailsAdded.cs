namespace Signature.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseAndDetailsAdded : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Purchases", t => t.Purchases_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Purchases_Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Bill = c.String(),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PurchasesDetails", "Purchases_Id", "dbo.Purchases");
            DropForeignKey("dbo.PurchasesDetails", "Product_Id", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.PurchasesDetails", new[] { "Purchases_Id" });
            DropIndex("dbo.PurchasesDetails", new[] { "Product_Id" });
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchasesDetails");
        }
    }
}
