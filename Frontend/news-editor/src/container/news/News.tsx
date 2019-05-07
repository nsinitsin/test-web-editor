import * as React from "react";
import './News.scss';
import TextEditor from "../../components/ui/text-editor/TextEditor";
import API from "../../utils/API";
import {INewsInputModel} from "../../models/INewsInputModel";
import Panel from "../../components/ui/panel/Panel";
import TextPreview from "../../components/ui/text-preview/TextPreview";
import Button from "@material-ui/core/Button";
import {INewsUpdateOutputModel} from "../../models/INewsUpdateOutputModel";

interface INewsProps {
}

interface INewsState {
  draftNews: INewsInputModel | null,
  liveNews: INewsInputModel | null,
  isLoading: boolean,
  isCollapsedLiveNews: boolean,
  isCollapsedDraftNews: boolean,
  editingText: INewsInputModel | null,
  isEditingDraftNews: boolean,
  isEditingLiveNews: boolean
}

class News extends React.Component<INewsProps, INewsState> {
  state: INewsState = {
    draftNews: null,
    liveNews: null,
    isLoading: true,
    isCollapsedLiveNews: true,
    isCollapsedDraftNews: true,
    editingText: null,
    isEditingDraftNews: false,
    isEditingLiveNews: false
  }

  createNewEditingNews = () => {
    const modal: INewsInputModel = { text: '', newsId: -1, createdOn: new Date() };
    this.setState({editingText: modal, isEditingDraftNews:true});
  }

  async saveDraftNews() {
    const modelToSave = this.state.editingText;
    this.setState({editingText: null, isEditingDraftNews: false});
    if (!modelToSave)
      return;

    const outputModel: INewsUpdateOutputModel = { text: modelToSave.text};
    if (modelToSave.newsId > 0) {
      await API.updateDraftNews(outputModel, modelToSave.newsId);
    }
    else {
      await API.saveNewNews(outputModel);
    }
  }

  async updateLiveNews() {
    const modelToSave = this.state.editingText;
    this.setState({editingText: null, isEditingLiveNews:true});
    if (!modelToSave)
      return;

    const outputModel: INewsUpdateOutputModel = { text: modelToSave.text};
    if (modelToSave.newsId > 0) {
      await API.updateLiveNews(outputModel, modelToSave.newsId);
    }
    else {
      //ToDo error
    }
  }

  async goToLiveNews() {
    const {draftNews} = this.state;

    if (!draftNews) {
      //todo show error message
    } else {
      await API.goToLiveDraftNews(draftNews.newsId);
    }

    this.setState({draftNews: null})
  }

  async componentDidMount() {
    let draftNews = null;
    let liveNews = null;

    try {
      liveNews = await API.getActiveNews() as INewsInputModel;
    } catch (e) {
      //ToDo add global error message
    }
    try {
      draftNews = (await API.getDraftNews()) as INewsInputModel;
    } catch (e) {
      //ToDo add global error message
    }
    this.setState({
      isLoading: false,
      draftNews: draftNews,
      liveNews: liveNews
    });
  }

  render() {

    const {liveNews, draftNews, editingText, isLoading, isCollapsedDraftNews, isCollapsedLiveNews, isEditingDraftNews, isEditingLiveNews} = this.state;

    const editingContent = editingText ? editingText.text : '';

    if (!isLoading) {
        return (
          <div className="news">
            <Panel className="news__draft-live-news">
              <Panel className="news__draft-live-news-draft" title="Draft" noMargin>
                {draftNews ?
                  <div>
                    <Button onClick={() => this.setState({isCollapsedDraftNews: !isCollapsedDraftNews})}>Show more</Button>
                    <Button onClick={() => this.setState({editingText: draftNews, isEditingDraftNews: true})}>Edit</Button>
                    {!isEditingLiveNews && editingText ? <Button onClick={() => this.saveDraftNews()}>Save</Button> : null}
                    <Button onClick={()=> this.goToLiveNews()}>To live</Button>
                    <TextPreview content={draftNews.text} isCollapsed={isCollapsedDraftNews}/>
                  </div>
                  :
                  !isEditingDraftNews
                    ? <Button onClick={() => this.createNewEditingNews()}>Create new draft</Button>
                    : <Button onClick={() => this.saveDraftNews()}>Save</Button>
                }
              </Panel>
              <Panel className="news__draft-live-news-live" title="Online" noMargin>
                {liveNews ?
                  <div>
                    <Button onClick={() => this.setState({isCollapsedLiveNews: !isCollapsedLiveNews})}>Show more</Button>
                    <Button onClick={() => this.setState({editingText: liveNews, isEditingLiveNews: true})}> Edit</Button>
                    {!isEditingDraftNews && editingText ? <Button onClick={() => this.updateLiveNews()}>Save</Button> : null}
                    <TextPreview content={liveNews.text} isCollapsed={isCollapsedLiveNews}/>
                  </div>
                  :
                  "No news online"
                }
              </Panel>
            </Panel>
            {editingText ?
              <Panel>
                <Button onClick={() => this.setState({editingText: null})}>Close</Button>
                <TextEditor
                  content={editingContent}
                  onChange={(content: string) => {
                    editingText.text = content;
                  }}/>
              </Panel>
              : null
            }
          </div>
        )
    }
    return null;
  }
}

export default News;