// See https://aka.ms/new-console-template for more information


using System.Text.Json;
using Raphaelle.Verdiere.FeatureMatching;

var img = await File.ReadAllBytesAsync(args[0]);
var imageScenesData = new List<byte[]>();
foreach (var imagePath in Directory.EnumerateFiles(args[1]))
{
    var imageBytes = await File.ReadAllBytesAsync(imagePath);
    imageScenesData.Add(imageBytes);
}
var detectObjectInScenesResults = 
    await new ObjectDetection().DetectObjectInScenesAsync(img,imageScenesData);


foreach (var objectDetectionResult in detectObjectInScenesResults)
{
    System.Console.WriteLine($"Points: {JsonSerializer.Serialize(objectDetectionResult.Points)}");
}