
import React, { useState, useEffect } from 'react';
import { getOrders } from "../managers/orderManager";
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


