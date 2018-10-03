namespace RL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountId = c.Int(nullable: false, identity: true),
                        AccountName = c.String(nullable: false),
                        AccountNumber = c.String(nullable: false),
                        BankId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BankAccountId)
                .ForeignKey("dbo.Banks", t => t.BankAccountId)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.BankCards",
                c => new
                    {
                        BankCardId = c.Int(nullable: false, identity: true),
                        BankCardName = c.String(nullable: false),
                        BankCardNumber = c.String(nullable: false),
                        BankAccountId = c.Int(),
                    })
                .PrimaryKey(t => t.BankCardId)
                .ForeignKey("dbo.BankAccounts", t => t.BankCardId)
                .Index(t => t.BankCardId);
            
            CreateTable(
                "dbo.Statements",
                c => new
                    {
                        StatementId = c.Int(nullable: false, identity: true),
                        AccountBalance = c.Double(nullable: false),
                        AvailableBalance = c.Double(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatementId)
                .ForeignKey("dbo.BankAccounts", t => t.StatementId)
                .Index(t => t.StatementId);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageSource = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
            CreateTable(
                "dbo.BudgetItemCategories",
                c => new
                    {
                        BudgetItemCategoryId = c.Int(nullable: false, identity: true),
                        BudgetItemCategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetItemCategoryId);
            
            CreateTable(
                "dbo.BudgetItems",
                c => new
                    {
                        BudgetItemId = c.Int(nullable: false, identity: true),
                        BudgetDescription = c.String(nullable: false),
                        BudgetItemAmount = c.Double(nullable: false),
                        BudgetItemCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.BudgetItemId)
                .ForeignKey("dbo.BudgetItemCategories", t => t.BudgetItemId)
                .Index(t => t.BudgetItemId);
            
            CreateTable(
                "dbo.Retailers",
                c => new
                    {
                        RetailerId = c.Int(nullable: false, identity: true),
                        RetailerName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RetailerId);
            
            CreateTable(
                "dbo.RetailerIdentifiers",
                c => new
                    {
                        RetailerIdentifierId = c.Int(nullable: false, identity: true),
                        Identity = c.String(nullable: false),
                        RetailerId = c.Int(),
                    })
                .PrimaryKey(t => t.RetailerIdentifierId)
                .ForeignKey("dbo.Retailers", t => t.RetailerId)
                .Index(t => t.RetailerId);
            
            CreateTable(
                "dbo.TransactionEntries",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionDescription = c.String(nullable: false),
                        TransactionAmount = c.Double(nullable: false),
                        TransactionBalance = c.Double(nullable: false),
                        BankCardId = c.Int(),
                        RetailerId = c.Int(),
                        BudgetItemId = c.Int(),
                        StatementId = c.Int(),
                        TransactionTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.TransactionTypeIdentifiers",
                c => new
                    {
                        TransactionTypeIdentifierId = c.Int(nullable: false, identity: true),
                        TransactionTypeIdentifierPrimary = c.String(nullable: false),
                        TransactionTypeIdentifierSecondary = c.String(),
                        TransactionTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionTypeIdentifierId)
                .ForeignKey("dbo.TransactionTypes", t => t.TransactionTypeId)
                .Index(t => t.TransactionTypeId);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        TransactionTypeId = c.Int(nullable: false, identity: true),
                        TransactionTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTypeIdentifiers", "TransactionTypeId", "dbo.TransactionTypes");
            DropForeignKey("dbo.RetailerIdentifiers", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.BudgetItems", "BudgetItemId", "dbo.BudgetItemCategories");
            DropForeignKey("dbo.BankAccounts", "BankAccountId", "dbo.Banks");
            DropForeignKey("dbo.Statements", "StatementId", "dbo.BankAccounts");
            DropForeignKey("dbo.BankCards", "BankCardId", "dbo.BankAccounts");
            DropIndex("dbo.TransactionTypeIdentifiers", new[] { "TransactionTypeId" });
            DropIndex("dbo.RetailerIdentifiers", new[] { "RetailerId" });
            DropIndex("dbo.BudgetItems", new[] { "BudgetItemId" });
            DropIndex("dbo.Statements", new[] { "StatementId" });
            DropIndex("dbo.BankCards", new[] { "BankCardId" });
            DropIndex("dbo.BankAccounts", new[] { "BankAccountId" });
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.TransactionTypeIdentifiers");
            DropTable("dbo.TransactionEntries");
            DropTable("dbo.RetailerIdentifiers");
            DropTable("dbo.Retailers");
            DropTable("dbo.BudgetItems");
            DropTable("dbo.BudgetItemCategories");
            DropTable("dbo.Banks");
            DropTable("dbo.Statements");
            DropTable("dbo.BankCards");
            DropTable("dbo.BankAccounts");
        }
    }
}
