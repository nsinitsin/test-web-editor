import * as React from "react";
import API from "../../utils/API";
import {IArchivedInputModel} from "../../models/IArchivedInputModel";
import Panel from "../../components/ui/panel/Panel";
import Button from "@material-ui/core/Button";
import TextPreview from "../../components/ui/text-preview/TextPreview";
import Paper from "@material-ui/core/Paper";
import {Collapse, Theme, withStyles, WithStyles} from "@material-ui/core";

const styles = (theme: Theme) => ({
  root: {
    margin: 5
  },
  container: {
    display: 'flex',
  },
  paper: {
    margin: 10
  }
});

interface IArchivedNewsProps extends WithStyles<typeof styles>{

}

class ArchivedNews extends React.Component<IArchivedNewsProps, any> {
  state: any = {
    isLoading: true,
    draftNews: null
  }

  async componentDidMount() {
    try {
      const draftNews = await API.getArchivedNews(0, 100) as Array<IArchivedInputModel>;

      draftNews.forEach((draft) =>
        this.setState({["collapsed" + draft.newsId]: !this.state["collapsed" + draft.newsId]}));

      this.setState({
        isLoading: false,
        draftNews: draftNews,
      });
    } catch (e) {
      //ToDo add global error message
    }
  }

  render() {
    const {isLoading, draftNews} = this.state;
    const {classes} = this.props;

    if (isLoading) {
      return <h2>Is loading</h2>;
    }

    if (draftNews.length === 0) {
      return <h2>No archived news</h2>
    }

    return (
      draftNews.map((dNews: IArchivedInputModel) => (

        <Panel className="news__draft-live-news">
          <div className={classes.root}>
            <div className={classes.container}>
              <Paper elevation={4} className={classes.paper}>
                {dNews.text}
              </Paper>
            </div>
          </div>
        </Panel>
      ))
    )
  }
}

export default withStyles(styles)(ArchivedNews);