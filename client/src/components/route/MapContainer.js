import React, { useState, useEffect, useCallback } from 'react';
import { GoogleMap, useJsApiLoader, Marker } from '@react-google-maps/api';

export default function MapContainer({ addresses }) {
  const [markers, setMarkers] = useState([]);
  const { isLoaded } = useJsApiLoader({
    id: 'google-map-script',
    //googleMapsApiKey: 'AIzaSyBetAXMXQ492XNty05zwtWsoAwdsPOzUfQ',
    googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY
  });
  const [map, setMap] = useState(null);

  const containerStyle = {
    width: '100%',
    height: '400px',
  };

  const center = {
    lat: 36.1300431,
    lng: -86.6919461,
  };

  useEffect(() => {
    if (isLoaded) {
      // Create the map instance
      const mapInstance = new window.google.maps.Map(document.getElementById('map'), {
        center: center,
        zoom: 10,
      });
      setMap(mapInstance);

      // Update markers based on addresses
      const newMarkers = addresses?.map((address, i) => ({
        position: {
          lat: address?.latitude,
          lng: address?.longitude,
        },
        label: `${i+1}`,
      }));
      setMarkers(newMarkers);
      
    }
  }, [isLoaded, addresses]);

  const onUnmount = useCallback(() => {
    setMap(null);
  }, []);

  return (
    <div id="map" style={containerStyle}>
      {isLoaded ? (
        <GoogleMap
          mapContainerStyle={containerStyle}
          center={center}
          zoom={10}
          onLoad={(map) => {
            // Set the map instance
            setMap(map);
          }}
          onUnmount={onUnmount}
        >
          {markers?.map((marker,i) => (
            <Marker key={`${i+1}-${marker?.label}`}
              position={marker?.position}
              label={marker?.label} />
          ))}
        </GoogleMap>
      ) : (
        <></>
      )}
    </div>
  );
  
}