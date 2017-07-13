<Query Kind="Program">
  <NuGetReference>System.ComponentModel.Annotations</NuGetReference>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
</Query>

void Main()
{
    var data = new DataClass();
    var dataType = typeof(DataClass);
    var results = new List<ValidationResult>();
    var context = new ValidationContext(data);

    var provider = new AssociatedMetadataTypeTypeDescriptionProvider(dataType);
    TypeDescriptor.AddProviderTransparent(provider, dataType);
    /*
        Without AssociatedMetadataTypeTypeDescriptionProvider
        Validation will not occur correctly.
        [http://stackoverflow.com/questions/6349645/validator-tryvalidateproperty-not-working]
    */

    Validator.TryValidateObject(data, context, results);
    results.Dump();
}

class DataClassMetadata
{
    [Display(Name="My Category")]
    [Required]
    public string DataCategory { get; set; }
}

[MetadataType(typeof(DataClassMetadata))]
class DataClass
{
    public string DataCategory { get; set; }
}