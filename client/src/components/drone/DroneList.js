import { useEffect, useState } from "react";
import { createDrone, getDrones } from "../../managers/droneManager";
import DroneCard from "./DroneCard";
import { Button, FormGroup, Input, Label } from "reactstrap";

export default function DroneList({ setDetailsDroneId }) {
    const [drones, setDrones] = useState([]);
    const [showForm, setShowForm] = useState(false);
    const [newDrone, setNewDrone] = useState({
      model: '',
      callsign: '',
      imageLocation: '',
      distanceCap: 0,
      isActive: true,
      inHangar: true
    });

    const getAllDrones = () => {
      getDrones().then(setDrones);
    };
  
    useEffect(() => {
      getAllDrones();
    }, []);

    const handleToggleForm = () => {
      setShowForm(!showForm);
    };
    const handleInputChange = (event) => {
      const { name, value } = event.target;
      setNewDrone({
        ...newDrone,
        [name]: value,
      });
    };
    const handleSubmit = (e) => {
      e.preventDefault();
      
      console.log(newDrone)
      createDrone(newDrone).then(() => {
          setShowForm(!true)
          getAllDrones();        
       });
    };
    return (
      <>
        <h2>Drones</h2>
        <Button onClick={handleToggleForm}  style={{marginBottom: "1rem"}}>
        {showForm ? 'Done' : 'Add Drone'}
        </Button>
        {showForm && (
        <>
        <FormGroup>
        <Label>Enter Model</Label>
          <Input
            type="text"
            name="model"
            placeholder="Model"
            value={newDrone.model}
            onChange={handleInputChange}
          />
          </FormGroup>

          <FormGroup>
          <Label>Enter Call-sign</Label>
          <Input
            type="text"
            name="callsign"
            placeholder="Callsign"
            value={newDrone.callsign}
            onChange={handleInputChange}
          />
          </FormGroup>

          <FormGroup>
          <Label>Enter Distance Cap</Label>
          <Input
            type="number"
            name="distanceCap"
            placeholder="Distance"
            value={newDrone.distanceCap}
            onChange={handleInputChange}
          />
          </FormGroup>

          <FormGroup>
          <Label>Enter Image URL</Label>
          <Input
            type="text"
            name="imageLocation"
            placeholder="ImageURL"
            value={newDrone.imageLocation}
            onChange={handleInputChange}
          />
          </FormGroup>

          <FormGroup>
          <Button color="primary" onClick={handleSubmit}>Submit</Button>
          </FormGroup>
          </>
      )}
        {drones.map((drone) => (
          <DroneCard
            drone={drone}
            setDetailsDroneId={setDetailsDroneId}
            key={`drone-${drone.id}`}
          ></DroneCard>
        ))}
      </>
    );
  }