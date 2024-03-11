import ProductList from '../components/Product/ProductList';
import Layout from '../components/Layout';

const Products: React.FC = () => {
    return (
        <Layout>
            <div className="container">
                <h1>Products</h1>
                <ProductList />
            </div>
        </Layout>
    );
};

export default Products;
