import React, { useEffect, useState } from "react";
import { Button, Card, CardBody, CardText, CardTitle, FormGroup, Input, Label, ListGroup, ListGroupItem, Table } from "reactstrap";
import { getActiveDrones, getDrones } from "../../managers/droneManager";
import { createRoute } from "../../managers/routeManager";
import { useNavigate } from "react-router-dom";


export default function OrderBuilder({ loggedInUser, detailsOrders , setDetailsOrders}) {
    const [drones, setDrones] = useState([]);
    const [ordered, setOrdered] = useState({});
    const [selectedDroneId, setSelectedDroneId] = useState(null);
    const [minimizeComponents, setMinimizeComponents] = useState(false);
    const navigate = useNavigate();

    const deleteClick = (e, order) => {
        e.preventDefault();

        //pass the order to the parent component
        setDetailsOrders((prevDetailsOrders) =>
      prevDetailsOrders.filter((item) => item.id !== order.id)
    );
    }
    const getAllDrones = () => {
        getActiveDrones().then(setDrones);
      };
    useEffect(() => {
        getAllDrones();
      }, []); 

      

      const handleSubmit = (event) => {
        event.preventDefault();

        const obj = {
          droneId: parseInt(selectedDroneId),
          expeditorId: loggedInUser.id,
          orders: detailsOrders
        };

        createRoute(obj).then((res) => {
        //console.log(res)
        //render list of orders in order
        setDetailsOrders([]);
        //navigate("/orders");
        setSelectedDroneId(null);
        setOrdered(res);
       })
      }


 return (<>
    <h2>Route Builder</h2>
    {detailsOrders.length > 0 
        ? ( <>
   <Table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Address</th>
            <th>Due Date</th>
            <th></th>
          </tr>
        </thead>
        
        <tbody>
        {detailsOrders?.map((order) => (
            <React.Fragment key={`Pizzas-${order.id}`}>
                <tr>
                <th scope="row">{order.id}</th>
                <td>{order?.address}</td>
                <td>{order?.deliveryDate.split("T")[0]}</td>
                <td>
                    <Button color="danger" onClick={(e) => deleteClick(e, order)}>Delete</Button>
                </td>
                </tr>

            </React.Fragment>
          ))}
          </tbody>
        </Table>
        <FormGroup>
          <Label>Choose drone</Label>
          <Input
            type="select"
            value={selectedDroneId}
            onChange={e => setSelectedDroneId(e.target.value)}
          >
            <option value="0">Choose drone</option>
            {drones.map((d) => (
              <option key={d.id} value={d.id}>
                {d.callsign}
              </option>
            ))}
          </Input>
        </FormGroup>
        <Button onClick={e => handleSubmit(e)} color="primary">
          Submit
        </Button>

        
          </>)
        : <></>
         
      }
      {ordered?.id 
              ? <FormGroup>         
              <Card
                  style={{
                    width: '18rem'
                  }}
                >
                  
                  <CardBody>
                    <CardTitle tag="h5">
                      Route: {ordered?.id}
                      <p class="routing-in-progress">Routing in progress...</p>
                    </CardTitle>
                    
                  </CardBody>
                  <ListGroup flush >
                    
                    {
                      ordered?.orders?.map(o => (
                        <ListGroupItem>{o.address}</ListGroupItem>
                      )) 
                    }
                    
                  </ListGroup>
      
                </Card>
            </FormGroup>
              : <></>}
 </>)
}
