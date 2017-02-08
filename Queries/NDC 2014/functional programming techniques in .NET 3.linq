<Query Kind="VBProgram">
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

'    Hans-Christian Holm - Case study: Making use of functional programming techniques in .NET
'    [https://vimeo.com/97541187]
'    http://www.yr.no/

readonly Memos as ConcurrentDictionary(of string, XElement) = new ConcurrentDictionary(of string, XElement)

function Memoise(f as Func(of XElement), key as string) as Func(of XElement)
    return function() Memos.GetOrAdd(key, function(k) f())
end function

function ViewItem(temperature as integer) as XElement
    return <li><%= temperature %></li>
end function

function ViewList(temperatures as integer()) as XElement
    return <ul><%= temperatures.Select(addressof ViewItem) %></ul>
end function

function GetData() as integer()
    dim s as string = "Getting data..."
    s.Dump()
    return {2, 3}
end function

function ListComponent() as XElement
    dim data = GetData()
    return <div><%= ViewList(data) %></div>
end function

sub Main
    dim c = Memoise(addressof ListComponent, "my-component")
    c().Dump()
    c().Dump()
end sub