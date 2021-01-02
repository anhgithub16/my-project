import { put, take, takeEvery } from 'redux-saga/effects'
import getItems from '../fetchAPI/getItems'
import addItemAPI from '../fetchAPI/addItemAPI'
import * as types from '../constant'
import deleteItemAPI from '../fetchAPI/deleteItemAPI'
import updateItemAPI from '../fetchAPI/updateItemAPI'
import searchItemAPI from '../fetchAPI/searchItemAPI'
function* getListItem() {
    try {
        const res = yield getItems()
        yield put({
            type: types.GET_ITEM_SUCCESS,
            payload: res
        })
    } catch (error) {
        yield put({
            type: types.GET_ITEM_FAILURE,
            payload: {
                errorMessage: error.message
            }
        })
    }
}

function* addItemSaga(action) {
    console.log(action.payload)
    try {
        const dataToApi = {
            name: action.payload
        }
        yield addItemAPI(dataToApi)
        yield put({
            type: types.ADD_ITEM_SUCCESS
        })
        yield put({
            type: types.GET_ITEM_REQUEST
        })
    } catch (error) {
        yield put({
            type: types.ADD_ITEM_FAILURE,
            payload: {
                errorMessage: error.message
            }
        })
    }
}

function* deleteItemSaga(action) {
    try {
        const dataToApi = {
            id: action.payload.id,
            txt:action.payload.text
        }
        yield deleteItemAPI(dataToApi)
        yield put({
            type: types.DELETE_ITEM_SUCCESS,
        })
        yield put({
            type: types.SEARCH_ITEM_REQUEST,
            payload:action.payload.text
        })
    } catch (error) {
        yield put({
            type: types.DELETE_ITEM_FAILURE,
            payload: {
                errorMessage: error.message
            }
        })
    }
}

function* updateItemSaga(action) {
    console.log(action.payload.name)
    try {
        const dataToApi = {
            id: action.payload.id,
            name: action.payload.name
        }
        yield updateItemAPI(dataToApi)
        yield put({
            type: types.UPDATE_ITEM_SUCCESS
        })
        yield put({
            type: types.GET_ITEM_REQUEST
        })
    } catch (error) {
        yield put({
            type: types.UPDATE_ITEM_FAILURE,
            payload: {
                errorMessage: error.message
            }
        })
    }
}

function* searchListItem(action) {
    try {
        const dataToApi = {
            textSearch: action.payload
        }
        const res = yield searchItemAPI(dataToApi)
        yield put({
            type: types.SEARCH_ITEM_SUCCESS,
            payload: res
        })
    } catch (error) {
        yield put({
            type: types.SEARCH_ITEM_FAILURE,
            payload: {
                errorMessage: error.message
            }
        })
    }
}

export const ItemSaga = [
    takeEvery(types.GET_ITEM_REQUEST,getListItem),
    takeEvery(types.ADD_ITEM_REQUEST,addItemSaga),
    takeEvery(types.DELETE_ITEM_REQUEST,deleteItemSaga),
    takeEvery(types.UPDATE_ITEM_REQUEST,updateItemSaga),
    takeEvery(types.SEARCH_ITEM_REQUEST,searchListItem)

];