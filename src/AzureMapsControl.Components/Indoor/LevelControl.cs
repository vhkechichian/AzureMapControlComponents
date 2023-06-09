﻿namespace AzureMapsControl.Components.Indoor
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A control for changing the level of the indoor map.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(LevelControlJsonConverter))]
    public struct LevelControl
    {
        internal readonly LevelControlOptions Options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">The options of the control</param>
        public LevelControl(LevelControlOptions options = null) => Options = options;
    }

    internal class LevelControlJsonConverter : JsonConverter<LevelControl>
    {
        public override LevelControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();
        public override void Write(Utf8JsonWriter writer, LevelControl value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value.Options is not null)
            {
                writer.WritePropertyName("options");
                JsonSerializer.Serialize(writer, value.Options, options);
            }
            writer.WriteEndObject();
        }
    }
}
