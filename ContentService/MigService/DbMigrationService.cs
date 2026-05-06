using ContentService.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ContentService.MigService
{
    public class DbMigrationService
    {
        public static void MigrationInit(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                try
                {
                    serviceScope.ServiceProvider.GetService<ContentServiceContext>().Database.Migrate();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
