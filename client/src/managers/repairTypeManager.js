const apiUrl = "/api/repairType";

export const getRepairTypes = () => {
    return fetch(apiUrl).then((res) => res.json());
};