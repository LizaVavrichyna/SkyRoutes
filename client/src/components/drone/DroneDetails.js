import React, { useEffect, useState } from "react";
import { deleteDrone, getDroneDetails } from "../../managers/droneManager";
import { getDroneTickets } from "../../managers/ticketManager";
import { Button, Card, CardBody, CardTitle } from "reactstrap";
import TicketBuilder from "../ticket/TicketBuilder";
import { useNavigate } from "react-router-dom";

export default function DroneDetails({ detailsDroneId, loggedInUser }) {
    const [drone, setDrone] = useState(null);
    const [tickets, setTickets] = useState([]);
    const [showTicketForm, setShowTicketForm] = useState(false);
    const [reget,setReget] = useState(false);
    const navigate = useNavigate();
    //get drone details
    //const getDroneDetails = (id) => {
      //getDroneById(id).then(setDrone);
   // };
    useEffect(() => {
        if (detailsDroneId) {
          getDroneDetails(detailsDroneId).then(setDrone);
        }
    }, [detailsDroneId]);

    //get drone tickets
   //const getAllTickets = () => {
      //  getTickets(detailsDroneId).then(setTickets);
      //};
    useEffect(() => {
        if (detailsDroneId) {
            getDroneTickets(detailsDroneId).then(setTickets);
        }
      }, [reget]);

    const handleToggleForm = () => {
        setShowTicketForm(!showTicketForm);
    };
  
    if (!drone) {
      return (
        <>
          <h2>Drone Details</h2>
          <p>Please choose a Drone...</p>
        </>
      );
    }
    const deleteClick = (e, drone) => {
      e.preventDefault();
      console.log(drone)
      deleteDrone(drone).then(() => {
        navigate("/drones");
      }) 
    }
    return (
      <>
        <h2>Drone Details</h2>

        <Card>
        <img
    alt="Sample"
    src={drone?.imageLocation}
  />
          <CardBody>
            <CardTitle tag="h4">{drone.id}. {drone.callsign}</CardTitle>
            {
            drone.isActive && loggedInUser.roles.includes("Admin")
            ?<Button
            color="danger"
            onClick={(e) => {
              deleteClick(e, drone);
              
            }}
          >
            Deactivate
          </Button>
            : <></>
          }
            
            <p>Model: {drone.model}</p>
            <p>In Fleet Since: {drone.inFleetSince.split("T")[0]}</p>
            <p>Distance(km): {drone?.distanceCap}</p>


        <Button color="danger" onClick={handleToggleForm}>
        {showTicketForm ? 'Done' : 'Repair Request'}
        </Button>
        {showTicketForm && (
        <TicketBuilder onTicketUpdate={() => setReget(true)} drone={drone} loggedInUser={loggedInUser} setShowTicketForm={setShowTicketForm}/>
      )}
        
          </CardBody>
        </Card>   
      </>
    );
  }