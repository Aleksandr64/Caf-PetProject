import styles from "./OrderItem.module.scss";

const OrderItem = (props) => {
  return (
    <div className={styles.myCard}>
      <div className={styles.content}>
        <div className={styles.leftColumn}>
          <p>№1</p>
          <p>2024-07-21</p>
        </div>
        <div className={styles.rightColumn}>
          <p>Сума: 667 грн.</p>
          <p>Доставлено</p>
        </div>
      </div>
      <div className={styles.listDish}>
        <div className={styles.dishItem}>
          <p className="ellipsis-text">x1 Гамбургерddddddddddddddddddddddd</p>
          <p className={styles.price}>: 250грн.</p>
        </div>
        <div className={styles.dishItem}>
          <p className="ellipsis-text">x1 Гамбургерddddddddddddddddddddddd</p>
          <p className={styles.price}>: 250грн.</p>
        </div>
        <div className={styles.dishItem}>
          <p className="ellipsis-text">x1 Гамбургерddddddddddddddddddddddd</p>
          <p className={styles.price}>: 250грн.</p>
        </div>
      </div>
    </div>
  );
};

export default OrderItem;
