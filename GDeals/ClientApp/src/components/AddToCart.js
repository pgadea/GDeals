import React, { Component } from 'react';
import Axios from 'axios';

export default class AddToCart extends Component {

    state = {}

    constructor(props) {
        super(props);

        this.onSubmit = this.onSubmit.bind(this);
    }

    async onSubmit(e) {
        e.preventDefault();
        let request = { productId: this.props.productId };
        let sessionId = localStorage.sessionId;
        if (sessionId) {
            request.sessionId = sessionId;
        }
        let result = await Axios.post('/api/cart', { productId: this.props.productId, sessionId: sessionId });
        localStorage.sessionId = result.data.sessionId;
        this.setState({ itemJustAdded: true });
        setTimeout(() => this.setState({ itemJustAdded: false }), 6000);
    }

    render() {
        return <form onSubmit={this.onSubmit}>
                    <div className="form-row">
                        <div className="form-group">
                        <button disabled={this.state.itemJustAdded} className="btn btn-primary form-control" type="submit">
                                Add to Cart
                            </button>
                        </div>
                    </div>
                    {this.state.itemJustAdded && <span className="alert alert-primary">Item added to cart</span>}
               </form>
    }
}