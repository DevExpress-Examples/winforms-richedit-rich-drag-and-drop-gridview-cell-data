using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragDropExample
{
    public class GridCellData
    {
        string content;
        public GridCellData(string text)
        {
            content = text;
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
