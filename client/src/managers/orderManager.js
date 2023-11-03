const apiUrl = "/api/order";

export const getOrders = () => {
    return fetch(apiUrl).then((res) => res.json());
};
export const getTodayOrders = () => {
  return fetch(`${apiUrl}/today`).then((res) => res.json());
};
export const getOrderById = (id) => {
    return fetch(`${apiUrl}/${id}`).then((res) => res.json());
};

export const getHubAddress = () => {
  return fetch(`${apiUrl}/hub`).then((res) => res.json());
};

export const deleteOrder = (orderId) => {
    return fetch(`${apiUrl}/${orderId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });
};
export const editOrder = (order) => {
  return fetch(`${apiUrl}/${order.id}`, {
      method: "PUT",
      headers: {
      "Content-Type": "application/json",
  },
  body: JSON.stringify(order),
});
};
export const createOrder = (order) => {
    return fetch(apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(order),
    }).then((res) => res.json);
}