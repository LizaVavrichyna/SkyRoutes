import { useEffect, useState } from "react";

import OrderList from "./OrderList";
import OrderBuilder from "./OrderBuilder";

export default function Orders({loggedInUser}) {
    const [detailsOrders, setDetailsOrders] = useState([]);

    return (
      <div className="container" >
        <div className="row">
          <div className="col-sm-6" style={{paddingTop: "2rem"}}>
            <OrderList setDetailsOrders={setDetailsOrders} />
          </div>
          <div className="col-sm-6" style={{paddingTop: "2rem"}}>
            <OrderBuilder loggedInUser={loggedInUser} detailsOrders={detailsOrders} setDetailsOrders={setDetailsOrders}/>
          </div>
        </div>
      </div>
    );
  }