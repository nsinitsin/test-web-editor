import * as React from 'react'
import { Editor } from 'react-draft-wysiwyg'
import {EditorState, ContentState, convertToRaw, convertFromRaw} from 'draft-js'
import 'react-draft-wysiwyg/dist/react-draft-wysiwyg.css'
// import draftToHtml from 'draftjs-to-html'
// import htmlToDraft from 'html-to-draftjs'
import { debounce } from 'ts-debounce';

import {StyleRules, WithStyles} from "@material-ui/core/styles";
import withStyles from "@material-ui/core/styles/withStyles";

const stylesheet: StyleRules = {
  editorWrapper: {
    marginTop: '1rem',
  },
  editor: {
    border: '1px solid #f1f1f1',
    height: '500px',
    padding: '1rem',
    overflow: 'scroll',
  },
  editorLinkPopup: {
    height: 'auto',
  },
  editorImagePopup: {
    left: '-100%',
  },
}

interface ITextEditor extends WithStyles<typeof stylesheet> {
  onChange: (val: string) => void,
  content: string
}

type State = {
  editorState: EditorState
}

class TextEditor extends React.Component<ITextEditor, State> {
  static defaultProps = {}

  state: State = {
    editorState: EditorState.createEmpty(),
  }

  constructor(props: ITextEditor) {
    super(props)
  }

  componentDidMount() {
    const {content} = this.props;

    this.setState({
      editorState: this.convertHTMLtoEditorState(content),
    })
  }

  convertHTMLtoEditorState(html: string): EditorState {
    if (html && html !== '') {
      const contentState = convertFromRaw(JSON.parse(html))
      const editorState = EditorState.createWithContent(contentState)
      return editorState
    }

    return EditorState.createEmpty()
  }

  onEditorStateChange = (editorState : EditorState) => {
    this.setState({
      editorState,
    })

    this.callOnChangeCb()
  }

  callOnChangeCb = debounce( () => {
    this.props.onChange(JSON.stringify(convertToRaw(this.state.editorState.getCurrentContent())));
  }, 400)

  render() {
    const { classes } = this.props
    return (
      <Editor
        localization={{
          locale: 'us',
        }}
        editorState={this.state.editorState}
        onEditorStateChange={this.onEditorStateChange}
        wrapperClassName={classes.editorWrapper}
        editorClassName={classes.editor}
        toolbar={{
          fontFamily: {
            options: ['Nanum Square', 'Arial', 'Georgia', 'Impact', 'Tahoma', 'Verdana'],
          },
          link: {
            popupClassName: classes.editorLinkPopup,
          }
        }}
      />
    )
  }
}

export default withStyles(stylesheet)(TextEditor)