import 'whatwg-fetch';

export function externalGet(url: string, headers: Headers) {
  return new Promise((fulfill: Function, reject: Function) => {
    return fetch(url, {
      method: 'GET',
      headers: headers
    }).then((res) => {
      if (res.status === 200 || res.status === 201) {
        return parseJSON(res);
      } else {
        return parseJSON(res).then((err: Error) => { throw new Exception(res.status, err); });
      }
    }).then((json) => {
      fulfill(json);
    }).catch((err) => {
      if (err.response === undefined) {
        reject(err);
      } else {
        err.status = err.response.status;
        err.statusText = err.response.statusText;
        err.response.text().then((text: string) => {
          try {
            const json = JSON.parse(text);
            err.message = json.message;
          } catch (ex) {
            err.message = text;
          }
          reject(err);
        });
      }
    });
  });
}

export function externalPost(url: string, headers: Headers, data: any) {
  return new Promise((fulfill: Function, reject: Function) => {
    return fetch(url, {
      method: 'POST',
      headers: headers,
      body: data
    }).then((res) => {
      if (res.status === 200 || res.status === 201) {
        return parseJSON(res);
      } else {
        return parseJSON(res).then((err: Error) => { throw new Exception(res.status, err); });
      }
    }).then((json) => {
      fulfill(json);
    }).catch((err) => {
      if (err.response === undefined) {
        reject(err);
      } else {
        err.status = err.response.status;
        err.statusText = err.response.statusText;
        err.response.text().then((text: string) => {
          try {
            const json = JSON.parse(text);
            err.message = json.message;
          } catch (ex) {
            err.message = text;
          }
          reject(err);
        });
      }
    });
  });
}

export function fetchGet(url: string, headers: Headers) {
  return new Promise(function (fulfill: Function, reject: Function) {

    //console.log("GET Request: " + url);

    return fetch(url, {
      method: 'GET',
      headers: headers
    }).then((response) => {

      // First premise gets the response information
      //console.log("GET Response Status: " + response.status);

      if(response.status < 400) {
        return parseJSON(response);
      } else if(response.status === 401) {
        // This code will force the error
        return parseJSON(response).then((err: Error) => { throw new Exception(response.status, err); });
      } else {
        return parseJSON(response).then((err: Error) => { throw new Exception(response.status, err); });
      }

    }).then((json) => {

      // Second premise gets the body data of the response
      //console.log("GET Response Body: " + json);
      fulfill(json);

    }).catch((error) => {
      // Catch gets the errors
      if (error.response === undefined) {
        reject(error);
      } else {
        error.status = error.response.status;
        error.statusText = error.response.statusText;
        error.response.text().then((text: string) => {
          try {
            const json = JSON.parse(text);
            error.message = json.message;
          } catch (ex) {
            error.message = text;
          }
          reject(error);
        });
      }
    });

  });
}

export function fetchDelete(url: string, headers: Headers) {
  return new Promise(function (fulfill: Function, reject: Function) {

    //console.log("GET Request: " + url);

    return fetch(url, {
      method: 'DELETE',
      headers: headers
    }).then((response) => {

      // First premise gets the response information
      //console.log("GET Response Status: " + response.status);

      if(response.status === 200 || response.status === 201) {
        return parseJSON(response);
      } else if(response.status === 401) {
        // This code will force the error
        return parseJSON(response).then((err: Error) => { throw new Exception(response.status, err); });
      } else {
        return parseJSON(response).then((err: Error) => { throw new Exception(response.status, err); });
      }

    }).then((json) => {

      // Second premise gets the body data of the response
      //console.log("GET Response Body: " + json);
      fulfill(json);

    }).catch((error) => {
      // Catch gets the errors
      if (error.response === undefined) {
        reject(error);
      } else {
        error.status = error.response.status;
        error.statusText = error.response.statusText;
        error.response.text().then((text: any) => {
          try {
            const json = JSON.parse(text);
            error.message = json.message;
          } catch (ex) {
            error.message = text;
          }
          reject(error);
        });
      }
    });

  });
}

