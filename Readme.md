<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128610032/19.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T267756)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Rich Text Editor for WinForms - How to Drag-and-Drop GridView Cell Data

This example illustrates how to drag-and-drop GridView cell data to a RichEditControl document.

Handle GridView's MouseMove and MouseDown events to obtain the grid cell under the cursor position via the GridHitInfo object and get the cell's data.

In the `RichEditControl.DragOver` event handler, update the `RichEditControl.Document.CaretPosition` property value according to the current mouse position. In the `RichEditControl.DragDrop` event, just insert the stored text to the caret position and call the `RichEditControl.Focus` method to focus the RichEditControl.

## Files to Review

* [Form1.cs](./CS/DragDropExample/Form1.cs) (VB: [Form1.vb](./VB/DragDropExample/Form1.vb))

## More Examples

* [How to perform drag-and-drop operation in a custom manner](https://www.devexpress.com/Support/Center/Example/Details/E2943)

## Documentation

* [How to: Obtain the Document Position under the Mouse Pointer](https://docs.devexpress.com/WindowsForms/6012/controls-and-libraries/rich-text-editor/examples/text/how-to-obtain-the-document-position-under-the-mouse-pointer)
