import React from 'react';
import { Link } from 'react-router-dom';

export default function ProductCard(props) {
        return (
            <div className="card product-card">
                <div className="card-body">
                    <h5 className="card-title">{props.product.name}</h5>
                    <p className="card-text">{props.product.description}</p>
                    <Link to={`/product/${props.product.id}`}>View</Link>
                </div>
            </div>
          );
}