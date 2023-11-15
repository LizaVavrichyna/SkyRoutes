import { useEffect, useState } from "react";
import {
    Card,
    CardBody,
    CardTitle,
    CardText,

    Button,
    ListGroup,
    ListGroupItem,
    Col,
    Row,
    Collapse,
  } from "reactstrap";
import { deleteRoute, getRoutes } from "../../managers/routeManager";
import MapContainer from "./MapContainer";


export default function RouteList({ loggedInUser }) {
    const [routes, setRoutes] = useState([]);
    const [openCardId, setOpenCardId] = useState(null);

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
      const containerStyle = {
        width: '100%',
        height: '400px',
      };
      const zoom = 10;

      const toggle = (routeId) => {
        if (openCardId === routeId) {
          // If the clicked card is already open, close it
          setOpenCardId(null);
        } else {
          // Otherwise, open the clicked card
          setOpenCardId(routeId);
        }
      };

    return (
        <><br></br><h2>Hub: 50 Airways Blvd, Nashville, TN</h2>
       
        <Row>
        {routes.map((route) => (
            <Col key={route.id}>
          <Card
          style={{
            width: '30rem',
            margin: '1rem'
          

          }}>
            <CardBody  >
                <CardTitle tag="h5" >
                Route: {route?.id}
                </CardTitle>
                
                <CardText >
                <p>Expeditor: {route?.expeditor?.fullName}</p>

                <p>Drone: {route?.drone?.callsign}</p>
                <p>Date: {route?.deliveredOn.split("T")[0]}</p>
                
                </CardText>
                <MapContainer addresses={route?.orders} containerStyle={containerStyle} zoom={zoom}/>
                {loggedInUser.roles.includes("Admin")
                    ?<Button style={{
                      
                      marginTop: '1rem'
                    
          
                    }} outline color="danger" onClick={(e) => deleteClick(e, route)}>Delete</Button>
                    : <></>}
            </CardBody>
<Button
                color="dark"
                outline
                onClick={() => toggle(route.id)} // Pass the route ID to the toggle function
                style={{ marginBottom: '1rem' }}
              >
                Order addresses
              </Button>
              <Collapse isOpen={openCardId === route.id}>
                <ListGroup flush numbered>
                  {route?.orders?.map((o) => (
                    <ListGroupItem>{o.address}</ListGroupItem>
                  ))}
                </ListGroup>
              </Collapse>
            
                    </Card>
                    </Col>
        ))}
      </Row></>
        
    );
  }