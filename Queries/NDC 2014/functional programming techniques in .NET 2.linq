<Query Kind="VBProgram" />

'    Hans-Christian Holm - Case study: Making use of functional programming techniques in .NET
'    [https://vimeo.com/97541187]
'    http://www.yr.no/
sub Main
    ViewItem(77).Dump()
    
    dim temperatures() as integer = new integer() { 64, 77, 88, 68 }
    ViewList(temperatures).Dump()
end sub

function ViewItem(temperature as integer) as XElement
    return <li><%= temperature %></li>
end function

function ViewList(temperatures as integer()) as XElement
    return <ul><%= temperatures.Select(addressof ViewItem) %></ul>
end function
