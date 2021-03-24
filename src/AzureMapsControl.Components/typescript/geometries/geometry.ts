export interface Geometry {
    coordinates: any;
    bbox: {
        east: number;
        north: number;
        south: number;
        west: number;
    },
    id: string;
    type: 'LineString' | 'MultiLineString' | 'MultiPoint' | 'MultiPolygon' | 'Point' | 'Polygon';
}

export interface Feature {
    id: string;
    bbox: {
        east: number;
        north: number;
        south: number;
        west: number;
    },
    geometry: Geometry,
    properties: { [key: string]: any }
}