import { useEffect, useState } from "react";
import {
    Card,
    CardBody,
    CardTitle,
    CardText,
    CardSubtitle,
    Button,
    ListGroup,
    ListGroupItem,
    Col,
    Row,
  } from "reactstrap";
import { deleteRoute, getRoutes } from "../../managers/routeManager";
import MapContainer from "./MapContainer";
export default function RouteList({ loggedInUser }) {
    const [routes, setRoutes] = useState([]);
  
    const getAllRoutes = () => {
      getRoutes().then(setRoutes);
    };
  
    useEffect(() => {
      getAllRoutes();
    }, []);

    const deleteClick = (e, route) => {
        e.preventDefault();
        deleteRoute(route.id).then(() => {
          getAllRoutes();
        }) 
      }
    return (
        <><h2>Hub: 50 Airways Blvd, Nashville, TN</h2>
        <h5>All routes start at the hub</h5>
        <Row>
        {routes.map((route) => (
            <Col key={route.id}>
          <Card
          style={{
            width: '30rem',
            margin: '1rem'
          }}>
            <CardBody style={{ backgroundColor: "yellow" }} >
                <CardTitle tag="h5" >
                Route: {route?.id}
                </CardTitle>
                
                <CardText >
                <p>Expeditor: {route?.expeditor?.fullName}</p>

                <p>Drone: {route?.drone?.callsign}</p>
                <p>Date: {route?.deliveredOn.split("T")[0]}</p>
                
                </CardText>
                <MapContainer addresses={route?.orders}/>
                {loggedInUser.roles.includes("Admin")
                    ?<Button color="danger" onClick={(e) => deleteClick(e, route)}>Delete</Button>
                    : <></>}
            </CardBody>
            <ListGroup flush numbered>
                {
                route?.orders?.map(o => (
                    <ListGroupItem>{o.address}</ListGroupItem>
                )) 
                }
                
            </ListGroup>
                    </Card>
                    </Col>
        ))}
      </Row></>
        
    );
  }