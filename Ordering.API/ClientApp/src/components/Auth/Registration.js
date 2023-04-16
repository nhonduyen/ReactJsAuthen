import { Component } from "react";
import {
    Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';

export default class Registration extends Component{
    constructor(){
        super();
        this.state = {

        };
    }

    render(){
        return(
            <h3>Create a new account</h3>
        );
    }
}