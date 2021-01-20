using System;
using Newtonsoft.Json;

public class Family
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    public string LastName { get; set; } = null!;
    public Address Address { get; set; } = null!;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class Address
{
    public string State { get; set; } = null!;
    public string County { get; set; } = null!;
    public string City { get; set; } = null!;
}