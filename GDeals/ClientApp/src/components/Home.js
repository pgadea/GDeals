import React, { Component } from 'react';
import ProductList from './Products/ProductList';

export class Home extends Component {
    static displayName = Home.name;

  render () {
    return (
        <ProductList />
    );
  }
}
