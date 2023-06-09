﻿namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A control for changing the rotation of the map.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class CompassControl : Control<CompassControlOptions>
    {
        internal override string Type => "compass";
        internal override int Order => 0;

        public CompassControl(CompassControlOptions options = null, ControlPosition position = default) : base(options, position) { }
    }

    internal class CompassControlJsonConverter : JsonConverter<CompassControl>
    {
        public override CompassControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();
        public override void Write(Utf8JsonWriter writer, CompassControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, CompassControl value)
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
                writer.WritePropertyName("options");
                writer.WriteStartObject();
                if (value.Options.RotationDegreesDelta.HasValue)
                {
                    writer.WriteNumber("rotationDegreesDelta", value.Options.RotationDegreesDelta.Value);
                }
                if (value.Options.Style.ToString() != default(ControlStyle).ToString())
                {
                    writer.WriteString("style", value.Options.Style.ToString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
