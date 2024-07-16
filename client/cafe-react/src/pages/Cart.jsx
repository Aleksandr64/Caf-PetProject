import FormOrder from "../components/Cart/FormOrder";
import ListDishOrder from "../components/Cart/ListDishOrder";
import styles from "./Cart.module.scss";

export default function Cart() {
  return (
    <>
      <div className={styles.container}>
        <h1>Ваше замовлення</h1>
        <div className={styles.gridContainer}>
          <div className={styles.item1}>
            <FormOrder />
          </div>
          <div className={styles.item2}>
            <ListDishOrder />
          </div>
        </div>
      </div>
    </>
  );
}
