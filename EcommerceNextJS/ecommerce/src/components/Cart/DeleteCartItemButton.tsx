import React, { useState } from 'react';
import styles from './cart.module.css'; 
import axios from 'axios';

interface Props {
    onClick: () => void;
  }

const DeleteCartItemButton: React.FC<Props> = ({ onClick }) => {
    return (
        <button onClick={onClick} className={styles.button}>
          Delete 
      </button>
  );
};

export default DeleteCartItemButton;