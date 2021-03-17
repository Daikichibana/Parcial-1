namespace fncPar
{
    using System;
    using System.Threading.Tasks;
    using fncPar.Models;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task RunAsync(
            [ServiceBusTrigger(
                    "qpar",
                    Connection = "MyConn"
                    )]string myQueueItem,
            [CosmosDB(
                    databaseName: "dbParcial",
                    collectionName: "dbPar",
                    ConnectionStringSetting = "strCosmos"
                    )]IAsyncCollector<Object> datos,
            ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                var data = JsonConvert.DeserializeObject<Data>(myQueueItem);
                await datos.AddAsync(data);
            }
            catch (Exception ex)
            {
                log.LogError($"no fue posible insertar datos: {ex.Message}");
            }
        }
    }
}
