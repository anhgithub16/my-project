import React from 'react'
import Items from '../components/Items'
import * as actions from '../actions/ItemPageActions'
import { connect } from 'react-redux'
import { ItemSaga } from '../sagas/ItemSaga'
import Author from '../components/Items'

class ItemPageContainer extends React.Component {
    componentDidMount() {
        this.props.initLoad()
    }
    render() {
        return (
            <div>
                {this.props.isLoading === true
                    ?
                    <img src="https://data.whicdn.com/images/42829097/original.gif" />
                    :
                    <Items name="abc"  ham={(data) => {
                        this.props.addDispatch(data)
                    }} hamDelete={(data) => {
                        this.props.deleteDispatch(data)
                    }} hamUpdate={(data) => {
                        this.props.updateDispatch(data)
                    }} hamSearch={(data) => {
                        this.props.searchDispatch(data)
                    }} {...this.props} />
                }
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        items: state.items.listItem,
        isLoading: state.items.isFetching,
    }
}

const mapDispatchToProps = (dispatch) => {
    return (
        {
            initLoad: () => {
                dispatch(actions.getListItem())
            },
            addDispatch: (dauvao) => {
                dispatch(actions.addItemAction(dauvao))
            },
            deleteDispatch: (data) => {
                dispatch(actions.deleteItemAction(data))
            },
            updateDispatch: (data) => {
                dispatch(actions.updateItemAction(data))
            },
            searchDispatch: (data) => {
                dispatch(actions.searchItemAction(data))
            }
        }
    )
}

export default connect(mapStateToProps, mapDispatchToProps)(ItemPageContainer)