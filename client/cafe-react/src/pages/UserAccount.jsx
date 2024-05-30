import React, { useState, useRef } from "react";
import { useDispatch, useSelector } from "react-redux";
import { logOut, selectCurrentAccessToken, selectCurrentUser } from "../redux/slices/authSlice";
import { Link } from "react-router-dom";
import { useLogoutMutation } from "../redux/API/authApiSlice";
import { BsCartCheck } from "react-icons/bs";
import { FaUserEdit } from "react-icons/fa";
import { IoIosArrowDown } from "react-icons/io";
import '../Style/style.scss';
import styles from "./UserAccount.module.scss";
import PersonalData from "../components/User/PersonalData";

const UserAccount = () => {
  const user = useSelector(selectCurrentUser);
  const accessToken = useSelector(selectCurrentAccessToken);
  const [logout] = useLogoutMutation();
  const dispatch = useDispatch();

  const [activeAccordion, setActiveAccordion] = useState(null);
  const [contentHeight, setContentHeight] = useState(0);
  const userDataRef = useRef(null);
  const orderRef = useRef(null);


  const toggleAccordion = (index, contentRef) => {
    setActiveAccordion(activeAccordion === index ? null : index);
    if (contentRef.current) {
      console.log(contentRef.current.scrollHeight);
      setContentHeight(contentRef.current.scrollHeight);
    }
  };

  const useLogout = async () => {
    console.log("logout");
    try {
      await logout(accessToken);
      dispatch(logOut());
    } catch (err) {
      if (!err.response) {
        console.log("No Server Response");
      } else if (err.response.status === 400) {
        console.log("Missing Username or Password");
      } else if (err.response?.status === 401) {
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
        керувати вашими відправними та платіжними адресами та редагувати свій пароль та дані облікового запису.
      </p>
      <div className={`${styles.account} ${activeAccordion === 0 ? styles.active : ''}`}>
        <div className={styles.title} onClick={() => toggleAccordion(0, userDataRef)}>
          <FaUserEdit />
          <h3>Personal data</h3>
          <IoIosArrowDown className={styles.arrowButton}/>
        </div>
        <div
          ref={userDataRef}
          className={styles.content}
          style={{ maxHeight: activeAccordion === 0 ? `${contentHeight}px` : '0' }}
        >
          <PersonalData/>
        </div>
      </div>
      <div className={`${styles.account} ${activeAccordion === 1 ? styles.active : ''}`}>
        <div className={styles.title} onClick={() => toggleAccordion(1, orderRef)}>
          <BsCartCheck/>
          <h3>Order History</h3>
          <IoIosArrowDown className={styles.arrowButton}/>
        </div>
        <div
          ref={orderRef}
          className={styles.content}
          style={{maxHeight: activeAccordion === 1 ? `${contentHeight}px` : '0'}}
        >
          <p>ddd</p>
          <p>ddd</p>
          <p>ddd</p>
          <p>ddd</p>
          <p>ddd</p>
        </div>
      </div>
      <div className={styles.boxWrapper}>
        <button className={`colorButton ${styles.buttonWidth}`} type="submit" onClick={useLogout}>Log Out</button>
      </div>
    </div>
  );
};

export default UserAccount;

