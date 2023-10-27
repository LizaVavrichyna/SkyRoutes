import { useState } from "react";
import DroneDetails from "./DroneDetails";
import DroneList from "./DroneList";

export default function Drones({ loggedInUser  }) {
    const [detailsDroneId, setDetailsDroneId] = useState(null);

    return (
      <div className="container" >
        <div className="row">
          <div className="col-sm-6">
            <DroneList setDetailsDroneId={setDetailsDroneId} />
          </div>
          <div className="col-sm-6">
            <DroneDetails detailsDroneId={detailsDroneId} setDetailsDroneId={setDetailsDroneId} loggedInUser={loggedInUser}/>
          </div>
        </div>
      </div>
    );
  }