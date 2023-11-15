import {
    Card,
    CardBody,
    CardTitle,
    CardText,
    CardSubtitle,
    Button,
  } from "reactstrap";
  import {  useNavigate } from "react-router-dom";


  export default function DroneCard({ drone, setDetailsDroneId }) {

    
    return (
      <Card color={drone.isActive ? "warning" : "secondary"} style={{ marginBottom: "4px" }}>
        
        <CardBody>
          <CardTitle tag="h5">{drone.callsign}</CardTitle>
          <CardSubtitle className="mb-2 text-muted" tag="h6">
            Model: {drone.model}
          </CardSubtitle>
          
          <Button
             outline
            color="dark"
            style={{marginBottom: "1rem",
        width: 150
      }}
            onClick={() => {
              setDetailsDroneId(drone.id);
              window.scrollTo({
                top: 0,
                left: 0,
                behavior: "smooth",
              });
            }}
          >
            Show Details
          </Button>
          
          
        </CardBody>
      </Card>
    );
  }