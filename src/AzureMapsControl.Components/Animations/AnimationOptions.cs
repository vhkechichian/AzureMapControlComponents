﻿namespace AzureMapsControl.Components.Animations
{
    public struct AnimationOptions : IAnimationOptions, IDisposableAnimationOptions, IDurationAnimationOptions
    {
        public bool? AutoPlay { get; set; }
        public bool? DisposeOnComplete { get; set; }
        public int? Duration { get; set; }
        public Easing Easing { get; set; }
        public bool? Loop { get; set; }
        public bool? Reverse { get; set; }
        public decimal? SpeedMultiplier { get; set; }
    }
}
