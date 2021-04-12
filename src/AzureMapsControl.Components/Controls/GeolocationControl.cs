﻿namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    public sealed class GeolocationControl : Control<GeolocationControlOptions>
    {
        internal override string Type => "geolocation";

        internal override int Order => 0;

        public GeolocationControl(GeolocationControlOptions options = null, ControlPosition position = default) : base(options, position) { }
    }

    internal class GeolocationControlJsonConverter : JsonConverter<GeolocationControl>
    {
        public override GeolocationControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, GeolocationControl value, JsonSerializerOptions options) => Write(writer, value);

        public static void Write(Utf8JsonWriter writer, GeolocationControl value)
        {
            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WriteString("type", value.Type);
            if (value.Position.ToString() != default(ControlPosition).ToString())
            {
                writer.WriteString("position", value.Position.ToString());
            }
            if (value.Options is not null)
            {
                writer.WriteStartObject("options");
                if (value.Options.CalculateMissingValues.HasValue)
                {
                    writer.WriteBoolean("calculateMissingValues", value.Options.CalculateMissingValues.Value);
                }

                if (!string.IsNullOrWhiteSpace(value.Options.MarkerColor))
                {
                    writer.WriteString("markerColor", value.Options.MarkerColor);
                }

                if (value.Options.MaxZoom.HasValue)
                {
                    writer.WriteNumber("maxZoom", value.Options.MaxZoom.Value);
                }

                if (value.Options.PositionOptions is not null)
                {
                    writer.WriteStartObject();
                    if (value.Options.PositionOptions.EnableHighAccuracy.HasValue)
                    {
                        writer.WriteBoolean("enableHighAccuracy", value.Options.PositionOptions.EnableHighAccuracy.Value);
                    }

                    if (value.Options.PositionOptions.MaximumAge.HasValue)
                    {
                        writer.WriteNumber("maximumAge", value.Options.PositionOptions.MaximumAge.Value);
                    }

                    if (value.Options.PositionOptions.Timeout.HasValue)
                    {
                        writer.WriteNumber("timeout", value.Options.PositionOptions.Timeout.Value);
                    }

                    writer.WriteEndObject();
                }

                if (value.Options.ShowUserLocation.HasValue)
                {
                    writer.WriteBoolean("showUserLocation", value.Options.ShowUserLocation.Value);
                }

                if (value.Options.Style is not null)
                {
                    writer.WriteString("style", value.Options.Style.ToString());
                }

                if (value.Options.TrackUserLocation.HasValue)
                {
                    writer.WriteBoolean("trackUserLocation", value.Options.TrackUserLocation.Value);
                }

                if (value.Options.UpdateMapCamera.HasValue)
                {
                    writer.WriteBoolean("updateMapCamera", value.Options.UpdateMapCamera.Value);
                }

                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
