import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import createAuth0Client from '@auth0/auth0-spa-js';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const auth0 = async () => await createAuth0Client({
    domain: 'dev-6s-jnjk7.auth0.com',
    client_id: 'bliWJVbbizwqHdK9fMzcs65Uf5auoOUv',
    redirect_uri: 'https://localhost:44388/callback',
    audience: 'https://api.gdeals.io'
});

auth0().then(auth => {
    ReactDOM.render(
        <BrowserRouter basename={baseUrl}>
            <App auth={auth} />
        </BrowserRouter>,
        rootElement);
})

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <App />
  </BrowserRouter>,
  rootElement);

registerServiceWorker();
