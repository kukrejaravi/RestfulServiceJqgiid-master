namespace RESTfulTutorial.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("Insert into dbo.BlogPosts Values('Resilient Connection for Entity Framework 6','http://jpreecedev.com/2014/02/05/resilient-connection-for-entity-framework-6/'),('How to pass Microsoft Exam 70-486 (Developing ASP.NET MVC 4 Web Applications) in 30 days','http://jpreecedev.com/2014/02/01/how-to-pass-microsoft-exam-70-486-developing-asp-net-mvc-4-web-applications-in-30-days/'),('5 easy security enhancements for your ASP .NET application','http://jpreecedev.com/2014/01/26/5-easy-security-enhancements-for-your-asp-net-application/'),('10 things every software developer should do in 2014','http://jpreecedev.com/2014/01/18/10-things-every-software-developer-should-do-in-2014/'),('Hello World','http://jpreecedev.com/2013/12/28/15-reasons-why-i-cant-work-without-jetbrains-resharper/')");
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BlogPosts");
        }
    }
}
