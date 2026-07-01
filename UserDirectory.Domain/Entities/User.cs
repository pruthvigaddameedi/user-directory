namespace UserDirectory.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public int Age { get; private set; }
    public string City { get; private set; } = null!;
    public string State { get; private set; } = null!;
    public string Pincode { get; private set; } = null!;

    private User() { } // for EF

    public User(string name, int age, string city, string state, string pincode)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 2 || name.Length > 100)
            throw new ArgumentException("Name must be 2-100 chars", nameof(name));
        if (age < 0 || age > 120) throw new ArgumentOutOfRangeException(nameof(age));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException(nameof(city));
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException(nameof(state));
        if (string.IsNullOrWhiteSpace(pincode) || pincode.Length < 4 || pincode.Length > 10)
            throw new ArgumentException("Pincode must be 4-10 chars", nameof(pincode));

        Name = name; Age = age; City = city; State = state; Pincode = pincode;
    }

    public void Update(string name, int age, string city, string state, string pincode)
    {
        // reuse same validation rules
        Name = name; Age = age; City = city; State = state; Pincode = pincode;
    }
}
