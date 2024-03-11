import FeaturedProducts from '../components/Product/FeaturedProducts';
import Layout from '../components/Layout';

const HomePage: React.FC = () => {
  return (
    <Layout>
      <div className="container">
        <h1>Welcome to our e-commerce store!</h1>
        <FeaturedProducts />
      </div>
   </Layout>
  );
};

export default HomePage;
