import Dish from "../components/Home/Dish";
import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { setAllDish } from "../redux/slices/dishSlice";
import styles from "./Home.module.scss";
import api from "../API/apiConfig";

export default function Home() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const dishes = useSelector((state) => state.dish.dishes);

  useEffect(() => {
    getAllDish();
  }, []);

  const getAllDish = async () => {
    try {
      console.log("Get All Dish");
      const response = await api.get("Dish/GetAllDish");
      dispatch(setAllDish(response.data));
    } catch (err) {
      console.log(err);
      if (!err.status) {
        console.log("No Server Response");
        navigate("NotFound");
      } else if (err.status === 400) {
        console.log("Missing Username or Password");
        navigate("NotFound");
      } else if (err.status === 401) {
        console.log("Unauthorized");
        navigate("/login");
      } else {
        console.log("Login Failed");
        navigate("NotFound");
      }
    }
  };

  return (
    <div className={styles.gridContainer}>
      {dishes?.map((record) => (
        <Dish
          key={record.id}
          id={record.id}
          title={record.title}
          description={record.description}
          price={record.price}
          imageUrl={record.imageUrl}
        />
      ))}
    </div>
  );
}
