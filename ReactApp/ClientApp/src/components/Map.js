import React, { Component } from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import { Icon } from 'leaflet';
import 'leaflet/dist/leaflet.css';

export class Map extends Component {
    static displayName = Map.name;

    render() {
        let position = [52.51, 13.38];
        let customIcon = new Icon({
            iconUrl: '/icons8-select-24.png',
            iconSize: [20, 20],
        });
        return (
            <div className='map'>
                <MapContainer center={position} zoom={6} scrollWheelZoom={true}
                style={{height: "400px", backgroundColor: "red", marginTop: "80px", marginBottom: '90px'}}>
                    <TileLayer
                        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    />
                    <Marker position={position}
                        icon={customIcon}
                    >
                        <Popup>
                            🐻🍻🎉
                        </Popup>
                    </Marker>
                </MapContainer>
            </div>
        );
    }
}