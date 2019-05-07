import {ConfigConstants} from './config';
import {fetchGet, fetchPost, fetchPut, fetchDelete, fetchPatch, externalGet, externalPost} from './fetchUtils';
//import { reduxStore } from '../index';
import {createBrowserHistory} from 'history';
import {storeGet} from './storeUtils';

function getFileUploadHeaders() {
  const headers = new Headers();

  return headers;
}

function getHeaders() {
  const headers = new Headers();

  headers.append('Content-Type', 'application/json');
  headers.append('Accept', 'application/json');

  return headers;
}

function getExternalHeaders() {
  const createHeaders = {
    'Content-Type': 'application/json',
    'Accept': 'application/json',
  };

  const headers = new Headers(createHeaders);

  return headers;
}

export function apiGetExternal(req: string) {
  return externalGet(req, getExternalHeaders());
}

export function apiPostExternal(req: string, data: object) {
  return externalPost(req, getExternalHeaders(), JSON.stringify(data));
}

export function apiGet(request: string) {
  return new Promise((fulfill, reject) => {
    const url = ConfigConstants.baseUrl + request;

    return fetchGet(url, getHeaders())
      .then((response) => {
        fulfill(response);
      }).catch((error) => {
        if (error.status === 403) {
          createBrowserHistory().push('/forbidden');
        } else {
          reject(error);
        }

      });
  });
}

export function apiPost(request: string, data: object, isMock?: boolean) {
  return new Promise((fulfill, reject) => {
    const url = ConfigConstants.baseUrl + request;

    return fetchPost(url, getHeaders(), JSON.stringify(data))
      .then((response) => {
        fulfill(response);
      }).catch((error) => {
        if (error.status === 403) {
          createBrowserHistory().push('/forbidden');
        } else {
          reject(error);
        }

      });
  });
}

export function apiPatch(request: string, data: object) {
  return new Promise((fulfill, reject) => {
    const url = ConfigConstants.baseUrl + request;

    return fetchPatch(url, getHeaders(), JSON.stringify(data))
      .then((response) => {
        fulfill(response);
      }).catch((error) => {
        if (error.status === 403) {
          createBrowserHistory().push('/forbidden');
        } else {
          reject(error);
        }
      });
  });
}

export function apiPostFiles(request: string, data: object) {
  return new Promise((fulfill, reject) => {
    const url = ConfigConstants.baseUrl + request;

    return fetchPost(url, getFileUploadHeaders(), data)
      .then((response) => {
        fulfill(response);
      }).catch((error) => {
        if (error.status === 403) {
          createBrowserHistory().push('/forbidden');
        } else {
          reject(error);
        }
      });
  });
}

export function apiPutFiles(request: string, data: object) {
  return new Promise((fulfill, reject) => {
    const url = ConfigConstants.baseUrl + request;

    return fetchPut(url, getFileUploadHeaders(), data)
      .then((response) => {
        fulfill(response);
      }).catch((error) => {
        if (error.status === 403) {
          createBrowserHistory().push('/forbidden');
        } else {
          reject(error);
        }
      });
  });
}

export function apiPut(request: string, data?: object) {
  return new Promise((fulfill, reject) => {
    const url = ConfigConstants.baseUrl + request;

    return fetchPut(url, getHeaders(), JSON.stringify(data ? data : ""))
      .then((response) => {
        fulfill(response);
      }).catch((error) => {
        if (error.status === 403) {
          createBrowserHistory().push('/forbidden');
        } else {
          reject(error);
        }
      });
  });
}

export function apiDelete(request: string) {
  return new Promise((fulfill, reject) => {
    const url = ConfigConstants.baseUrl + request;

    return fetchDelete(url, getHeaders())
      .then((response) => {
        fulfill(response);
      }).catch((error) => {
        reject(error);
      });
  });
}

export function parseJSON(response: any) {
  return response.json();
}