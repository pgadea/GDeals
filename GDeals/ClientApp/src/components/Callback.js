import React, { Component } from 'react';

export default class Callback extends Component {

    async componentDidMount() {
        await this.props.auth.handleRedirectCallback();
        this.props.history.push('/checkout');
    }

    render() {
        return (
            <div>
                <p>Success</p>
                You're now authenticated
            </div>
        )
    }
}
