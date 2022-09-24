namespace Database.Search;

public class SearchFilter
{
    public int Amount { get; set; } = 10;
    public string? Username { get; set; }
    public SearchRange? Elo { get; set; }
    public int? MinimumFriendAmount { get; set; }

    public override int GetHashCode()
    {
        unchecked
        {
            var result = 0;
            result = (result * 397) ^ Amount;
            if (Username is not null) result = (result * 397) ^ Username.GetHashCode();
            if (Elo is not null) result = (result * 397) ^ Elo.GetHashCode();
            if (MinimumFriendAmount is not null) result = (result * 397) ^ (int)MinimumFriendAmount;
            return result;
        }
    }

    public override string ToString()
    {
        var items = new List<string>();
        if (Username is not null) items.Add($"Username: {Username}");
        if (Elo is not null) items.Add($"EloRange: {Elo}");
        if (MinimumFriendAmount is not null) items.Add($"MinimumFriendAmount: {MinimumFriendAmount}");
        items.Add($"Limit: {Amount}");

        return string.Join(", ", items);
    }
}