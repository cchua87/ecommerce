import { useEffect, useState } from 'react';
import styles from './cart.module.css';
import DeleteCartButton from './DeleteCartItemButton';
import axios from 'axios';

interface Cart {
  cartId: string;
  cartItems: CartItem[];
  subTotal: number;
}

interface CartItem {
  productId: number,
  productName: string,
  quantity: number,
  lineTotal: number
}

const Cart: React.FC = () => {
  const [cart, setCart] = useState<Cart>();
  const apiUrl = 'http://localhost:5000';
  const cartId = localStorage?.getItem('cartId');

  useEffect(() => {
    const fetchCart = async () => {
      try {
        const response = await fetch(`${apiUrl}/cart/${cartId}`);
        if (!response.ok) {
          throw new Error('Failed to fetch cart');
        }
        const data = await response.json();
        console.log('data', data);
        setCart(data);
      } catch (error) {
        console.error('Error fetching cart:', error);
      }
    };

    fetchCart();
  }, []);

    const handleDeleteCartItem = async(productId: number) => {
      // Filter out the item to be deleted
      const apiUrl = 'http://localhost:5000';
        try {
          const response = await axios.delete(`${apiUrl}/cart/${cartId}/${productId}`);
          console.log(`Product ${productId} is deleted`);
          setCart(response.data);
        } catch (error) {
          console.error('Error deleting cart item:', error);
        } finally {
          //set loading
        }
    };


  if (cart && cart.cartItems?.length > 0) {
    return (
      <div className={styles.container}>
        <div className={styles.grid}>
          <div key={cart?.cartId} className={styles.cartcard}>
            {cart?.cartItems?.map(item => (
              <div key={item.productId} className={styles.productcard}>
                <p>Name: {item.productName}</p>
                <p>Quantity: {item.quantity}</p>
                <p>Price: ${item.lineTotal}</p>
                <DeleteCartButton onClick={() => handleDeleteCartItem(item.productId)} />
              </div>
            ))}
          </div>
          <p>SubTotal: ${cart?.subTotal}</p>
        </div>
      </div>
    );
  }
  else {
    return(
      <div>
        Cart is empty
      </div>
    );
  }
};

export default Cart;