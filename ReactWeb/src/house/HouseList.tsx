import { useState } from "react";
import { House } from "../types/House";
import config from "../config";

const HouseList = () => {
    const [houses, setHouses] = useState<House[]>([]);

    const fetchedHouses = async () => {
        const rsp = await fetch(`${config.baseApiUrl}/houses`);
        const houses= await rsp.json();
        setHouses(houses);
    };
    fetchedHouses();

    return (
        <div>
            <div className="row mb-2">
                <h5 className="themeFontColor text-center">
                    House currently on the market
                </h5>
            </div>
            <table className="table table-hover">
                <thead>
                    <tr>
                        <th>Address</th>
                        <th>Country</th>
                        <th>Asking Price</th>
                    </tr>
                </thead>
                <tbody>
                    {houses.map((h) => (
                        <tr key={h.id}>
                            <td>{h.address}</td>
                            <td>{h.country}</td>
                            <td>{h.price}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default HouseList;