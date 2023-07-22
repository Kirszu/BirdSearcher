import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Observations } from './components/Observations';
import { Map } from './components/Map';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/observations' component={Observations} />
        <Route path='/map' component={Map} />
      </Layout>
    );
  }
}
