using MQTTnet;
using MQTTnet.Client;

await Publish();
return;

async Task Publish()
{
    var factory = new MqttFactory();

    using var client = factory.CreateMqttClient();
    var clientOptions = new MqttClientOptionsBuilder()
        .WithTcpServer("localhost")
        .Build();

    await client.ConnectAsync(clientOptions, CancellationToken.None);

    var applicationMessage = new MqttApplicationMessageBuilder()
        .WithTopic("samples/temperature/living_room")
        .WithPayload("19.5")
        .Build();

    await client.PublishAsync(applicationMessage, CancellationToken.None);

    await client.DisconnectAsync();

    Console.WriteLine("MQTT application message is published.");
}

async Task PublishFile()
{
    var factory = new MqttFactory();

    using var client = factory.CreateMqttClient();
    var clientOptions = new MqttClientOptionsBuilder()
        .WithTcpServer("localhost")
        .Build();

    await client.ConnectAsync(clientOptions, CancellationToken.None);

    var payload = File.ReadAllBytes("/Volumes/External/www/mqtt-dotnet/mqtt-dotnet-publish/assets/definição_totem_Sinetram.docx");
    var applicationMessage = new MqttApplicationMessageBuilder()
        .WithTopic("samples/temperature/living_room/file")
        .WithPayload(payload)
        .Build();

    await client.PublishAsync(applicationMessage, CancellationToken.None);

    await client.DisconnectAsync();

    Console.WriteLine("MQTT application message is published.");
}