import { useEffect, useState } from "react";
import config from "../config";
import { House } from "../types/House";

const useHouseHooks = (): House[] => {
    const [houses, setHouses] = useState<House[]>([]);

    useEffect(() => {
        const fetchedHouses = async () => {
            const rsp = await fetch(`${config.baseApiUrl}/houses`);
            const houses= await rsp.json();
            setHouses(houses);
        };
        fetchedHouses();
    }, []);

    return houses;
};

export default useHouseHooks;