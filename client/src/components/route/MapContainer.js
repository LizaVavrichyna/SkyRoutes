import React, { useState, useEffect, useCallback } from 'react';
import { GoogleMap, useJsApiLoader, Marker } from '@react-google-maps/api';
import "./map.css"


export default function MapContainer({ addresses, containerStyle, zoom }) {
  const [markers, setMarkers] = useState([]);
  const [showMarkers, setShowMarkers] = useState(0); // Initialize with 0
  const { isLoaded } = useJsApiLoader({
    id: 'google-map-script',
    googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY
  });
  const [map, setMap] = useState(null);
  

  const image = "https://images.rawpixel.com/image_png_800/czNmcy1wcml2YXRlL3Jhd3BpeGVsX2ltYWdlcy93ZWJzaXRlX2NvbnRlbnQvbHIvcGQyMzMtbWFzdGVybXJnMDIzMDIzNjMxMWFfMy5wbmc.png"
  const center = {
    lat: 36.162237,
    lng: -86.776977,
  };

  useEffect(() => {
    if (isLoaded) {

      //set markers
      const newMarkers = addresses?.map((address, i) => (
        {
        position: {
          lat: address?.latitude,
          lng: address?.longitude,
        },
        label: `${i + 1}`,
        animation: window.google.maps.Animation.DROP,
        //icon: image,
      }
      ));
  
      setMarkers(newMarkers);
      
    }
  }, [isLoaded, addresses, showMarkers]);



  const onUnmount = useCallback(() => {
    setMap(null);
  }, []);

  return (
    <div class="map" id="map" style={containerStyle}>
      {isLoaded ? (
        <GoogleMap
          mapContainerStyle={containerStyle}
          mapTypeId={window.google.maps.MapTypeId.SATELLITE}
          
          center={center}
          zoom={zoom}
          heading={0}
          tilt={25}
          onUnmount={onUnmount}
        >
          {markers?.map((marker,i) => {
          

          return (
            <Marker
              key={`${i + 1}-${marker?.label}`}
              position={marker?.position}
              label={marker?.label}
              animation={marker?.animation}
              //icon={marker?.icon}
            />
            
          )})}
        </GoogleMap>
      ) : (
        <></>
      )}
    </div>
  );
  
}