import * as React from 'react';
import withRoot from "./withRoot";
import './App.scss';

import {Route, Router} from "react-router";
import {createBrowserHistory} from 'history';
import News from "./container/news/News";
import ArchivedNews from "./container/news/ArchivedNews";
import Container from "./container/Container";


class App extends React.Component {
  render() {
    return (
      <div>
        <Router history={createBrowserHistory()}>
          <Container>
            <Route path="/" exact component={News}/>
            <Route path="/archived-news/" component={ArchivedNews}/>
          </Container>
        </Router>
      </div>
    )
  }
}

export default withRoot(App);




