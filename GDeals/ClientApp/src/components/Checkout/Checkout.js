import React from 'react';
import { CustomerDetails } from './CustomerDetails';
import { Address } from './Address';
import { Form, FormGroup, Label, Input } from 'reactstrap';
import { StripeProvider, Elements } from 'react-stripe-elements';
import CheckoutForm from './CheckoutForm';
import Axios from 'axios';

export default class Checkout extends React.Component {

    state = { deliverToBillingAddress: true, customer: {}, billingAddress: {}, deliveryAddress: {} };

    handleCustomerDetailsUpdated = (newDetails) => {
        this.setState({ customer: newDetails });
    }

    toggleUseBillingAddress = () => {
        this.setState({ deliverToBillingAddress: !this.state.deliverToBillingAddress });
    }

    handlePaymentMethodChanged = async (token) => {
        if (token) {

            const bearerToken = await this.props.auth.getTokenSilently();

            Axios.post('/api/checkout', {
                ...this.state,
                paymentToken: token.id,
                sessionId: localStorage.sessionId
            }, {
                    headers: {'Authorization': `bearer ${bearerToken}`}
                });
        }
    }

    render() {
        return (
            <div>
                <h4>Your Details</h4>
                <CustomerDetails onChanged={this.handleCustomerDetailsUpdated} />
                <h4>Billing Address</h4>
                <Address onChanged={newAddress => this.setState({ billingAddress: newAddress })} />

                <Form>
                    <FormGroup check>
                        <Label check>
                            <Input type="checkbox" checked={this.state.deliverToBillingAddress}
                                onChange={this.toggleUseBillingAddress} /> Deliver to billing address?
                        </Label>
                    </FormGroup>
                </Form>
                {!this.state.deliverToBillingAddress &&
                    <div>
                        <h4>Delivery Address</h4>
                        <Address onChanged={newAddress => this.setState({ deliveryAddress: newAddress })} />
                    </div>
                }

                <StripeProvider apiKey='pk_test_F80qEuylxBAmmQajKBpHdbXt'>
                    <Elements>
                        <CheckoutForm onPaymentMethodChanged={this.handlePaymentMethodChanged} customer={this.state.customer}/>
                    </Elements>
                </StripeProvider>
            </div>
        )

    }
}