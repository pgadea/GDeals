import React, { Component } from 'react';
import ProductCard from '../ProductCard';
import Axios from 'axios';

export default class ProductList extends Component {

    constructor(props) {
        super(props);
        this.state = {
            products: []
        };
    }

    async componentDidMount() {
        let result = await Axios('/api/product');
        this.setState({ 'products': result.data.products });
    }

    render() {
        return (
            <div className="row">
                {this.state.products.map(product =>
                    <div key={product.id} className="col-4">
                        <ProductCard product={product}/>
                    </div>
                )}
            </div>
        );
    }
}