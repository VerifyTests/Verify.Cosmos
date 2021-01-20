using Newtonsoft.Json;

public class Family
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public Parent[] Parents { get; set; } = null!;
    public Child[] Children { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public bool IsRegistered { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class Parent
{
    public string FamilyName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
}

public class Child
{
    public string FamilyName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public int Grade { get; set; }
    public Pet[] Pets { get; set; } = null!;
}

public class Pet
{
    public string GivenName { get; set; } = null!;
}

public class Address
{
    public string State { get; set; } = null!;
    public string County { get; set; } = null!;
    public string City { get; set; } = null!;
}