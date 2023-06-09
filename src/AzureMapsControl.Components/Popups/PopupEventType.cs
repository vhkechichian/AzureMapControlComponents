﻿namespace AzureMapsControl.Components.Popups
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class PopupEventType : AtlasEventType
    {
        public static readonly PopupEventType Close = new PopupEventType("close");
        public static readonly PopupEventType Drag = new PopupEventType("drag");
        public static readonly PopupEventType DragEnd = new PopupEventType("dragend");
        public static readonly PopupEventType DragStart = new PopupEventType("dragstart");
        public static readonly PopupEventType Open = new PopupEventType("open");

        private PopupEventType(string type) : base(type) { }
    }
}
