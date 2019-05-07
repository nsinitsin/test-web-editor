interface IConfig {
  baseUrl: string,
}

export let ConfigConstants: IConfig = {
  baseUrl: "",
}

export function loadConfigJSON() {
  var xobj = new XMLHttpRequest();
  xobj.overrideMimeType("application/json");
  xobj.open('GET', `${process.env.PUBLIC_URL}/config/config.json`, false);
  xobj.onreadystatechange = function () {
    if (xobj.readyState === 4 && xobj.status === 200) {
      ConfigConstants = JSON.parse(xobj.responseText);
    }
  };
  xobj.send(null);
}