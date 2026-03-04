namespace authentication.Models;

public class User{
    public Guid Id{get;private set;} = Guid.NewGuid();
    public string Email{get;set;}=default!;
    public string Password{get;set;}=default!;
    public string Name{get;set;} =default!;

    public User(string name,string email,string password)
    {
        Name =name;
        Email = email;
        Password = password;
    }
}

public record TokenContainer(string AccessToken);
