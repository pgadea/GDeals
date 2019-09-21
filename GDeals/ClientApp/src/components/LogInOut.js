import React, { Component } from 'react';
import { Button } from 'reactstrap';

export default class LogInOut extends Component {   
    
    state = {loggedIn: false};
    
    constructor(props) {
        super(props);
        this.handleLogIn = this.handleLogIn.bind(this);
        this.handleLogOut = this.handleLogOut.bind(this);
    } 

    async componentDidMount() {

        if (!this.props.auth) {
            this.setState({ loggedIn: false });
        }
        else {
            const loggedIn = await this.props.auth.isAuthenticated();
            this.setState({ loggedIn: loggedIn });
        }
    }

    async handleLogOut() {
        await this.props.auth.logout();       
    }

    async handleLogIn() {
        await this.props.auth.loginWithRedirect();       
    }
    
    render() {  
        return (           
            this.state.loggedIn
                ? <Button onClick={this.handleLogOut}>Log out</Button>
                : <Button onClick={this.handleLogIn}>Log in</Button>
        )
    }
}
