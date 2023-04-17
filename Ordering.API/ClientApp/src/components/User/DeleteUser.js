import React, { Component } from "react";
import { deleteData, getData } from "../services/AccessAPI";

export class DeleteUser extends Component {
    constructor(props) {
        super(props);

        this.state = {
            fullName: '',
            userName: '',
            email: '',
            roles: [],
            loading: true
        }

        this.onCancel = this.onCancel.bind(this);
        this.onConfirmation = this.onConfirmation.bind(this);
    }

    componentDidMount() {
        const { id } = this.props.match.params;

        getData('api/User/GetUserDetails/' + id).then(
            (result) => {
                console.log("Role for edit: ");
                console.log(result);
                if (result) {
                    this.setState({
                        fullName: result.fullName,
                        userName: result.userName,
                        email: result.email,
                        roles: result.roles,
                        loading: false
                    });
                }
            }
        );
    }

    onCancel() {
        const { history } = this.props;
        history.push('/admin/users');
    }

    onConfirmation(e) {
        e.preventDefault();

        const { id } = this.props.match.params;
        const { history } = this.props;

        deleteData('api/User/Delete/' + id).then((result) => {
            let responseJson = result;
            if (responseJson) {
                history.push('/admin/users');
            }
        }
        );

    }


    render() {
        return (
            <div>
                <h2>::Delete user::</h2>
                <h3>Are you sure you want to delete this?</h3>
                <div>
                    <h4>User Information</h4>
                    <dl className="row">
                        <dt className="col-sm-2">
                            Full Name:
                        </dt>
                        <dd className="col-sm-10">
                            {this.state.fullName}
                        </dd>
                    </dl>

                    <dl className="row">
                        <dt className="col-sm-2">
                            User Name:
                        </dt>
                        <dd className="col-sm-10">
                            {this.state.userName}
                        </dd>
                    </dl>

                    <dl className="row">
                        <dt className="col-sm-2">
                            Email:
                        </dt>
                        <dd className="col-sm-10">
                            {this.state.email}
                        </dd>
                    </dl>

                    <form onSubmit={this.onConfirmation}>
                        <input type="hidden" asp-for="Id" />
                        <button type="submit" className="btn btn-danger">Delete</button> |
                        <button onClick={this.onCancel} className="btn btn-primary">Back to List</button>
                    </form>
                </div>
            </div>
        )
    }
}