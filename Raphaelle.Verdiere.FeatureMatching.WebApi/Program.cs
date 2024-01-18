using Microsoft.AspNetCore.Mvc;
using Raphaelle.Verdiere.FeatureMatching;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/FeatureMatching", async ([FromForm] IFormFileCollection files) =>
{
    if (files.Count != 2)
        return Results.BadRequest();
    using var objectSourceStream = files[0].OpenReadStream();
    using var objectMemoryStream = new MemoryStream();
    objectSourceStream.CopyTo(objectMemoryStream);
    var imageObjectData = objectMemoryStream.ToArray();
    using var sceneSourceStream = files[1].OpenReadStream();
    using var sceneMemoryStream = new MemoryStream();
    sceneSourceStream.CopyTo(sceneMemoryStream);
    var imageSceneData = sceneMemoryStream.ToArray();
    // Your implementation code
    var result = await new ObjectDetection().DetectObjectInScenesAsync(imageObjectData, imageSceneData);
    
    // La méthode ci-dessous permet de retourner une image depuis un tableau de bytes, var imageData = new bytes[];
    return Results.File(result[0].ImageData, "image/png");
}).DisableAntiforgery(); 

app.Run();