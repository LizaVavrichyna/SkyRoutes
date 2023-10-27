import React, { useEffect, useState } from "react";
import { Button, Input, Table } from "reactstrap";
import { assignTicket, closeTicket, getTickets } from "../../managers/ticketManager";

export default function TicketList({ detailsTicketId, setDetailsTicketId, loggedInUser }) {
  const [tickets, setTickets] = useState([]);
  const [body, setBody] = useState('');

  const getAllTickets = () => {
    getTickets().then(setTickets);
  };

  // Create a state object to track whether the body input is open for each ticket
  const [openBodyForm, setOpenBodyForm] = useState({});

  const handleCloseTicket = (t) => {
    // Open the body input field only for the specific ticket
    setOpenBodyForm((prevOpenBodyForm) => ({
      ...prevOpenBodyForm,
      [t.id]: true,
    }));
  };

  const handleSubmitClose = (t) => {
    const obj = {
      id: t.id,
      technicianId: t.technicianId,
      droneId: t.droneId,
      repairTypeId: t.repairTypeId,
      submittedById: t.submittedById,
      inRepairSince: t.inRepairSince,
      repairSummary: body,
      open: false,
    };

    closeTicket(obj).then(() => {
      // Hide the body input field for the specific ticket
      setOpenBodyForm((prevOpenBodyForm) => ({
        ...prevOpenBodyForm,
        [t.id]: false,
      }));
      // Clear the input field
      getAllTickets();
      setBody('');
    });
  };

  const handleAssignTicket = (e, t) => {
    e.preventDefault();
    const obj = {
      id: t.id,
      technicianId: loggedInUser.id,
      droneId: t.droneId,
      repairTypeId: t.repairTypeId,
      submittedById: t.submittedById,
      inRepairSince: t.inRepairSince,
      repairSummary: '',
      open: true,
    };
    console.log(obj);
    assignTicket(obj).then(() => {
      getAllTickets();
    });
  };

  useEffect(() => {
    getAllTickets();
    console.log('test');
  }, []);

  return (
    <>
      <h2>Tickets</h2>
      <Table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Drone</th>
            <th>Repair Type</th>
            <th>Date In</th>
            <th>Date Out</th>
            <th>Technician</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {tickets?.map((ticket) => (
            <React.Fragment key={`ticket-${ticket?.id}`}>
              <tr>
                <th scope="row">{ticket?.id}</th>
                <td>{ticket?.drone?.callsign}</td>
                <td>{ticket?.repairType?.name}</td>
                <td>{ticket?.inRepairSince.split('T')[0]}</td>
                <td>{ticket?.outOfRepair?.split('T')[0]}</td>
                <td>{ticket?.technician?.fullName}</td>
                <td>
                  {ticket?.technicianId === loggedInUser.id ? (
                    openBodyForm[ticket.id] ? (
                      <>
                        <Input
                          type="text"
                          value={body}
                          onChange={(e) => setBody(e.target.value)}
                        />
                        <Button onClick={() => handleSubmitClose(ticket)}>Submit</Button>
                      </>
                    ) : (
                      ticket.open ? (
                        <Button onClick={() => handleCloseTicket(ticket)}>Close</Button>
                      ) : (
                        `${ticket?.repairSummary}`
                      )
                    )
                  ) : (
                    <Button onClick={(e) => handleAssignTicket(e, ticket)}>Assign</Button>
                  )}
                </td>
              </tr>
            </React.Fragment>
          ))}
        </tbody>
      </Table>
    </>
  );
}