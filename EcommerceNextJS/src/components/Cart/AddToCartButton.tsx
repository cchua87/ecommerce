import axios from 'axios';
import { useState } from 'react';
import styles from './cart.module.css';

const AddToCartButton= ({ productId, quantity }: {productId: number, quantity: number }) => {
  const [loading, setLoading] = useState(false);
  const apiUrl = 'http://localhost:5000';
  const cartId = localStorage?.getItem('cartId');
  
  const addToCart = async () => {
    setLoading(true);
    try {
      const response = await axios.post(`${apiUrl}/cart/${cartId}`, {
        productId: productId,
        quantity: quantity
      });
      console.log('Item added to cart:', response.data);
      localStorage.setItem('cartId', response.data);
    } catch (error) {
      console.error('Error adding item to cart:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <button onClick={addToCart} disabled={loading} className={styles.button}>
      {loading ? 'Adding to Cart...' : 'Add to Cart'}
    </button>
  );
};

export default AddToCartButton;