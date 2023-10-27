const apiUrl = "/api/route";

export const getRoutes = () => {
    return fetch(apiUrl).then((res) => res.json());
};

export const getRouteById = (id) => {
    return fetch(`${apiUrl}/${id}`).then((res) => res.json());
};

export const createRoute = (route) => {
    return fetch(apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(route),
    }).then((res) => res.json());
}

export const deleteRoute = (routeId) => {
  return fetch(`${apiUrl}/${routeId}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
    });
};