using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Asb404
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<Models.DBContexter>(null);
            EnsureContactMessagesTable();
        }

        private static void EnsureContactMessagesTable()
        {
            using (var db = new Models.DBContexter())
            {
                db.Database.ExecuteSqlCommand(@"
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ContactMessages')
BEGIN
    CREATE TABLE [dbo].[ContactMessages](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Email] [nvarchar](200) NOT NULL,
        [Subject] [nvarchar](200) NOT NULL,
        [Memo] [nvarchar](4000) NOT NULL,
        [Status] [nvarchar](50) NULL,
        [CreatedAt] [datetime] NOT NULL,
        CONSTRAINT [PK_dbo.ContactMessages] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END");
            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(new Char[] { ',' });
                GenericPrincipal userPrincipal =
                    new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
                Context.User = userPrincipal;
            }
        }
    }
}
