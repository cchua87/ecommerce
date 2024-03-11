import ProductList from '../components/Product/ProductList';
import Layout from '../components/Layout';
import Cart from '@/components/Cart/Cart';

const CartPage: React.FC = () => {
    return (
        <Layout>
            <div className="container">
                <Cart />
            </div>
        </Layout>
    );
};

export default CartPage;
