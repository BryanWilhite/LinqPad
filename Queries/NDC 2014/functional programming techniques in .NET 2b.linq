<Query Kind="VBStatements" />

'    Hans-Christian Holm - Case study: Making use of functional programming techniques in .NET
'    [https://vimeo.com/97541187]
'    http://www.yr.no/

dim viewItem as Func(of integer, XElement) =
    function(temperature as integer)
        return <li><%= temperature %></li>
    end function

dim viewList as Func(of integer(), XElement) =
    function(temperatures as integer())
        return <ul><%= temperatures.Select(viewItem) %></ul>
    end function

viewItem(77).Dump()

dim highs() as integer = new integer() { 64, 77, 88, 68, 73, 77, 80 }
viewList(highs).Dump()
