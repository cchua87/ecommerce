import AddToCartButton from '@/components/Cart/AddToCartButton';
import Layout from '@/components/Layout';
import { GetServerSideProps } from 'next';

const ProductDetailPage: React.FC<{ product: any }> = ({ product }) => {
    return (
        <Layout>
            <div className="container">
                <h1>{product.name}</h1>
                <p>Product Number: {product.productNumber}</p>
                <p>Price: ${product.price}</p>
                <AddToCartButton productId={product.id} quantity={1} />
            </div>
        </Layout>
    );
};

export const getServerSideProps: GetServerSideProps = async ({ params }) => {
    const productId = params?.id;
    const apiUrl = 'http://localhost:5000';
    try {
        const response = await fetch(`${apiUrl}/products/${productId}`);
        if (!response.ok) {
            throw new Error('Failed to fetch product');
        }
        const product = await response.json();
        return {
            props: {
                product,
            },
        };
    } catch (error) {
        console.error('Error fetching product:', error);
        return {
            notFound: true,
        };
    }
};

export default ProductDetailPage;