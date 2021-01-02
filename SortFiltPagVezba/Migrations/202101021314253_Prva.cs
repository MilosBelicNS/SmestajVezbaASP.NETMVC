namespace SortFiltPagVezba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prva : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rezervacijas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImePrezime = c.String(nullable: false, maxLength: 50),
                        DatumPocetka = c.DateTime(nullable: false),
                        DatumKraja = c.DateTime(nullable: false),
                        Otkazana = c.Boolean(nullable: false),
                        SobaId = c.Int(nullable: false),
                        Smestaj_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Smestajs", t => t.Smestaj_Id)
                .ForeignKey("dbo.Sobas", t => t.SobaId, cascadeDelete: true)
                .Index(t => t.SobaId)
                .Index(t => t.Smestaj_Id);
            
            CreateTable(
                "dbo.Sobas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojSobe = c.Int(nullable: false),
                        BrojKreveta = c.Int(nullable: false),
                        CenaNoc = c.Int(nullable: false),
                        SmestajId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Smestajs", t => t.SmestajId, cascadeDelete: true)
                .Index(t => t.SmestajId);
            
            CreateTable(
                "dbo.Smestajs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 50),
                        Opis = c.String(nullable: false),
                        Adresa = c.String(nullable: false),
                        Ocena = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rezervacijas", "SobaId", "dbo.Sobas");
            DropForeignKey("dbo.Sobas", "SmestajId", "dbo.Smestajs");
            DropForeignKey("dbo.Rezervacijas", "Smestaj_Id", "dbo.Smestajs");
            DropIndex("dbo.Sobas", new[] { "SmestajId" });
            DropIndex("dbo.Rezervacijas", new[] { "Smestaj_Id" });
            DropIndex("dbo.Rezervacijas", new[] { "SobaId" });
            DropTable("dbo.Smestajs");
            DropTable("dbo.Sobas");
            DropTable("dbo.Rezervacijas");
        }
    }
}
