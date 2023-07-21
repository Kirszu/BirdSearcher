import React, { Component } from 'react';

export class Observations extends Component {
  static displayName = Observations.name;

  constructor(props) {
    super(props);
      this.state = { observations: [], loading: true };
  }

  componentDidMount() {
    this.populateObservationsData();
  }

  static renderObservationsTable(observations) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Bird name</th>
            <th>Date</th>
            <th>Latitude</th>
            <th>Longitude</th>
          </tr>
        </thead>
        <tbody>
          {observations.map(observation =>
            <tr key={observation.obsDt}>
              <td>{observation.comName}</td>
              <td>{observation.obsDt}</td>
              <td>{observation.lat}</td>
              <td>{observation.lng}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : Observations.renderObservationsTable(this.state.observations);

    return (
      <div>
        <h1 id="tabelLabel">Recent bird observations</h1>
        <p>This component demonstrates fetching data eBird API 2.0.</p>
        {contents}
      </div>
    );
  }

  async populateObservationsData() {
    const response = await fetch('observation/PL/recent');
    const data = await response.json();
      this.setState({ observations: data, loading: false });
  }
}
