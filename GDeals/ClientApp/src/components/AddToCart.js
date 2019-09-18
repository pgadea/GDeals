import React, { Component } from 'react';
import Axios from 'axios';

export default class AddToCart extends Component {

    constructor(props) {
        super(props);

        this.onSubmit = this.onSubmit.bind(this);
        this.nameChanged = this.nameChanged.bind(this);

        this.state = { quantity: 1 };
    }

    async onSubmit(e) {
        e.preventDefault();
        let request = { productId: this.props.productId, quantity: this.state.quantity };
        let sessionId = localStorage.sessionId;
        if (sessionId) {
            request.sessionId = sessionId;
        }
        let result = await Axios.post('/api/cart', request);
        localStorage.sessionId = result.data.sessionId;
    }

    nameChanged(event) {
        let abs = Math.abs(event.target.value);
        let quantity = abs === 0 ? 1 : abs;
        this.setState({ quantity: quantity });
    }

    render() {
        return <form onSubmit={this.onSubmit}>
            <div className="form-row">
                <input type="number" className="form-control col-sm-2 mr-2" value={this.state.quantity} onChange={this.nameChanged} required />
                <button className="btn btn-primary form-control col-sm-3" type="submit">Add to Cart</button>
            </div>
        </form>;
    }

}
