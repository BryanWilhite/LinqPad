<Query Kind="Program">
  <NuGetReference>NAuto</NuGetReference>
  <Namespace>Amido.NAuto</Namespace>
  <Namespace>Amido.NAuto.Builders</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
    var config = new AutoBuilderConfiguration
    {
        MaxDepth = 1
    };
    
    var segment = NAuto
        .AutoBuild<Segment>(config)
        .Construct()
        .Build();
    Console.WriteLine($"{nameof(Segment)}: {segment}");
    
    var jO = JObject.FromObject(segment);
    Console.WriteLine($"json: {jO.ToString()}");
    
    segment = jO.FromJObject<ISegment, Segment>();
    Console.WriteLine($"{nameof(Segment)} from JObject: {segment}");
}

/// <summary>
/// GenericWeb Segment
/// </summary>
public partial class Segment : ISegment
{
    public Segment()
    {
        this.ChildSegments = new List<Segment>();
    }

    public string ClientId { get; set; }

    public Nullable<DateTime> CreateDate { get; set; }

    public Nullable<bool> IsActive { get; set; }

    public Nullable<int> ParentSegmentId { get; set; }

    public int SegmentId { get; set; }

    public string SegmentName { get; set; }

    public Nullable<byte> SortOrdinal { get; set; }

    public virtual ICollection<Segment> ChildSegments { get; set; }

    public virtual Segment ParentSegment { get; set; }
}

public interface ISegment
{
    int SegmentId { get; set; }
    string SegmentName { get; set; }
    Nullable<byte> SortOrdinal { get; set; }
    Nullable<System.DateTime> CreateDate { get; set; }
    Nullable<int> ParentSegmentId { get; set; }
    string ClientId { get; set; }
    Nullable<bool> IsActive { get; set; }
}