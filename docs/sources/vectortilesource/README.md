A vector tile source loads data formatted as vector tiles for the current map view, based on the maps tiling system. Ideal for large to massive data sets (millions or billions of shapes).

![Vector Tile source](../../assets/vectortilesource.png) 

```
@page "/VectorTileSource"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center = new Components.Atlas.Position(-74, 40.723), Zoom = 12 }"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = MapStyle.GrayscaleDark
    };

    public async Task OnMapReady(MapEventArgs events)
    {
        var sourceId = "dataSourceId";
        var source = new AzureMapsControl.Components.Data.VectorTileSource(sourceId)
        {
            Options = new Components.Data.VectorTileSourceOptions
            {
                Tiles = new []
                {
                    "https://{azMapsDomain}/traffic/flow/tile/pbf?api-version=1.0&style=relative&zoom={z}&x={x}&y={y}"
                }
            }
        };

        await events.Map.AddSourceAsync(source);

        var layer = new AzureMapsControl.Components.Layers.LineLayer
        {
            Options = new Components.Layers.LineLayerOptions
            {
                Source = sourceId,
                SourceLayer = "Traffic flow",
                StrokeColor = new Components.Atlas.ExpressionOrString(
                    new[] {
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("step"),
                        new Components.Atlas.Expression(new [] {
                            new AzureMapsControl.Components.Atlas.ExpressionOrString("get"),
                            new AzureMapsControl.Components.Atlas.ExpressionOrString("traffic_level")
                        }),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("#6B0512"),
                        new AzureMapsControl.Components.Atlas.ExpressionOrNumber(0.01),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("#EE2F53"),
                        new AzureMapsControl.Components.Atlas.ExpressionOrNumber(0.8),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("orange"),
                        new AzureMapsControl.Components.Atlas.ExpressionOrNumber(1),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("#66CC99")
                                        }
                ),
                StrokeWidth = new Components.Atlas.ExpressionOrNumber(
                    new[]
                    {
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("interpolate"),
                        new Components.Atlas.Expression(new [] { new AzureMapsControl.Components.Atlas.ExpressionOrString("linear") }),
                        new Components.Atlas.Expression(new [] {
                            new AzureMapsControl.Components.Atlas.ExpressionOrString("get"),
                            new AzureMapsControl.Components.Atlas.ExpressionOrString("traffic_level")
                        }),
                        new AzureMapsControl.Components.Atlas.ExpressionOrNumber(0),
                        new AzureMapsControl.Components.Atlas.ExpressionOrNumber(6),
                        new AzureMapsControl.Components.Atlas.ExpressionOrNumber(1),
                        new AzureMapsControl.Components.Atlas.ExpressionOrNumber(1),
                                }
                )
            }
        };

        await events.Map.AddLayerAsync(layer, "labels");
    }
}
```