export function fetchPost(url: string, headers: any, data: any) {
  return new Promise(function (fulfill: Function, reject: Function) {

    //console.log("POST Request: " + url);

    return fetch(url, {
      method: 'POST',
      headers: headers,
      body: data
    }).then((response) => {

      // First premise gets the response information
      //console.log("POST Response Status: " + response.status);

      if(response.status === 200 || response.status === 201) {
        return parseJSON(response);
      } else if(response.status === 401) {
        // This code will force the error
        return parseJSON(response).then((err: Object) => { throw new Exception(response.status, err); });
      } else {
        return parseJSON(response).then((err: Object) => { throw new Exception(response.status, err); });
      }

    }).then((json) => {

      // Second premise gets the body data of the response
      //console.log("POST Response Body: " + json);
      fulfill(json);

    }).catch((error) => {
      // Catch gets the errors
      if (error.response === undefined) {
        reject(error);
      } else {
        error.status = error.response.status;
        error.statusText = error.response.statusText;
        error.response.text().then((text: string) => {
          try {
            const json = JSON.parse(text);
            error.message = json.message;
          } catch (ex) {
            error.message = text;
          }
          reject(error);
        });
      }
    });

  });
}

export function fetchPatch(url: string, headers: Headers, data: any) {
  return new Promise(function (fulfill: Function, reject: Function) {

    //console.log("POST Request: " + url);

    return fetch(url, {
      method: 'PATCH',
      headers: headers,
      body: data
    }).then((response) => {

      // First premise gets the response information
      //console.log("POST Response Status: " + response.status);

      if(response.status === 200 || response.status === 201) {
        return parseJSON(response);
      } else if(response.status === 401) {
        // This code will force the error
        return parseJSON(response).then((err: Error) => { throw new Exception(response.status, err); });
      } else {
        return parseJSON(response).then((err: Error) => { throw new Exception(response.status, err); });
      }

    }).then((json) => {

      // Second premise gets the body data of the response
      //console.log("POST Response Body: " + json);
      fulfill(json);

    }).catch((error) => {
      // Catch gets the errors
      if (error.response === undefined) {
        reject(error);
      } else {
        error.status = error.response.status;
        error.statusText = error.response.statusText;
        error.response.text().then((text: string) => {
          try {
            const json = JSON.parse(text);
            error.message = json.message;
          } catch (ex) {
            error.message = text;
          }
          reject(error);
        });
      }
    });

  });
}

export function fetchPut(url: string, headers: Headers, data: any) {
  return new Promise(function (fulfill: Function, reject: Function) {

    //console.log("PUT Request: " + url);

    return fetch(url, {
      method: 'PUT',
      headers: headers,
      body: data
    }).then((response) => {

      // First premise gets the response information
      //console.log("PUT Response Status: " + response.status);

      if(response.status === 200 || response.status === 201) {
        return parseJSON(response);
      } else if(response.status === 401) {
        // This code will force the error
        return parseJSON(response).then((err: Error) => { throw new Exception(response.status, err); });
      } else {
        return parseJSON(response).then((err: Error) => { throw new Exception(response.status, err); });
      }

    }).then((json) => {

      // Second premise gets the body data of the response
      //console.log("PUT Response Body: " + json);
      fulfill(json);

    }).catch((error) => {
      // Catch gets the errors
      if (error.response === undefined) {
        reject(error);
      } else {
        error.status = error.response.status;
        error.statusText = error.response.statusText;
        error.response.text().then((text: string) => {
          try {
            const json = JSON.parse(text);
            error.message = json.message;
          } catch (ex) {
            error.message = text;
          }
          reject(error);
        });
      }
    });

  });
}


export class Exception {
  status: any;
  message: any;
  type: string;

  constructor(status: any, error: any) {
    this.status = status;
    this.message = error.errorCode;
    this.type = "Exception";
  }
}

function parseJSON(response: any) {
  return response.text().then((text: string) => {
    try {
      const obj = JSON.parse(text);
      return obj;
    }
    catch(err) {
      return text;
    }
  })
}
