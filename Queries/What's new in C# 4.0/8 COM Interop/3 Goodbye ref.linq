<Query Kind="Statements">
  <GACReference>Microsoft.Office.Interop.Excel, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.Office.Interop.Word, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <Namespace>Microsoft.Office.Interop.Word</Namespace>
</Query>

const string filename = @"c:\source\test.docx";

var word = new Application();
word.Visible = true;
word.Documents.Add (filename);