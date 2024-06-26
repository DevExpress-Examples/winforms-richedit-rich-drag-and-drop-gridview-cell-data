<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128610032/14.2.8%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T267756)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/DragDropExample/Form1.cs) (VB: [Form1.vb](./VB/DragDropExample/Form1.vb))
<!-- default file list end -->
# How to drag-and-drop GridView cell data to a RichEditControl document


<p>This example illustrates how to drag-and-drop GridView cell data to a RichEditControl document . </p>
<p>Handle GridView's MouseMove and MouseDown events to obtain the grid cell under the cursor position via the GridHitInfo object and get the cell's data.<br />Also, use the following RichEditControl events: DragOver and DragDrop. Most of the work is performed in the DragOver event handler. Here, you should update the RichEditControl.Document.CaretPosition property value according to the current mouse position (use the approach described in the <a href="https://documentation.devexpress.com/#WindowsForms/CustomDocument6012">How to: Obtain the Document Position under the Mouse Cursor</a> article). In the DragDrop event, just insert the stored text to the caret position and call the RichEditControl.Focus method to focus the RichEditControl.<br /><br />See also: <a href="https://www.devexpress.com/Support/Center/Example/Details/E2943">How to perform drag-and-drop operation in a custom manner</a>. </p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-richedit-rich-drag-and-drop-gridview-cell-data&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-richedit-rich-drag-and-drop-gridview-cell-data&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
