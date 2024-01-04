using mqtt_dotnet_library;
using MQTTnet;
using MQTTnet.Client;

await Subscribe();
return;

async Task Subscribe()
{
    var mqttFactory = new MqttFactory();

    using var mqttClient = mqttFactory.CreateMqttClient();
    var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();

    mqttClient.ApplicationMessageReceivedAsync += e =>
    {
        Console.WriteLine("Received application message.");
        e.DumpToConsole();

        return Task.CompletedTask;
    };

    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

    var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
        .WithTopicFilter(
            f => { f.WithTopic("samples/temperature/living_room"); })
        .Build();

    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

    Console.WriteLine("MQTT client subscribed to topic.");

    Console.WriteLine("Press enter to exit.");
    Console.ReadLine();
}


async Task SubscribeFile()
{
    var mqttFactory = new MqttFactory();

    using var mqttClient = mqttFactory.CreateMqttClient();
    var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();

    mqttClient.ApplicationMessageReceivedAsync += e =>
    {
        Console.WriteLine("Received application message.");
        var payload = e.ApplicationMessage.Payload;
        
        // save file
        
        var path = "/Volumes/External/www/mqtt-dotnet/mqtt-dotnet-subscribe/assets/tkpp-cli.docx";
        
        File.WriteAllBytes(path, payload);
        Console.WriteLine($"File saved to {path}");
        return Task.CompletedTask;
    };

    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

    var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
        .WithTopicFilter(
            f => { f.WithTopic("samples/temperature/living_room/file"); })
        .Build();

    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

    Console.WriteLine("MQTT client subscribed to topic.");

    Console.WriteLine("Press enter to exit.");
    Console.ReadLine();
}
