import {Theme, WithStyles} from "@material-ui/core";
import * as React from "react";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Button from "@material-ui/core/Button";
import {Link, Switch} from "react-router-dom";
import Typography from "@material-ui/core/Typography";
import withStyles, {StyleRules} from "@material-ui/core/styles/withStyles";
import createStyles from "@material-ui/core/styles/createStyles";
import {blue, green, red} from "@material-ui/core/colors";

const styles = (theme: Theme): StyleRules =>
  createStyles({
    root: {
      textAlign: 'center',
      color: blue[50],
    },
    button: {
      backgroundColor: green[500]
    }
  });

interface IContainer extends WithStyles<typeof styles> {

}

const Container: React.FC<IContainer> = ({classes, children}) => {

  return (
    <div>
      <AppBar className={classes.root} id="bar" position="static">
        <Toolbar>
          <Button component={Link} {...{to: "/"} as any} color="inherit">
            <Typography variant="h6" color="inherit">
              News
            </Typography>
          </Button>
          <Button component={Link} {...{to: "/archived-news/"} as any} color="inherit">
            <Typography variant="h6" color="inherit">
              Archived news
            </Typography>
          </Button>
        </Toolbar>
      </AppBar>
      <Switch>
        {children}
      </Switch>
    </div>
  );
}

export default withStyles(styles)(Container);