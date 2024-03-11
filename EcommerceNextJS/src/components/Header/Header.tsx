import Link from 'next/link';
import styles from './header.module.css'; 

const Header = () => {
  return (
    <header className={styles.header}>
       <nav>
        <ul>
          <li>
            <Link href="/">
              Home
            </Link>
          </li>
          <li>
            <Link href="/products">
              Products
            </Link>
          </li>
          <li>
            <Link href="/cart">
              Cart
            </Link>
          </li>
        </ul>
      </nav>
    </header>
  );
};

export default Header;