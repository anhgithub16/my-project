import React from 'react'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import * as page from './pages'

const Routes = () => (
    <BrowserRouter>
        <Switch>
            <Route exact path="/" component={page.HomePage}></Route>
            <Route exact path="/items" component={page.ItemPage}></Route>
        </Switch>
    </BrowserRouter>
);
export default Routes;