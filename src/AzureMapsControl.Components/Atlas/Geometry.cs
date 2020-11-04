﻿namespace AzureMapsControl.Components.Atlas
{
    public abstract class Geometry
    {
        private string _type;

        public string Type
        {
            get => string.IsNullOrEmpty(_type) ? GetGeometryType() : _type; 
            set => _type = value;
        }

        internal abstract string GetGeometryType();
    }
}
