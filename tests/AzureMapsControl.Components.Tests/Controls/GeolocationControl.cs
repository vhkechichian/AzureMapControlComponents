﻿namespace AzureMapsControl.Components.Tests.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Geolocation;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class GeolocationControlTests
    {
        private readonly Mock<IMapJsRuntime> _mapJsRuntimeMock = new Mock<IMapJsRuntime>();

        [Fact]
        public async void Should_GetLastKnowPositionAsync()
        {
            var feature = new Feature<Point>();
            _mapJsRuntimeMock.Setup(runtime => runtime.InvokeAsync<Feature<Point>>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(feature);

            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            var result = await control.GetLastKnownPositionAsync();
            Assert.Equal(feature, result);

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeAsync<Feature<Point>>(Constants.JsConstants.Methods.GeolocationControl.GetLastKnownPosition.ToGeolocationControlNamespace(), control.Id), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotGetLastKnowPosition_IfAlreadyDisposed_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ControlDisposedException>(async () => await control.GetLastKnownPositionAsync());

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), control.Id), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotGetLastKnowPosition_NotAddedToMapCase_Async()
        {
            var control = new GeolocationControl();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.GetLastKnownPositionAsync());

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_DisposeAsync()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            var disposedCalled = false;
            control.OnDisposed += () => disposedCalled = true;

            await control.DisposeAsync();

            Assert.True(control.Disposed);
            Assert.True(disposedCalled);
            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), control.Id), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotDispose_IfAlreadyDisposed_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };
            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ControlDisposedException>(async () => await control.DisposeAsync());

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), control.Id), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotDispose_NotAddedToMapCase_Async()
        {
            var control = new GeolocationControl();
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.DisposeAsync());

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOptions_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            await control.SetOptionsAsync(options => options.CalculateMissingValues = true);

            Assert.True(control.Options.CalculateMissingValues);

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.SetOptions.ToGeolocationControlNamespace(), control.Id, control.Options), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetOptions_DisposedCase_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ControlDisposedException>(async () => await control.SetOptionsAsync(options => options.CalculateMissingValues = true));

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), control.Id), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetOptions_NotAddedToMapCase_Async()
        {
            var control = new GeolocationControl();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.SetOptionsAsync(options => options.CalculateMissingValues = true));

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddEvents_Async()
        {
            var control = new GeolocationControl(eventFlags: GeolocationEventActivationFlags.None().Enable(GeolocationEventType.GeolocationError)) {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            await control.AddEventsAsync();

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.AddEvents.ToGeolocationControlNamespace(), It.Is<object[]>(parameters =>
               (parameters[0] as Guid?).GetValueOrDefault() == control.Id
               && parameters[1] is IEnumerable<string>
               && (parameters[1] as IEnumerable<string>).Single() == GeolocationEventType.GeolocationError.ToString()
               && parameters[2] is DotNetObjectReference<GeolocationEventInvokeHelper>
            )), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddEvents_NoEvents_Async()
        {
            var control = new GeolocationControl(eventFlags: GeolocationEventActivationFlags.None()) {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            await control.AddEventsAsync();

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddEvents_NullCase_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            await control.AddEventsAsync();

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}