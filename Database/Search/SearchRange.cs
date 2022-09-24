namespace Database.Search;

public class SearchRange
{
    public int? Min { get; set; }
    public int? Max { get; set; }

    public override int GetHashCode()
    {
        unchecked
        {
            var result = 0;
            if (Min is not null) result = (result * 397) ^ (int)Min;
            if (Max is not null) result = (result * 397) ^ (int)Max;
            return result;
        }
    }

    public override string ToString()
    {
        if (Max is not null && Min is null)
        {
            return $"<= {Max}";
        }
        else if (Max is null && Min is not null)
        {
            return $"{Min} <=";
        }
        else
        {
            return $"{Min} <= {Max}";
        }
    }
}