import React, { useEffect, useState } from "react";
import { createOrder, deleteOrder, editOrder, getOrders } from "../../managers/orderManager";
import { Button, Form, FormGroup, Input, Label, Table } from "reactstrap";
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";


export default function OrderList({  setDetailsOrders }) {
 

    const [orders, setOrders] = useState([]);
    const [showOrderForm, setShowOrderForm] = useState(false);
    const [newOrder, setNewOrder] = useState({
      address: '',
      deliveryDate: '',
    });
    const getAllOrders = () => {
      getOrders().then(setOrders);
    };
    const onSelectOrder = (e,order) => {
      order.delivered = true;
     //pass the order to the parent component
      setDetailsOrders((prevDetailsOrders) => [...prevDetailsOrders, order]);
    };


    const handleToggleForm = () => {
      setShowOrderForm(!showOrderForm);
    };
  
    const handleInputChange = (event) => {
      const { name, value } = event.target;
      setNewOrder({
        ...newOrder,
        [name]: value,
      });
    };
    const handleSubmitOrder = (e) => {
      e.preventDefault();
      const obj = {
        address: newOrder.address,
        deliveryDate: newOrder.deliveryDate,
        latitude: 0,
        longitude: 0,
        delivered: false
      };
      console.log(obj)
       createOrder(obj).then(() => {
          setShowOrderForm(!true)
          getAllOrders();        
       });
    };


  

    const deleteClick = (e, order) => {
      e.preventDefault();
      deleteOrder(order.id).then(() => {
        getAllOrders();
      }) 
    }
    useEffect(() => {
      getAllOrders();
    }, []);


    return (
      <>
        <h2>Orders</h2>
        <Button onClick={handleToggleForm}>
        {showOrderForm ? 'Done' : 'Create Order'}
      </Button>
      {showOrderForm && (
        <>
        <FormGroup>
        <Label>Enter Address</Label>
          <Input
            type="text"
            name="address"
            placeholder="Address"
            value={newOrder.address}
            onChange={handleInputChange}
          />
          </FormGroup>
          <FormGroup>
         <Label>Choose Delivery Date</Label>    
         <br></br>
          <DatePicker
            selected={newOrder.deliveryDate}
            onChange={(date) => setNewOrder({ ...newOrder, deliveryDate: date })}
          />
          </FormGroup>
          <FormGroup>
          <Button color="primary" onClick={handleSubmitOrder}>Submit</Button>
          </FormGroup>
          </>
      )}
            <Table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Address</th>
            <th>Due Date</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
        {orders.map((order) => (
          <>
            <React.Fragment key={`orders-${order.id}`}>
                <tr>
                <th scope="row">{order.id}</th>
                <td>{order?.address}</td>
                <td>{order?.deliveryDate.split("T")[0]}</td>
                        
                <td>
                    {!order?.delivered
                      ?<Button onClick={(e) => onSelectOrder(e, order) }>Deliver</Button>
                  : <></>}
                </td>

                <td>
                    
                    {!order?.delivered
                      ?<Button color="danger" onClick={(e) => deleteClick(e, order)}>Delete</Button>
                  : <></>}
                </td>
                
                </tr>
                

        
            </React.Fragment>

          </>
          ))}
          
          </tbody>
            
        </Table>
      </>
    );
  }