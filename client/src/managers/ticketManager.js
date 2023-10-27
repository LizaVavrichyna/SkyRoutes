const apiUrl = "/api/ticket";

export const getTickets = () => {
    return fetch(apiUrl).then((res) => res.json());
};

export const getDroneTickets = (droneId) => {
    return fetch(`${apiUrl}/${droneId}`).then((res) => res.json());
};

export const createTicket = (ticket) => {
    return fetch(apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(ticket),
    }).then((res) => res.json());
}

export const closeTicket = (ticket) => {
    return fetch(`${apiUrl}/close`, {
        method: "PUT",
        headers: {
        "Content-Type": "application/json",
    },
    body: JSON.stringify(ticket),
  });
};

export const assignTicket = (ticket) => {
    return fetch(`${apiUrl}/assign`, {
        method: "PUT",
        headers: {
        "Content-Type": "application/json",
    },
    body: JSON.stringify(ticket),
  });
};