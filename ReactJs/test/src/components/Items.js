import React, { Component } from 'react'

class Items extends Component {
    state = {
        inputAdd: "",
        inputSearch: "",
        objectDelete: {
            id: "",
            text: ""
        }
    }
    handleAdd =  (data) => {
         this.setState({ inputAdd: data })
         console.log('du lieu in add',this.state.inputAdd)
    }

    handleAddButton = () => {
        // console.log('đây là nút: ', this.state.inputAdd)
        // console.log('Prop', this.props.name)
        // const dt = this.state.inputAdd
        // console.log('du lieu dt',dt)
        // this.setState({inputSearch:dt})
        // console.log('search ',this.state.inputSearch)
        // console.log('search ',this.state.inputSearch)

        this.props.ham(this.state.inputAdd)
    }

    handleDelete = (data_id) => {

        const data = {
            id: data_id,
            text: this.state.inputSearch
        }
        console.log('du lieu handleDelete '+this.state.inputSearch)
        this.props.hamDelete(data)
    }

    //sửa
    handleUpdate = (data_id) => {
        const data = {
            id: data_id,
            name: this.state.inputAdd
        }
        this.props.hamUpdate(data)
        
    }

    //search
    handleSearch = () => {
        //this.setState({ inputSearch: data })
        // const text = {
        //     txt: data
        // }
        // const dt = this.state.inputAdd
        // this.setState({
        //     objectDelete: {
        //         ...this.state.objectDelete,
        //         text: dt
        //     }
        // })
        this.setState({
            inputSearch: this.state.inputAdd
        })
        this.props.hamSearch(this.state.inputAdd);
    }

    render() {
        let listData = []
        if (this.props.items) {
            listData = this.props.items.map((item, key) => {
                return (
                    <tr key={key}>
                        <th>{item.id}</th>
                        <th>{item.name}</th>
                        <th><button onClick={() => {
                         
                            this.handleDelete(item.id)
                        }}>Xóa</button></th>
                        <th><button onClick={() => {
                            this.handleUpdate(item.id)
                        }}>Sửa</button></th>
                    </tr>
                )
            })
        }

        return (
            <div className="">
                <div>
                    <div>
                        <input onChange={(e) => {
                            this.handleAdd(e.target.value)
                        }} onKeyPress={(e) => {
                            if (e.key === "Enter") {
                                this.handleAddButton()
                            }
                        }} />
                        <button onClick={() => {
                            this.handleAddButton()
                        }}>Add</button>
                        <button onClick={() => {

                            this.handleDelete()
                        }}>Delete</button>
                        <button onClick={() => {
                           
                            this.handleSearch()
                        }}>Search</button>
                    </div>
                    <table className="list-item">
                        <tbody>
                            <tr>
                                <th className="id">ID của dữ liệu</th>
                                <th className="name">Tên của dữ liệu</th>
                            </tr>
                            {listData}
                        </tbody>
                    </table>
                </div>
            </div>
        )
    }

}
export default Items;