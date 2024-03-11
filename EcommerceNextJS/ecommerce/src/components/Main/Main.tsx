import React from 'react';
import styles from './main.module.css'; // Import the CSS module

const MainContainer = ({ children }: {children: React.ReactNode}) => {
  return (
    <div className={styles.main}>
      <div className={styles.container}>
        {children}
      </div>
    </div>
  );
};

export default MainContainer;