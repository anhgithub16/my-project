import * as types from '../constant'
const DEFAULT_STATE = {
    listItem: [],
    isFetching: false,
    error: false,
    errorMessage: null,
    data: "",
    data1: []
}

export default (state = DEFAULT_STATE, action) => {
    switch (action.type) {
        case types.SEARCH_ITEM_REQUEST:
        case types.UPDATE_ITEM_REQUEST:
        case types.DELETE_ITEM_REQUEST:
        case types.ADD_ITEM_REQUEST:
        case types.GET_ITEM_REQUEST:
            return {
                ...state,
                isFetching: true,
            }
        case types.GET_ITEM_SUCCESS:
            return {
                ...state,
                isFetching: false,
                listItem: action.payload
            }
        case types.UPDATE_ITEM_SUCCESS:
            return {
                ...state,
                isFetching: false,
                error: false,
                errorMessage: null
            }
        case types.DELETE_ITEM_SUCCESS:
            return {
                ...state,
                isFetching: false,
                error: false,
                errorMessage: null,
                data: action.payload
            }
        case types.ADD_ITEM_SUCCESS:
            return {
                ...state,
                isFetching: false,
                error: false,
                errorMessage: null
            }
        case types.SEARCH_ITEM_SUCCESS:
            return {
                ...state,
                isFetching: false,
                listItem: action.payload,
               
            }
        case types.UPDATE_ITEM_FAILURE:
        case types.UPDATE_ITEM_FAILURE:
        case types.DELETE_ITEM_FAILURE:
        case types.ADD_ITEM_FAILURE:
        case types.GET_ITEM_FAILURE:
            return {
                ...state,
                isFetching: false,
                error: true,
                errorMessage: action.payload.errorMessage
            }
        default:
            return state;
    }
}
