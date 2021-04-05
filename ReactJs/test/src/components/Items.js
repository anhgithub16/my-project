import React, { Component } from 'react'

class Items extends Component {
    state = {
        inputAdd: "",
        inputSearch: "",
        objectDelete: {
            id: "",
            text: ""
        },
        textField: "",
        totalPage: "",
        page: "1",
        length: "",
    }
    handleAdd = (data) => {
        this.setState({ inputAdd: data, textField: data })

        console.log('du lieu in add', this.state.inputAdd)
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
            textField: this.props.textField
        }
        console.log('du lieu handleDelete ' + data.textField)
        this.props.hamDelete(data)
    }

    //sửa
    handleUpdate = (data_id) => {
        const data = {
            id: data_id,
            name: this.state.inputAdd,
            textField: this.props.textField
        }
        this.props.hamUpdate(data)

    }

    //search
    handleSearch = (length) => {
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
        console.log('Dữ liệu listdata', length)
    }
    handlePaginate = (data) => {
        this.props.hamPaginate(data)
    }
    hamXuLy = (ar) => {
        let arr = [];
        for (let index = 1; index < (ar/3)+1
        ; index++) {
            arr.push(<button onClick={()=>{
                let da = index
                this.props.hamPaginate(da)
            }}>{index}</button>)
        }
        return (arr)

    }
    render() {
        let listData = [];
        let btt = [];
        let pagin = []
        if (this.props.items) {
            listData = this.props.items.map((item, key) => {
                let dt = [
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
                ]
                // let arr = [];
                // for (let index = 1; index < 6; index++) {
                //         arr.push(<button>{index}</button>)

                // }
                let dataMain = [
                    dt
                    //arr
                ]
                return (
                    dataMain
                )
            })
            pagin = this.hamXuLy(listData.length)

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
                            this.handleSearch(listData.length)
                        }}>Search</button>
                        <button onClick={() => {
                            this.handlePaginate()
                        }}>Paginate</button>
                        <button onClick={() => {
                            this.hamXuLy()
                        }}>Xử lý</button>
                    </div>
                    <table className="list-item">
                        <tbody>
                            <tr>
                                <th className="id">ID của dữ liệu</th>
                                <th className="name">Tên của dữ liệu</th>
                            </tr>
                            {listData}
                            <tr>
                                <button onClick={() => {
                                    this.handlePaginate()
                                }}>1</button>
                                <a onClick={() => {
                                    this.handlePaginate(2)
                                }}>2</a>
                                <a onClick={() => {
                                    this.handlePaginate(3)
                                }}>3</a>
                                <a onClick={() => {
                                    this.handlePaginate(4)
                                }}>4</a>

                            </tr>
                        {pagin}
                        </tbody>
                    </table>
                </div>
            </div>
        )
    }

}
export default Items;