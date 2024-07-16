import React from "react";
import { Link } from "react-router-dom";
import { MdAccountCircle, MdOutlineAddShoppingCart } from "react-icons/md";
import "../../Style/style.scss";
import styles from "./Header.module.scss";

const Header = () => {
  return (
    <div className={`${styles.header}`}>
      <Link to="/" className={styles.logo}>
        FastFood
      </Link>
      <div className={styles.headerRight}>
        <Link to="/accountPage">
          <button className="roundButton">
            <MdAccountCircle className="iconButton" />
          </button>
        </Link>
        <Link to="/order">
          <button className="roundButton">
            <MdOutlineAddShoppingCart className="iconButton" />
          </button>
        </Link>
      </div>
    </div>
  );
};

export default Header;
