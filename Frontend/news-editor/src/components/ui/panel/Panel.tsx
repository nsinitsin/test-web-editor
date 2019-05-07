import React, { Component } from 'react';
import './Panel.scss';

interface IPanel {
  /**
   * Change the background colour
   */
  clear?: boolean,
  /**
   * The elements which will be wrapped by the panel
   */
  children: any,
  /**
   * The title of the panel
   */
  title?: string,
  /**
   * Allow custom styling
   */
  className?: string,
  /**
   * add dots to border
   */
  dotted?: boolean,
  /**
   * remove border
   */
  noBorder?: boolean,
  /**
   * add inline style
   */
  style?: object,
  /**
   * callback on click on the panel container
   */
  onClick?: (evt: any) => void,
  /**
   * callback to be triggered on hover in
   */
  onMouseEnter?: () => Event,
  /**
   * callback to be triggered on hover out
   */
  onMouseLeave?: () => Event,
  /*
   * Remove the bottom margin
   */
  noMargin?: boolean,
}

class Panel extends Component<IPanel, any> {
  render() {
    const {
      children = null,
      title = "",
      className = "",
      style = {},
      dotted = false,
      clear = false,
      onClick = () => {},
      onMouseEnter = () => {},
      onMouseLeave = () => {},
      noMargin = false,
      noBorder = false
    } = this.props;

    const isDotted = dotted ? "content-panel--dotted" : "";
    const isNoBorder = noBorder ? "content-panel--noborder" : "";
    const isClear = clear ? "content-panel--clear" : "";
    const onClickCb = typeof onClick === 'function' ? { onClick } : {};
    const onMouseEnterCb = typeof onMouseEnter === 'function' ? { onMouseEnter } : {};
    const onMouseLeaveCb = typeof onMouseLeave === 'function' ? { onMouseLeave } : {};
    const margin = noMargin ? "mb0" : "";

    return (
      <section className={`content-panel ${isDotted} ${isClear} ${margin} ${isNoBorder} ${className}`} style={style} {...onClickCb} {...onMouseEnterCb} {...onMouseLeaveCb}>
        {title && <h6 className="content-panel-title">{title}</h6>}
        {children}
      </section>
    );
  }
}


export default Panel;
