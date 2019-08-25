import React, { Component } from 'react';
import Axios from 'axios';

export default class AddToCart extends Component {

    constructor(props) {
        super(props);

        this.onSubmit = this.onSubmit.bind(this);
    }

    async onSubmit(e) {
        e.preventDefault();
        await Axios.post('/api/cart', { productId: this.props.productId });
    }

    render() {
        return <form onSubmit={this.onSubmit}>
            <div className="form-row">
                <div className="form-group">
                    <button className="btn btn-primary form-control" type="submit">
                        Add to Cart
                    </button>
                </div>
            </div>
               </form>
    }
}