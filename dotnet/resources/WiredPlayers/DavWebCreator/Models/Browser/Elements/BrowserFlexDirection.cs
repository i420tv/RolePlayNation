using System;
using System.Collections.Generic;
using System.Text;

namespace DavWebCreator.Server.Models.Browser.Elements
{
    /// <summary>
    /// Indicator for the direction the elements inside of this should be floated. (e.g. from left to right, from bottom to top....) 
    /// </summary>
    public enum BrowserFlexDirection
    {
        flex_row,
        flex_row_reverse,
        flex_column,
        flex_column_reverse,
        flex_sm_row,
        flex_sm_row_reverse,
        flex_sm_column,
        flex_sm_column_reverse,
        flex_md_row,
        flex_md_row_reverse,
        flex_md_column,
        flex_md_column_reverse,
        flex_lg_row,
        flex_lg_row_reverse,
        flex_lg_column,
        flex_lg_column_reverse,
        flex_xl_row,
        flex_xl_row_reverse,
        flex_xl_column,
        flex_xl_column_reverse,
        inherit,
        initial,
        unset

    }
}
