import React, { Component } from 'react';
import Axios from 'axios';
import RemoveItem from './RemoveItem';
import { Row, Col } from 'reactstrap';
import { Link } from 'react-router-dom';

export default class Cart extends Component {

    state = { items: [] }

    constructor(props) {
        super(props);
        this.handleItemRemoved = this.handleItemRemoved.bind(this);
    }

    async componentDidMount() {
        await this.getCart();
    }

    async getCart() {
        var response = await Axios(`/api/cart?sessionId=${localStorage.sessionId}`);
        this.setState(response.data);
    }

    async handleItemRemoved() {
        this.getCart();
    }

    render() {
        return (
            <div>
                <Row className='clearfix' style={{ padding: '.5rem' }}>
                    <Col>
                        <Link to='/checkout' className='btn btn-primary float-right'>Checkout</Link>
                    </Col>
                </Row>
                <Row>
                <table className='table table-striped table-bordered'>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Quantity</th>
                            <th>Price</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.items.map(item =>
                            <tr key={item.productId}>
                                <td>{item.name}</td>
                                <td>{item.quantity}</td>
                                <td>{item.price}</td>
                                <td><RemoveItem productId={item.productId} onItemRemoved={this.handleItemRemoved} /></td>
                            </tr>
                        )}
                    </tbody>
                    </table>
                    </Row>
                </div>
            )
    }
}