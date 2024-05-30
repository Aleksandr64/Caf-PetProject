import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { addDish } from "../../redux/slices/cartSlice";
import styles from "./Dish.module.scss";
import {MdOutlineAddShoppingCart} from "react-icons/md";

export default function Dish(props) {
  const countDishOrder = useSelector((state) =>
    state.cart.orderItems.find((item) => item.dishId === props.id),
  );
  const dispatch = useDispatch();

  return (
    <div className={styles.myCard}>
      <div className={styles.imageContainer}>
        <img className={styles.cardImage} src={props.imageUrl} alt={props.title} />
      </div>
      <div className={styles.contentContainer}>
        <div className={styles.detailsContainer}>
          <p>{props.title}</p>
          <p>{props.price.toFixed(2)} грн.</p>
        </div>
        <div className={styles.cartContainer}>
          <button className="roundButton" onClick={() => dispatch(addDish({dishId: props.id, price: props.price}))}>
            <MdOutlineAddShoppingCart className="iconButton" />
          </button>
          <div className={styles.quantity}>
            <p>
              {countDishOrder ? countDishOrder.quantity : 0}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}
