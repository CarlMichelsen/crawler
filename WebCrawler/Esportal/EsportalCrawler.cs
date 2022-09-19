using Database.Entities;

namespace WebCrawler.Esportal;

public class EsportalCrawler : ICrawler<UnknownEntity>
{
    private EsportalRequestHandler _handler;

    public EsportalCrawler()
    {
        _handler = new();
    }

    public async Task<UnknownEntity?> Next()
    {
        return await _handler.GetNext();
    }

    public async Task<bool> Act(UnknownEntity? input)
    {
        var result = await _handler.HandleNext(input);
        var success = await _handler.FinalizeNext(input, result, null);
        return success;
    }

    private UnknownEntity BootstrapUserEntity()
    {
        // mock bootstrap data taken 30. aug. 2022
        var usr = new UserEntity();
        usr.Id = 957283692;
        usr.Username = "mag";
        usr.AvatarHash = "ecfef2bf5b1146c96566d92b6d8b663cb0f4d2c3";
        usr.CountryId = 57;
        usr.DisplayMedals = 115587185;
        usr.Level = null;
        usr.Flags = 271679521;
        usr.RegionId = 0;
        usr.SubregionId = 0;

        var ent = new UnknownEntity();
        ent.User = usr;
        ent.Recorded = DateTime.Now;
        return ent;
    }
}