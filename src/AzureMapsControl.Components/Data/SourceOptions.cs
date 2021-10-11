﻿namespace AzureMapsControl.Components.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Data.Grid;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(SourceOptionsJsonConverter))]
    public abstract class SourceOptions
    {
    }

    internal class SourceOptionsJsonConverter : JsonConverter<SourceOptions>
    {
        public override SourceOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();

        public override void Write(Utf8JsonWriter writer, SourceOptions value, JsonSerializerOptions options)
        {
            if (value is DataSourceOptions datasourceOptions)
            {
                JsonSerializer.Serialize(writer, datasourceOptions, options);
            }
            else if (value is VectorTileSourceOptions vectorTileSourceOptions)
            {
                JsonSerializer.Serialize(writer, vectorTileSourceOptions, options);
            }
            else if (value is GriddedDataSourceOptions griddedDataSourceOptions)
            {
                JsonSerializer.Serialize(writer, griddedDataSourceOptions, options);
            }
        }
    }
}
