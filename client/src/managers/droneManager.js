const apiUrl = "/api/drone";

export const getDrones = () => {
    return fetch(apiUrl).then((res) => res.json());
};
export const getActiveDrones = () => {
    return fetch(`${apiUrl}/active`).then((res) => res.json());
};
export const getDroneDetails = (id) => {
    return fetch(`${apiUrl}/${id}`).then((res) => res.json());
};

export const deleteDrone = (drone) => {
    return fetch(`${apiUrl}`, {
        method: "PUT",
        headers: {
        "Content-Type": "application/json",
    },
    body: JSON.stringify(drone),
  });
};

export const createDrone = (drone) => {
    return fetch(apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(drone),
    }).then((res) => res.json());
}