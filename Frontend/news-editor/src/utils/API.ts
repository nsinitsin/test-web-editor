import {apiGet, apiPost, apiPut} from "./apiUtils";
import {INewsUpdateOutputModel} from "../models/INewsUpdateOutputModel";

class API {
  static  getArchivedNews(page:number, take: number) {
    return apiGet(`/api/News/archived?page=${page}&take=${take}`);
  }

  static getActiveNews(){
    return apiGet(`/api/News/live`);
  }

  static updateLiveNews(content: INewsUpdateOutputModel, id:number){
    return apiPut(`/api/News/live/${id}`, content)
  }

  static getDraftNews(){
    return apiGet(`/api/News/draft`);
  }

  static updateDraftNews(content: INewsUpdateOutputModel, id:number){
    return apiPut(`/api/News/draft/${id}`, content)
  }

  static saveNewNews(content: INewsUpdateOutputModel){
    return apiPost(`/api/News/`, content)
  }

  static goToLiveDraftNews(id: number) {
    return apiPut(`/api/News/${id}`);
  }
}

export default API;