import { useEffect, useState } from 'react';
import styles from './products.module.css';
import Link from 'next/link';
import AddToCartButton from '../Cart/AddToCartButton';

interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
}

const FeaturedProducts: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);
  const apiUrl = 'http://localhost:5000';
  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await fetch(`${apiUrl}/products/featured`);
        if (!response.ok) {
          throw new Error('Failed to fetch products');
        }
        const data = await response.json();
        setProducts(data);
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };

    fetchProducts();
  }, []);

  return (
    <div className={styles.container}>
      <div className={styles.grid}>
        {products.map(product => (
          <div key={product.id} className={styles.productcard}>
            <Link href={`/products/${product.id}`}>
              {product.name}
            </Link>
            <p>Price: ${product.price}</p>
            <AddToCartButton productId={product.id} quantity={1} />
          </div>
        ))}
      </div>
    </div>
  );
};

export default FeaturedProducts;