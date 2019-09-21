import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import ProductDetails from './components/Products/ProductDetails';
import Cart from './components/Cart/Cart';
import CheckoutPage from './components/Checkout/CheckoutPage';
import { Button } from 'reactstrap';
import Callback from './components/Callback';

export default class App extends Component {
    static displayName = App.name;

    render() {

        return (
            <Route path='/*' component={({ ...others }) =>
                <Layout auth={this.props.auth}>
                    <Route exact path='/' component={Home} />
                    <Route path='/counter' component={Counter} />
                    <Route path='/fetch-data' component={FetchData} />
                    <Route path='/product/:id' component={ProductDetails} />
                    <Route path='/cart' component={Cart} />
                    <Route path='/callback' component={({ ...others }) =>
                        <Callback auth={this.props.auth} {...others} />
                    } />
                    <Route path='/checkout' component={({ ...others }) =>
                        <SecureCheckout auth={this.props.auth}>
                            <CheckoutPage auth={this.props.auth} {...others} />
                        </SecureCheckout>
                    } />
                </Layout>
            } />
        );
    }
}

export class SecureCheckout extends Component {

    state = { authenticated: false }

    constructor(props) {
        super(props);
        this.handleLogin = this.handleLogin.bind(this);
    }

    async componentDidMount() {
        const isLoggedIn = await this.props.auth.isAuthenticated();
        this.setState({ authenticated: isLoggedIn });
    }

    async handleLogin() {
        await this.props.auth.loginWithRedirect();
    }

    render() {
        return (
            this.state.authenticated
                ? this.props.children
                : <div>
                    <p>Please register or log in to complete your order</p>
                    <Button color="primary" onClick={this.handleLogin}>Log in or Sign up</Button>
                </div>
        )
    }
}
