import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import "../../Style/style.scss";
import styles from "./DishOrder.module.scss";
import { FaTrashRestore } from "react-icons/fa";
import { addDish, deleteDish } from "../../redux/slices/cartSlice";

export default function DishOrder({ dishId, quantity }) {
  const dispatch = useDispatch();
  const dish = useSelector((state) =>
    state.dish.dishes.find((item) => item.id === dishId),
  );

  return (
    <div className={styles.myCard}>
      <div className={styles.imageContainer}>
        <img
          className={styles.dishImage}
          src={dish.imageUrl}
          alt={dish.title}
        />
      </div>
      <div className={styles.contentContainer}>
        <div className={styles.nameContainer}>
          <p>{dish.title}</p>
        </div>
        <div className={styles.buttonContainer}>
          <div className={`counter-button ${styles.counterButton}`}>
            <button
              className="decrement"
              onClick={() =>
                dispatch(deleteDish({ dishId, price: dish.price }))
              }
            >
              -
            </button>
            <div className={`count ${styles.count}`}>{quantity}</div>
            <button
              className="increment"
              onClick={() => dispatch(addDish({ dishId, price: dish.price }))}
            >
              +
            </button>
          </div>
        </div>
        <div className={styles.priceContainer}>
          <p>{dish.price * quantity} грн.</p>
        </div>
      </div>
    </div>
  );
}
