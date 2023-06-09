﻿namespace AzureMapsControl.Components.Tests.Data
{
    using System.Collections.Generic;
    using System.Text.Json;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class DataSourceOptionsJsonConverterTests : JsonConverterTests<DataSourceOptions>
    {
        public DataSourceOptionsJsonConverterTests() : base(new DataSourceOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new DataSourceOptions {
                Buffer = 1,
                Cluster = true,
                ClusterMaxZoom = 2,
                ClusterRadius = 3,
                LineMetrics = true,
                MaxZoom = 4,
                Tolerance = 5,
                ClusterProperties = new Dictionary<string, Expression> {
                    { "total", new Expression(JsonDocument.Parse("[\"==\", [\"get\", \"cell_id\"], \"\"]")) }
                }
            };

            var expectedJson = "{"
                + "\"buffer\":1"
                + ",\"cluster\":true"
                + ",\"clusterMaxZoom\":2"
                + ",\"clusterProperties\":{"
                + "\"total\":[\"==\",[\"get\",\"cell_id\"],\"\"]"
                + "}"
                + ",\"clusterRadius\":3"
                + ",\"lineMetrics\":true"
                + ",\"maxZoom\":4"
                + ",\"tolerance\":5"
                + "}";

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");

        [Fact]
        public void Should_Read()
        {
            var json = "{"
                + "\"buffer\":1"
                + ",\"cluster\":true"
                + ",\"clusterMaxZoom\":2"
                + ",\"clusterProperties\":{"
                + "\"total\":[\"==\",[\"get\",\"cell_id\"],\"\"]"
                + "}"
                + ",\"clusterRadius\":3"
                + ",\"lineMetrics\":true"
                + ",\"maxZoom\":4"
                + ",\"tolerance\":5"
                + "}";

            var options = Read(json);
            Assert.Equal(1, options.Buffer);
            Assert.True(options.Cluster);
            Assert.Equal(2, options.ClusterMaxZoom);
            Assert.Null(options.ClusterProperties);
            Assert.Equal(3, options.ClusterRadius);
            Assert.True(options.LineMetrics);
            Assert.Equal(4, options.MaxZoom);
            Assert.Equal(5, options.Tolerance);
        }
    }
}
