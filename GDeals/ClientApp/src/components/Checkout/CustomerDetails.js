import React from 'react';
import { Form, FormGroup, Label, Col, Input } from 'reactstrap';

export class CustomerDetails extends React.Component {

    state = { firstName: '', lastName: '', email: '' };

    handleChange(newState) {
        this.setState(newState, () => {
            if (this.props.onChanged)
                this.props.onChanged(this.state);
        });
    }

    render() {
        return (
            <Form>
                <FormGroup row>
                    <Label for="firstName" sm={2}>First Name</Label>
                    <Col sm={10}>
                        <Input
                            type="text"
                            id="firstName"
                            value={this.state.firstName}
                            onChange={event => this.handleChange({ firstName: event.target.value })}
                            placeholder="First Name" />
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label for="lastName" sm={2}>Last Name</Label>
                    <Col sm={10}>
                        <Input
                            type="text"
                            id="lastName"
                            value={this.state.lastName}
                            onChange={event => this.handleChange({ lastName: event.target.value })}
                            placeholder="Last Name" />
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label for="email" sm={2}>Email</Label>
                    <Col sm={10}>
                        <Input
                            type="text"
                            id="email"
                            value={this.state.email}
                            onChange={event => this.handleChange({ email: event.target.value })}
                            placeholder="Email" />
                    </Col>
                </FormGroup>
            </Form>
        )
    }

}
