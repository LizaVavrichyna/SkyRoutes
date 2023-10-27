import { useEffect, useState } from "react";
import { getRepairTypes } from "../../managers/repairTypeManager";
import { Button, FormGroup, Input, Label } from "reactstrap";
import { createTicket } from "../../managers/ticketManager";

export default function TicketBuilder({ onTicketUpdate, drone , loggedInUser, setShowTicketForm}) {
    const [repairTypes, setRepairTypes] = useState([]);
    const [selectedRepairTypeId, setSelectedRepairTypeId] = useState(null);
    //get RepairTypes
    const getAllRepairTypes = () => {
        getRepairTypes().then(setRepairTypes);
      };
    useEffect(() => {
        getAllRepairTypes();
    }, [drone]);

    const handleSubmitTicket = (e) => {
        e.preventDefault();
        const obj = {
          droneId: drone.id,
          repairTypeId: parseInt(selectedRepairTypeId),
          submittedById: loggedInUser.id,
          
          repairSummary: "",
          open: true
        };
        console.log(obj)
        createTicket(obj).then(() => {
            setShowTicketForm(!true); //close create ticket modal
            //onTicketUpdate();       //trigger refresh of tickets
         });
      };

    return (
        <div>
          <FormGroup>
          <Label>Choose Repair Type</Label>
          <Input
            type="select"
            value={selectedRepairTypeId}
            onChange={e => setSelectedRepairTypeId(e.target.value)}
          >
            <option value="0">Choose Size</option>
            {repairTypes.map((rt) => (
              <option key={rt.id} value={rt.id}>
                {rt.name}
              </option>
            ))}
          </Input>
        </FormGroup>

          <Button onClick={e => handleSubmitTicket(e)}>Submit Ticket</Button>
        </div>
    );
}