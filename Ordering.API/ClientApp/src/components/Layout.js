import React, { Component } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";
import LoginMenu from './LoginMenu';
import SessionManager from "./Auth/SessionManager";

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return SessionManager.getToken() ? (
      <div>
        <NavMenu />
        <Container>{this.props.children}</Container>
      </div>
    ) : (
      <div>
        <LoginMenu />
        <Container>{this.props.children}</Container>
      </div>
    );
  }
}
