namespace CommBank.Models;

interface ILoginInput
{
    string Email { get; set; }
    string Password { get; set; }
}


public class LoginInput : ILoginInput
{
    public LoginInput(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}
