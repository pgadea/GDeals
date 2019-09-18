import React from "react";
import { Col, Form, FormGroup, Input, Label } from "reactstrap";

export class Address extends React.Component {

    state = { line1: '', line2: '', city: '', county: '', postcode: '' };

    handleChange(newState) {
        this.setState(newState, () => this.props.onChanged(this.state));
    }

    render() {
        return (
            <Form>
                <FormGroup row>
                    <Label for="line1" sm={2}>Line 1</Label>
                    <Col sm={10}>
                        <Input type="text" id="line1" value={this.state.line1}
                            onChange={event => this.handleChange({ line1: event.target.value })}
                            placeholder="Line 1" />
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label for="line2" sm={2}>Line 2</Label>
                    <Col sm={10}>
                        <Input type="text" id="line2" value={this.state.line2}
                            onChange={event => this.handleChange({ line2: event.target.value })}
                            placeholder="Line 2" />
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label for="city" sm={2}>Town/City</Label>
                    <Col sm={10}>
                        <Input type="text" id="city" value={this.state.city}
                            onChange={event => this.handleChange({ city: event.target.value })}
                            placeholder="City" />
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label for="county" sm={2}>County</Label>
                    <Col sm={10}>
                        <Input type="text" id="county" value={this.state.county}
                            onChange={event => this.handleChange({ county: event.target.value })}
                            placeholder="County" />
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label for="postcode" sm={2}>Postcode</Label>
                    <Col sm={10}>
                        <Input type="text" id="postcode" value={this.state.postcode}
                            onChange={event => this.handleChange({ postcode: event.target.value })}
                            placeholder="Postcode" />
                    </Col>
                </FormGroup>
            </Form>
        )
    }

}
