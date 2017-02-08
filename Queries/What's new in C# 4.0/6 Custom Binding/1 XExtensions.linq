<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;System.Dynamic.dll</Reference>
  <Namespace>System.Dynamic</Namespace>
</Query>

void Main()
{
	XElement x = XElement.Parse (@"<Label Text='Hello' Id='5'/>");
	dynamic da = x.DynamicAttributes();
	Console.WriteLine (da.Id);
}

}
static class XExtensions
{
	public static dynamic DynamicAttributes (this XElement e)
	{
		return new XWrapper (e);
	}
	
	class XWrapper : DynamicObject
	{
		XElement _element;
		public XWrapper (XElement e) { _element = e; }
	
		public override bool TryGetMember (GetMemberBinder binder, out object result)
		{
			result = _element.Attribute (binder.Name).Value;
			return true;
		}
	
		public override bool TrySetMember (SetMemberBinder binder, object value)
		{
			_element.SetAttributeValue (binder.Name, value);
			return true;
		}
	}