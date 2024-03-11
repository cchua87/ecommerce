import Header from './Header/Header';
import Main from './Main/Main';
import Footer from './Footer/Footer';

export default function RootLayout({children,}: {children: React.ReactNode,}) {
    return (
        <div>
            <Header />
            <Main children={children} />
            <Footer />
        </div>
    );
}