import React, { useState, useRef, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { logOut, selectCurrentUserName } from "../redux/slices/authSlice";
import { BsCartCheck } from "react-icons/bs";
import { FaUserEdit } from "react-icons/fa";
import { IoIosArrowDown } from "react-icons/io";
import "../Style/style.scss";
import styles from "./UserAccount.module.scss";
import PersonalData from "../components/User/PersonalData";
import api from "../API/apiConfig";
import OrderItem from "../components/User/OrderItem";

const UserAccount = () => {
  const user = useSelector(selectCurrentUserName);
  const dispatch = useDispatch();

  const [activeAccordion, setActiveAccordion] = useState(null);
  const userDataRef = useRef(null);
  const orderRef = useRef(null);

  const updateContentHeight = (contentRef) => {
    if (contentRef.current) {
      return contentRef.current.scrollHeight;
    }
    return 0;
  };

  const toggleAccordion = (index) => {
    setActiveAccordion(activeAccordion === index ? null : index);
  };

  useEffect(() => {
    const handleResize = () => {
      if (activeAccordion === 0) {
        userDataRef.current.style.maxHeight = `${updateContentHeight(userDataRef)}px`;
      } else if (activeAccordion === 1) {
        orderRef.current.style.maxHeight = `${updateContentHeight(orderRef)}px`;
      }
    };

    window.addEventListener("resize", handleResize);
    handleResize(); // Initial call to set height

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, [activeAccordion]);

  const useLogout = async () => {
    console.log("logout");
    try {
      await api.post("Auth/Logout");
      dispatch(logOut());
    } catch (err) {
      if (!err.response) {
        console.log("No Server Response");
      } else if (err.response.status === 400) {
        console.log("Missing Username or Password");
      } else if (err.response?.status === 401) {
        dispatch(logOut());
        console.log("Unauthorized");
      } else {
        console.log("Login Failed");
      }
    }
  };

  return (
    <div className={styles.container}>
      <h1>Вітаємо {user} в особистому акаунті</h1>
      <p className={styles.description}>
        На сторінці облікового запису ви можете переглядати останні замовлення,
        керувати вашими відправними та платіжними адресами та редагувати свій
        пароль та дані облікового запису.
      </p>
      <div
        className={`${styles.account} ${activeAccordion === 0 ? styles.active : ""}`}
      >
        <div
          className={styles.title}
          onClick={() => toggleAccordion(0, userDataRef)}
        >
          <FaUserEdit />
          <h3>Personal data</h3>
          <IoIosArrowDown className={styles.arrowButton} />
        </div>
        <div
          ref={userDataRef}
          className={styles.content}
          style={{
            maxHeight:
              activeAccordion === 0
                ? `${updateContentHeight(userDataRef)}px`
                : "0",
          }}
        >
          <PersonalData />
        </div>
      </div>
      <div
        className={`${styles.account} ${activeAccordion === 1 ? styles.active : ""}`}
      >
        <div
          className={styles.title}
          onClick={() => toggleAccordion(1, orderRef)}
        >
          <BsCartCheck />
          <h3>Order History</h3>
          <IoIosArrowDown className={styles.arrowButton} />
        </div>
        <div
          ref={orderRef}
          className={styles.content}
          style={{
            maxHeight:
              activeAccordion === 1
                ? `${updateContentHeight(orderRef)}px`
                : "0",
          }}
        >
          <OrderItem />
          <OrderItem />
          <OrderItem />
          <OrderItem />
        </div>
      </div>
      <div className={styles.boxWrapper}>
        <button
          className={`colorButton ${styles.buttonWidth}`}
          type="submit"
          onClick={useLogout}
        >
          Log Out
        </button>
      </div>
    </div>
  );
};

export default UserAccount;
