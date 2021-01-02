import * as types from '../constant'
export function getListItem(payload){
    return({
        type: types.GET_ITEM_REQUEST,
        payload
    })
}
export function addItemAction(payload){
    return({
        type: types.ADD_ITEM_REQUEST,
        payload
    })
}
export function deleteItemAction(payload){
    return({
        type:types.DELETE_ITEM_REQUEST,
        payload
    })
}
export function updateItemAction(payload){
    return({
        type:types.UPDATE_ITEM_REQUEST,
        payload
    })
}
export function searchItemAction(payload){
    return({
        type:types.SEARCH_ITEM_REQUEST,
        payload
    })
}