namespace CommBank.Models;

interface ILoginInput
{
    string Email { get; set; }
    string Password { get; set; }
}


<<<<<<< HEAD
public class LoginInput : ILoginInput
{
    public LoginInput(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
=======
public class LoginInput(string email, string password) : ILoginInput
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
>>>>>>> 2bc1eb6 (Your commit message)
}
