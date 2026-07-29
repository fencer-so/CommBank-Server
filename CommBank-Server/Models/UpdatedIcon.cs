namespace CommBank.Models;

interface IUpdatedIcon
{
    string Icon { get; set; }
}


public class UpdatedIcon : IUpdatedIcon
{
    public UpdatedIcon()
    {
        Icon = string.Empty;
    }

    public UpdatedIcon(string icon)
    {
        Icon = icon;
    }

    public string Icon { get; set; }
}
