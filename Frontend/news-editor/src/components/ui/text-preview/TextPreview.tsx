import React from 'react'
import './TextPreview.scss'

import {Theme, WithStyles, withStyles} from '@material-ui/core/styles';
import {Collapse} from "@material-ui/core";
import Paper from "@material-ui/core/Paper";
import Button from "@material-ui/core/Button";

const styles = (theme: Theme) => ({
  root: {
    width: 440
  },
  container: {
    display: 'flex',
  },
  paper: {
    margin: theme.spacing.unit,
    maxWidth: 440
  }
});

interface ITextPreviewProps extends WithStyles<typeof styles> {
  content: string,
  isCollapsed: boolean
}

class TextPreview extends React.Component<ITextPreviewProps, any> {

  render() {
    const {content, classes, isCollapsed} = this.props;
    return (
      <div className={classes.root}>
        <div className={classes.container}>
          <Collapse className='someclass someclass'  in={!isCollapsed} collapsedHeight="40px">
            <Paper elevation={4} className={classes.paper}>
              {content}
            </Paper>
          </Collapse>
        </div>
      </div>
    )
  }
}

export default withStyles(styles)(TextPreview);