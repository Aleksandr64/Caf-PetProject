import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { addDish, deleteDish } from "../../redux/slices/cartSlice";
import styles from "./Dish.module.scss";
import { MdOutlineAddShoppingCart } from "react-icons/md";

export default function Dish(props) {
  const countDishOrder = useSelector((state) =>
    state.cart.orderItems.find((item) => item.dishId === props.id),
  );
  const dispatch = useDispatch();

  return (
    <div className={styles.myCard}>
      <div className={styles.imageContainer}>
        <img
          className={styles.cardImage}
          src={props.imageUrl}
          alt={props.title}
        />
      </div>
      <div className={styles.contentContainer}>
        <div className={styles.detailsContainer}>
          <p>{props.title}</p>
          <p>{props.price.toFixed(2)} грн.</p>
        </div>
        <div className={styles.cartContainer}>
          {countDishOrder ? (
            <div className="counter-button">
              <button
                className="decrement"
                onClick={() =>
                  dispatch(deleteDish({ dishId: props.id, price: props.price }))
                }
              >
                -
              </button>
              <div className="count">{countDishOrder.quantity}</div>
              <button
                className="increment"
                onClick={() =>
                  dispatch(addDish({ dishId: props.id, price: props.price }))
                }
              >
                +
              </button>
            </div>
          ) : (
            <button
              className="roundButton"
              onClick={() =>
                dispatch(addDish({ dishId: props.id, price: props.price }))
              }
            >
              <MdOutlineAddShoppingCart className="iconButton" />
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
