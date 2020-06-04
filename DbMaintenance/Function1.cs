using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DbMaintenance
{
    public static class Function1
    {
        [FunctionName("DeleteToDoListsWithoutUserId")]
        public static async void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log) //"0 */1 * * * *" is once every top of the minute (placeholder)
        {
            //TimerTrigger documentation: https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=csharp

            ConnectionStringManager connectionStringManager = new ConnectionStringManager();
            var connectionString = connectionStringManager.GetConnectionString();

            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                await con.OpenAsync();

                var query = "DELETE FROM dbo.TDLists " +
                            "WHERE UserId IS NULL";

                using (SqlCommand cmd = new SqlCommand(query, con)) 
                {
                    await cmd.ExecuteNonQueryAsync();
                    log.LogInformation("Todo lists without a userId has been deleted");
                }

                await con.DisposeAsync();
            }
        }

        public class ConnectionStringManager 
        {
            public string GetConnectionString() 
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            }
        }
    }
}
