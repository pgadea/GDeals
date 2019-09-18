import React from 'react';
import { CardElement, injectStripe } from 'react-stripe-elements';
import { Button } from 'reactstrap';
import './CheckoutForm.css';

class CheckoutForm extends React.Component {

    submit = async (ev) => {
        let { token } = await this.props.stripe.createToken({ name: `${this.props.customer.firstName}  ${this.props.customer.lastName}` });
        if (this.props.onPaymentMethodChanged)
            this.props.onPaymentMethodChanged(token);
    }

    render() {
        return (
            <div className='checkout'>
                <h4>Payment Details</h4>
                <CardElement style={{ base: { fontSize: '18px' } }} />
                <Button color='primary' onClick={this.submit}>Place Order</Button>
            </div>
            )
    }
}

export default injectStripe(CheckoutForm);