namespace CommBank.Data;

public class CollectionNameAttribute : Attribute
{
    public string Name { get; set; }
    public CollectionNameAttribute(string name)  => Name = name;
}