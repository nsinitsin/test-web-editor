import { SESSION_STORAGE, LOCAL_STORAGE, COOKIE_STORAGE } from './constants';
let store = require('store');
const Cookies = require("js-cookie");


const alternativeStorages: any = {
  [LOCAL_STORAGE]: [require('store/storages/localStorage')],
  [SESSION_STORAGE]: [require('store/storages/sessionStorage')],
  [COOKIE_STORAGE]: Cookies
}

export function storeSet(key: string, data: any) {
  if (store !== Cookies) {
    return store.set(key, data);
  }

  // COOKIES FALLBACK
  if (typeof data === "object") {
    data = `${JSON.stringify(data)}`;
  } else {
    data = `${data}`
  }

  store.set(key, data);
}

export function storeGet(key: string) {
  if (store !== Cookies) {
    return store.get(key);
  }

  // COOKIES FALLBACK
  let data = store.get(key);
  try {
    const parsedObj = JSON.parse(data);
    return parsedObj;
  }
  catch (err) {
    return data;
  }
}

export function storeRemove(key: string) {
  if(store !== Cookies) {
    return store.remove(key);
  }

  // COOKIES FALLBACK
  store.remove(key);
}


export function setStoreType(STORAGE_TYPE: string) {
  if (alternativeStorages[STORAGE_TYPE]) {
    if (STORAGE_TYPE !== COOKIE_STORAGE) {
      store = require('store').createStore(alternativeStorages[STORAGE_TYPE]);
    } else {
      store = alternativeStorages[STORAGE_TYPE];
    }
  }
}

setStoreType(COOKIE_STORAGE);


/**
 * custom utility to set a cookie, used because js-cookie library wierdly append 'www.' as a prefix in the domain that prevent the cookies to work for subdomains
 * @param name
 * @param value
 * @param days
 */
function setCookie(name: string, value: string, days: number) {
  var expires = "";
  if (days) {
    var date = new Date();
    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    expires = "; expires=" + date.toUTCString();
  }
  document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

Cookies.set = setCookie;