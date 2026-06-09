namespace CommBank.Models;

interface IUpdatedIcon
{
    string Icon { get; set; }
}


<<<<<<< HEAD
public class UpdatedIcon : IUpdatedIcon
{
    public UpdatedIcon(string icon)
    {
        Icon = icon;
    }

    public string Icon { get; set; }
=======
public class UpdatedIcon(string icon) : IUpdatedIcon
{
    public string Icon { get; set; } = icon;
>>>>>>> 2bc1eb6 (Your commit message)
}
