import { useState } from "react";

import TicketList from "./TicketList";

export default function Tickets({ loggedInUser  }) {
    const [detailsTicketId, setDetailsTicketId] = useState(null);

    return (
      <div className="container" >
        <div className="row">
          <div className="col-sm-8">
            <TicketList setDetailsTicketId={setDetailsTicketId} loggedInUser={loggedInUser} />
          </div>

        </div>
      </div>
    );
  }