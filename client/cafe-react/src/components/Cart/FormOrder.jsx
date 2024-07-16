import React from "react";
import api from "../../API/apiConfig";
import { useDispatch, useSelector } from "react-redux";
import { setInputValue, resetCart } from "../../redux/slices/cartSlice";
import { useNavigate } from "react-router-dom";
import { refreshAccessToken } from "../../API/authUtils";
import "../../Style/style.scss";
import styles from "./FormOrder.module.scss";

export default function FormOrder() {
  const navigate = useNavigate();
  const cart = useSelector((state) => state.cart);
  const userName = useSelector((state) => state.auth.userName);
  const dispatch = useDispatch();

  const submitOrder = async (event) => {
    event.preventDefault();

    let updatedCart = { ...cart };

    if (userName) {
      try {
        await refreshAccessToken();
        updatedCart = { ...updatedCart, userName: userName };
      } catch (err) {
        console.log("Failed to refresh access token", err);
        updatedCart = { ...updatedCart, userName: null };
      }
    } else {
      updatedCart = { ...updatedCart, userName: null };
    }

    dispatch(
      setInputValue({ propertyName: "userName", value: updatedCart.userName }),
    );

    try {
      await api.post("Order/AddNewOrder", updatedCart);
      dispatch(resetCart());
      navigate("/");
    } catch (err) {
      console.log(err);
      if (!err.response) {
        console.log("No Server Response");
      } else if (err.response.status === 400) {
        console.log("Missing Username or Password");
      } else if (err.response?.status === 401) {
        console.log("Unauthorized");
      } else {
        console.log("Failed");
      }
    }
  };

  const addFormOrder = (propertyName, event) => {
    dispatch(setInputValue({ propertyName, value: event.target.value }));
  };

  return (
    <div className={styles.formOrder}>
      <form onSubmit={submitOrder}>
        <label htmlFor="customerName">Ім'я</label>
        <div className="formField">
          <input
            type="text"
            id="customerName"
            className="input"
            onChange={(event) => addFormOrder("customerName", event)}
            value={cart.customerName}
            required
          />
        </div>
        <label htmlFor="phoneNumber">Номер телефону</label>
        <div className="formField">
          <input
            type="text"
            id="phoneNumber"
            className="input"
            onChange={(event) => addFormOrder("phoneNumber", event)}
            value={cart.phoneNumber}
            required
          />
        </div>
        <label htmlFor="emailAddress">Електронна Адресса</label>
        <div className="formField">
          <input
            type="text"
            id="emailAddress"
            className="input"
            onChange={(event) => addFormOrder("emailAddress", event)}
            value={cart.emailAddress}
            required
          />
        </div>
        <label htmlFor="address">Адреса доставки</label>
        <div className="formField">
          <input
            type="text"
            id="address"
            className="input"
            onChange={(event) => addFormOrder("address", event)}
            value={cart.address}
            required
          />
        </div>
        <p className={styles.totalAmount}>
          Сума замовлення: {cart.totalAmount.toFixed(2)} грн.
        </p>
        <div className={styles.boxWrapper}>
          <button className="colorButton" type="submit">
            Створити замовлення
          </button>
        </div>
      </form>
    </div>
  );
}
