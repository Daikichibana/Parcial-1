using Microsoft.Azure.Management.Fluent;

namespace apiDoble.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using apiDoble.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> enviarAsync([FromBody] Data data)
        {
            string connectionString;
            string queueName;
            String mensaje = JsonConvert.SerializeObject(data);
            JObject numero = JObject.Parse(mensaje);
            int randomNum = (int) numero["Random"];

            if (randomNum % 2 == 0)
            {
                connectionString = "Endpoint=sb://colaparcial1daiki.servicebus.windows.net/;SharedAccessKeyName=Enviar;SharedAccessKey=8oA1hZMcukUkqZUpE5+D8SBfX/hktrodLR4c7rROxk4=;EntityPath=qpar";
                queueName = "qpar";
                Console.WriteLine("Numero par");
            }
            else {
                connectionString = "Endpoint=sb://colaparcial1daiki.servicebus.windows.net/;SharedAccessKeyName=Enviar;SharedAccessKey=JaayL4pZZnkcrn4bHl5BXQTfemxGvMJ1YWxDA+vpTTQ=;EntityPath=qimpar";
                queueName = "qimpar";
                Console.WriteLine("Numero impar");
            }

            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }


            return true;
        }
    }
}
