import config from "../config";
import { House } from "../types/House";
import { useQuery } from "@tanstack/react-query";
import axios, { AxiosError } from "axios";

const useFetchHouses = () => {
    return useQuery<House[], AxiosError>({
        queryKey: ["houses"],
        queryFn:() => axios.get(`${config.baseApiUrl}/houses/v1`)
            .then((resp) => resp.data),
    });
};

const useFetchHouse = (id: number) => {
    return useQuery<House, AxiosError>({
        queryKey: ["houses", id],
        queryFn:() => axios.get(`${config.baseApiUrl}/houses/v1/${id}`)
            .then((resp) => resp.data),
    });
};

export default useFetchHouses;
export {useFetchHouse};