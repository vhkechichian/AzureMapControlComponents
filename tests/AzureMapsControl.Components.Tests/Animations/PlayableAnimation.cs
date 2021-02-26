﻿namespace AzureMapsControl.Components.Tests.Animations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class AnimationTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new();

        internal class AnimationFactory
        {
            internal static Animation GetAnimation(string type, string id, IMapJsRuntime runtime)
            {
                return type switch {
                    "dropmarkers" => new DropMarkersAnimation(id, runtime),
                    "flowingdashed" => new FlowingDashedLineAnimation(id, runtime),
                    "movealongpath" => new MoveAlongPathAnimation(id, runtime),
                    "snakeline" => new SnakeLineAnimation(id, runtime),
                    _ => throw new NotSupportedException(type),
                };
            }
        }

        public static IEnumerable<object[]> AllAnimationsTypes => 
            new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "flowingdashed" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" }
            };

        public static IEnumerable<object[]> AllSeekAnimationsTypes =>
            new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" }
            };

        public static IEnumerable<object[]> AllSetOptionsAnimationsTypes =>
            new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" }
            };

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public void Should_Create(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            Assert.Equal(id, animation.Id);
        }

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public async void Should_DisposeAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.DisposeAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public async void Should_PauseAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.PauseAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public async void Should_PlayAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.PlayAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public async void Should_ResetAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.ResetAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllSeekAnimationsTypes))]
        public async void Should_SeekAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            var seek = 0.5m;
            await (animation as Animation).SeekAsync(seek);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), id, seek), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public async void Should_StopAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.StopAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllSetOptionsAnimationsTypes))]
        public async void Should_SetOptionsAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);

            var newOptions = new Mock<IAnimationOptions>();
            await (animation as Animation<IAnimationOptions>).SetOptionsAsync(newOptions.Object);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), id, newOptions.Object), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
