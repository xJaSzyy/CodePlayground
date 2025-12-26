using System.Text.Json;
using System.Text.Json.Serialization;
using ClosedXML.Excel;
using CodePlayground.Extensions;

namespace CodePlayground.Services;

public static class RoadCoordinateExporter
{
    public static void ExportRoadsToExcel(string geoJsonPath, string excelPath, int cityId)
    {
        var geoJsonContent = File.ReadAllText(geoJsonPath);
        using var doc = JsonDocument.Parse(geoJsonContent);
        var root = doc.RootElement;

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Flow");

        ws.Cell(1, 1).Value = "Id";
        ws.Cell(1, 2).Value = "VehicleType";
        ws.Cell(1, 3).Value = "MaxTrafficIntensity";
        ws.Cell(1, 4).Value = "AverageSpeed";
        ws.Cell(1, 5).Value = "Points";
        ws.Cell(1, 6).Value = "CityId";

        var row = 2;

        if (root.TryGetProperty("features", out var features))
        {
            var groupedFeatures = features
                .EnumerateArray()
                .Select(f =>
                {
                    JsonElement propsElement;
                    JsonElement nameElement = default;

                    var hasName = f.TryGetProperty("properties", out propsElement)
                                  && propsElement.TryGetProperty("name", out nameElement);

                    var key = hasName && nameElement.ValueKind == JsonValueKind.String
                        ? nameElement.GetString()
                        : "";

                    return new { Feature = f, Name = key };
                })
                .GroupBy(x => x.Name)
                .ToList();

            var dict = new Dictionary<string, List<List<Coordinate>>>();
            
            foreach (var group in groupedFeatures)
            {
                var list = new List<List<Coordinate>>();
                
                foreach (var item in group)
                {
                    if (!item.Feature.TryGetProperty("geometry", out var geometry))
                    {
                        continue;
                    }

                    if (!geometry.TryGetProperty("type", out var typeProp) ||
                        !geometry.TryGetProperty("coordinates", out var coordsProp))
                    {
                        continue;
                    }

                    var type = typeProp.GetString();
                    if (type != "LineString" && type != "MultiLineString")
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(item.Name))
                    {
                        continue;
                    }

                    var coords = new List<Coordinate>();

                    if (type == "LineString")
                    {
                        foreach (var c in coordsProp.EnumerateArray())
                        {
                            if (c.GetArrayLength() < 2) continue;
                            coords.Add(new Coordinate
                            {
                                Lon = c[0].GetDouble(),
                                Lat = c[1].GetDouble()
                            });
                        }
                    }
                    else if (type == "MultiLineString")
                    {
                        foreach (var line in coordsProp.EnumerateArray())
                        {
                            foreach (var c in line.EnumerateArray())
                            {
                                if (c.GetArrayLength() < 2) continue;
                                coords.Add(new Coordinate
                                {
                                    Lon = c[0].GetDouble(),
                                    Lat = c[1].GetDouble()
                                });
                            }
                        }
                    }

                    if (coords.Count == 0)
                    {
                        continue;
                    }

                    list.Add(coords);
                    /*if (!dict.ContainsKey(item.Name))
                    {
                        dict.Add(item.Name, new List<List<Coordinate>>
                        {
                            coords
                        });
                    }
                    else
                    {
                        dict[item.Name].Add(coords);
                    }*/
                }

                if (list.Count == 0)
                {
                    continue;
                }

                var merged = GeoUtils.MergeAll(list);
                
                var json = JsonSerializer.Serialize(
                    merged,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = null,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });

                ws.Cell(row, 1).Value = row - 1;
                ws.Cell(row, 2).Value = 1;
                ws.Cell(row, 3).Value = 35;
                ws.Cell(row, 4).Value = 40;
                ws.Cell(row, 5).Value = json;
                ws.Cell(row, 6).Value = cityId;
                row++;
            }
        }

        ws.Columns().AdjustToContents();
        wb.SaveAs(excelPath);
    }
}

public class Coordinate
{
    public double Lon { get; set; }
    public double Lat { get; set; }
}