using System.Collections.Generic;
using UnityEngine;

public class NormalCubeItem : Item
{
    private static readonly ItemImageRepo _itemImageRepo = ItemImageRepo.Instance;

    private readonly Dictionary<MatchType, Sprite> aCubeDict = new()
    {
        { MatchType.Green, _itemImageRepo.GreenCubeA },
        { MatchType.Red, _itemImageRepo.RedCubeA },
        { MatchType.Blue, _itemImageRepo.BlueCubeA },
        { MatchType.Purple, _itemImageRepo.PurpleCubeA },
        { MatchType.Pink, _itemImageRepo.PinkCubeA },
        { MatchType.Yellow, _itemImageRepo.YellowCubeA }
    };

    private readonly Dictionary<MatchType, Sprite> bCubeDict = new()
    {
        { MatchType.Green, _itemImageRepo.GreenCubeB },
        { MatchType.Red, _itemImageRepo.RedCubeB },
        { MatchType.Blue, _itemImageRepo.BlueCubeB },
        { MatchType.Purple, _itemImageRepo.PurpleCubeB },
        { MatchType.Pink, _itemImageRepo.PinkCubeB },
        { MatchType.Yellow, _itemImageRepo.YellowCubeB }
    };

    private readonly Dictionary<MatchType, Sprite> cCubeDict = new()
    {
        { MatchType.Green, _itemImageRepo.GreenCubeC },
        { MatchType.Red, _itemImageRepo.RedCubeC },
        { MatchType.Blue, _itemImageRepo.BlueCubeC },
        { MatchType.Purple, _itemImageRepo.PurpleCubeC },
        { MatchType.Pink, _itemImageRepo.PinkCubeC },
        { MatchType.Yellow, _itemImageRepo.YellowCubeC }
    };

    private readonly Dictionary<MatchType, Sprite> defaultCubeDict = new()
    {
        { MatchType.Green, _itemImageRepo.GreenCubeDefault },
        { MatchType.Red, _itemImageRepo.RedCubeDefault },
        { MatchType.Blue, _itemImageRepo.BlueCubeDefault },
        { MatchType.Purple, _itemImageRepo.PurpleCubeDefault },
        { MatchType.Pink, _itemImageRepo.PinkCubeDefault },
        { MatchType.Yellow, _itemImageRepo.YellowCubeDefault }
    };

    private MatchType _matchType;

    public void PrepareNormalCubeItem(ItemBase itemBase, MatchType matchType)
    {
        _matchType = matchType;
        itemBase.canClickable = true;
        Prepare(itemBase, GetSpritesForType());
    }

    private Sprite GetSpritesForType()
    {
        return defaultCubeDict.GetValueOrDefault(_matchType);
    }

    public override MatchType GetMatchType()
    {
        return _matchType;
    }

    public override void HintUpdateToSprite(ItemType itemType, int matchCount)
    {
        UpdateColorfulSprite(matchCount);
    }

    private void UpdateColorfulSprite(int matchedCount) //junk code update this.
    {
        Sprite newSprite;
        if (matchedCount > 4 && matchedCount < 8) //A
        {
            newSprite = aCubeDict.GetValueOrDefault(_matchType);
            UpdateSprite(newSprite);
        }
        else if (matchedCount > 7 && matchedCount < 10) //B
        {
            newSprite = bCubeDict.GetValueOrDefault(_matchType);
            UpdateSprite(newSprite);
        }
        else if (matchedCount > 9) //C
        {
            newSprite = cCubeDict.GetValueOrDefault(_matchType);
            UpdateSprite(newSprite);
        }
        else
        {
            newSprite = defaultCubeDict.GetValueOrDefault(_matchType);
            UpdateSprite(newSprite);
        }
    }

    public override void Execute()
    {
        ParticlesController.Instance.PlayParticleSystem(this);
        AudioController.Instance.PlaySoundEffect(SoundEffects.Cube);
        base.Execute();
    }
}