﻿namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    [JsonConverter(typeof(LineCapJsonConverter))]
    [ExcludeFromCodeCoverage]
    public struct LineCap
    {
        private readonly string _lineCap;

        /// <summary>
        /// A cap with a squared-off end which is drawn to the exact endpoint of the line.
        /// </summary>
        public static readonly LineCap Butt = new("butt");

        /// <summary>
        ///  A cap with a rounded end which is drawn beyond the endpoint of the line at a radius of one-half of the lines width and centered on the endpoint of the line
        /// </summary>
        public static readonly LineCap Round = new("round");

        /// <summary>
        /// A cap with a squared-off end which is drawn beyond the endpoint of the line at a distance of one-half of the line width
        /// </summary>
        public static readonly LineCap Square = new("square");

        private LineCap(string lineCap) => _lineCap = lineCap;

        public override string ToString() => _lineCap;

        /// <summary>
        /// Return a LineCap corresponding to the given value
        /// </summary>
        /// <param name="lineCap">Value of the LineCap</param>
        /// <returns>LineCap corresponding to the given value. If none was found, returns `default`</returns>
        public static LineCap FromString(string lineCap)
        {
            return lineCap switch {
                "butt" => Butt,
                "round" => Round,
                "square" => Square,
                _ => default,
            };
        }
    }

    internal class LineCapJsonConverter : JsonConverter<LineCap>
    {
        public override LineCap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => LineCap.FromString(reader.GetString());

        public override void Write(Utf8JsonWriter writer, LineCap value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }

    [JsonConverter(typeof(LineJoinJsonConverter))]
    public struct LineJoin
    {
        private readonly string _lineJoin;

        /// <summary>
        /// A join with a squared-off end which is drawn beyond the endpoint of the line at a distance of one-half of the lines width
        /// </summary>
        public static readonly LineJoin Bevel = new("bevel");

        /// <summary>
        /// A join with a sharp, angled corner which is drawn with the outer sides beyond the endpoint of the path until they meet
        /// </summary>
        public static readonly LineJoin Miter = new("miter");

        /// <summary>
        /// A join with a rounded end which is drawn beyond the endpoint of the line at a radius of one-half of the lines width and centered on the endpoint of the line
        /// </summary>
        public static readonly LineJoin Round = new("round");

        private LineJoin(string lineJoin) => _lineJoin = lineJoin;

        public override string ToString() => _lineJoin;

        /// <summary>
        /// Return a LineJoin corresponding to the given value
        /// </summary>
        /// <param name="lineJoin">Value of the LineJoin</param>
        /// <returns>LineJoin corresponding to the given value. If none was found, returns `default`</returns>
        public static LineJoin FromString(string lineJoin)
        {
            return lineJoin switch {
                "bevel" => Bevel,
                "miter" => Miter,
                "round" => Round,
                _ => default,
            };
        }
    }

    internal class LineJoinJsonConverter : JsonConverter<LineJoin>
    {
        public override LineJoin Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => LineJoin.FromString(reader.GetString());

        public override void Write(Utf8JsonWriter writer, LineJoin value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }

    /// <summary>
    /// Options used when rendering SimpleLine, SimplePolygon, CirclePolygon, LineString, MultiLineString, Polygon, and MultiPolygon objects in a line layer.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class LineLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// Specifies how the ends of the lines are rendered.
        /// </summary>
        public LineCap LineCap { get; set; }

        /// <summary>
        /// Specifies how the joints in the lines are rendered.
        /// </summary>
        public LineJoin LineJoin { get; set; }

        /// <summary>
        /// The amount of blur to apply to the line in pixels.
        /// </summary>
        public ExpressionOrNumber Blur { get; set; }

        /// <summary>
        /// The line's offset.
        /// A positive value offsets the line to the right, relative to the direction of the line.
        /// A negative value offsets to the left.
        /// </summary>
        public ExpressionOrNumber Offset { get; set; }

        /// <summary>
        /// Specifies the color of the line.
        /// </summary>
        public ExpressionOrString StrokeColor { get; set; }

        /// <summary>
        /// Specifies the lengths of the alternating dashes and gaps that form the dash pattern.
        /// Numbers must be equal or greater than 0. The lengths are scaled by the strokeWidth.
        /// To convert a dash length to pixels, multiply the length by the current stroke width.
        /// </summary>
        public IEnumerable<double> StrokeDashArray { get; set; }

        /// <summary>
        /// Defines a gradient with which to color the lines.
        ///  Requires the DataSource lineMetrics option to be set to true.
        /// </summary>
        public Expression StrokeGradient { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the line will be drawn.
        /// </summary>
        public ExpressionOrNumber StrokeOpacity { get; set; }

        /// <summary>
        /// The width of the line in pixels. Must be a value greater or equal to 0.
        /// </summary>
        public ExpressionOrNumber StrokeWidth { get; set; }

        /// <summary>
        /// The amount of offset in pixels to render the line relative to where it would render normally.
        /// </summary>
        public Pixel Translate { get; set; }

        /// <summary>
        /// Specifies the frame of reference for `translate`.
        /// </summary>
        public PitchAlignment TranslateAnchor { get; set; }
    }
}
