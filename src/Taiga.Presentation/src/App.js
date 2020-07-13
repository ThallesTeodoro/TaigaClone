import React from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  useParams
} from "react-router-dom";
import Home from './pages/home';
import Menu from './commun/menu';

const routes = [
  {
    path: "/",
    exact: true,
    main: () => <Home />
  },
  {
    path: "/about",
    main: () => <h2>About</h2>
  },
  {
    path: "/auth",
    main: () => <h2>Auth</h2>
  },
  {
    path: "/dashboard",
    main: () => <h2>Dashboard</h2>
  }
];

function App() {
  let { id } = useParams();

  return (
    <Router>
      <Menu />
      <Switch>
        { routes.map((route, index) => (
          <Route
            key={index}
            path={route.path}
            exact={route.exact}
            children={<route.main />} />
        )) }
      </Switch>
    </Router>
  );
}

export default App;
