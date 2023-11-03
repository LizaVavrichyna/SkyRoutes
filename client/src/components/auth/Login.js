import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../../managers/authManager";
import { Button, FormFeedback, FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import LoginModal from "./LoginModal";
import { getHubAddress, getOrders } from "../../managers/orderManager";
import MapContainer from "../route/MapContainer";

export default function Login({ setLoggedInUser }) {
  const [hub, setHub] = useState([]);

  const getHub = () => {
    getHubAddress().then(data => setHub(data))
  };
  useEffect(() => {
      getHub();
     
  },[]);

  const containerStyle = {
        width: '100%',
        height: '1000px',
      };
  const zoom = 17;


  return (
    <div  style={{ maxWidth: "100%" }}>
      {hub?.length > 0
        ? <MapContainer addresses={hub} containerStyle={containerStyle} zoom={zoom}/>
        : <div>Loading map...</div>
      }
        </div>
  );
}
