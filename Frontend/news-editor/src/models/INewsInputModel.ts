export interface INewsInputModel extends IBaseNewsModel{
  newsId: number,
  createdOn: Date
}

export interface IBaseNewsModel {
  text: string
}