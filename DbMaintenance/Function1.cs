using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace DbMaintenance
{
    public static class Function1
    {
        [FunctionName("DeleteToDoListsWithoutUserId")]
        public static async void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log) //"0 */1 * * * *" is once every top of the minute (placeholder)
        {
            //TimerTrigger documentation: https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=csharp

            //Replace connection string with the one in Azure SQL Database, if published
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ToDoListDb;Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                await con.OpenAsync();

                var query = "DELETE FROM dbo.TDLists " +
                            "WHERE UserId IS NULL";

                using (SqlCommand cmd = new SqlCommand(query, con)) 
                {
                    int rows = await cmd.ExecuteNonQueryAsync();
                    log.LogInformation("Todo lists without a userId has been deleted");
                }

                await con.DisposeAsync();
            }
        }
    }
}
