import DishOrder from "./DishOrder";
import React from "react";
import { useSelector } from "react-redux";
import styles from "./ListDishOrder.module.scss";

export default function ListDishOrder() {
  const orderList = useSelector((state) => state.cart.orderItems);

  return (
    <div>
      {orderList.length === 0 ? (
        <h3 className={styles.content}>Cart Empty</h3>
      ) : (
        <>
          {orderList.map((items) => (
            <DishOrder
              key={items.dishId}
              dishId={items.dishId}
              quantity={items.quantity}
            />
          ))}
        </>
      )}
    </div>
  );
}
