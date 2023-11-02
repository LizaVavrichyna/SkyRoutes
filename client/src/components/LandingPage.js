
import { Wrapper } from "@googlemaps/react-wrapper";
import React, { useState, useEffect, useCallback } from 'react';
import { getOrders, getTodayOrders } from "../managers/orderManager";

import { GoogleMap, useJsApiLoader, Marker } from '@react-google-maps/api';
import MapContainer from "./route/MapContainer";
export default function LandingPage({ loggedInUser }) {
    const [addresses, setAddresses] = useState([]);

    const getAllAddresses = () => {
        getOrders().then(setAddresses);
    };
    useEffect(() => {
        getAllAddresses();
       
    },[]);

    const containerStyle = {
        width: '100%',
        height: '1000px',
      };

    const zoom = 17;
    return  (
        addresses?.length > 0
        ? <MapContainer addresses={addresses} containerStyle={containerStyle} zoom={zoom}/>
        : <div>Loading addresses...</div>
    )
   
}


