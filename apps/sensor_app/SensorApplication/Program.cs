var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/temperature/{sensorId?}", (string? location, string? sensorId) =>
    {
        
        if (string.IsNullOrEmpty(location))
        {
            location = sensorId switch
            {
                "1" => "Living Room",
                "2" => "Bedroom",
                "3" => "Kitchen",
                _ => "Unknown"
            };
        }

        if (string.IsNullOrEmpty(sensorId))
        {
            sensorId = location switch
            {
                "Living Room" => "1",
                "Bedroom" => "2",
                "Kitchen" => "3",
                _ => "0"
            };
        }

        double temperature = Random.Shared.Next(150, 300) / 10.0;

        return new TemperatureSensor
        (
            location,
            sensorId,
            temperature
        );
    })
    .WithName("GetTemperature");

app.Run();

record TemperatureSensor(string Location, string SensorId, double Value);