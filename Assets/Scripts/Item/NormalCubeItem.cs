using UnityEngine;
using VContainer;

public class NormalCubeItem : Item
{
    private MatchType _matchType;
    public void PrepareNormalCubeItem(ItemBase itemBase, MatchType matchType)
    {
        _matchType = matchType;
        itemBase.canClickable = true;
        Prepare(itemBase, GetSpritesForType());
    }

    private Sprite GetSpritesForType()
    {
        switch (_matchType)
        {
            case MatchType.Green:
                return ItemImageRepo.Instance.GreenCubeDefault;
            case MatchType.Yellow:
                return ItemImageRepo.Instance.YellowCubeDefault;
            case MatchType.Blue:
                return ItemImageRepo.Instance.BlueCubeDefault;
            case MatchType.Red:
                return ItemImageRepo.Instance.RedCubeDefault;
            case MatchType.Pink:
                return ItemImageRepo.Instance.PinkCubeDefault; 
            case MatchType.Purple:
                return ItemImageRepo.Instance.PurpleCubeDefault;
        }
        return null;
    }

    public override MatchType GetMatchType()
    {
        return _matchType;
    }

    public override void HintUpdateToSprite(ItemType itemType, int matchCount)
    {
        if (itemType == ItemType.Hint)
        {
            UpdateColorfulSprite(matchCount);
        }else
        {
            UpdateSprite(GetSpritesForType());
        }
    }
    
    private void UpdateColorfulSprite(int matchedCount) //junk code update this.
    {
        Sprite newSprite;
        if (matchedCount > 4 && matchedCount < 8) //A
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = ItemImageRepo.Instance.GreenCubeA;
                    break;
                case MatchType.Yellow:
                    newSprite = ItemImageRepo.Instance.YellowCubeA;
                    break;
                case MatchType.Blue:
                    newSprite = ItemImageRepo.Instance.BlueCubeA;
                    break;
                case MatchType.Red:
                    newSprite = ItemImageRepo.Instance.RedCubeA;
                    break;
                case MatchType.Purple:
                    newSprite = ItemImageRepo.Instance.PurpleCubeA;
                    break;
                case MatchType.Pink:
                    newSprite = ItemImageRepo.Instance.PinkCubeA;
                    break;
                default:
                    return;
            }
        }else if (matchedCount > 7 && matchedCount < 10) //B
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = ItemImageRepo.Instance.GreenCubeB;
                    break;
                case MatchType.Yellow:
                    newSprite = ItemImageRepo.Instance.YellowCubeB;
                    break;
                case MatchType.Blue:
                    newSprite = ItemImageRepo.Instance.BlueCubeB;
                    break;
                case MatchType.Red:
                    newSprite = ItemImageRepo.Instance.RedCubeB;
                    break;
                case MatchType.Purple:
                    newSprite = ItemImageRepo.Instance.PurpleCubeB;
                    break;
                case MatchType.Pink:
                    newSprite = ItemImageRepo.Instance.PinkCubeB;
                    break;
                default:
                    return;
            }
        }else if (matchedCount > 9) //C
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = ItemImageRepo.Instance.GreenCubeC;
                    break;
                case MatchType.Yellow:
                    newSprite = ItemImageRepo.Instance.YellowCubeC;
                    break;
                case MatchType.Blue:
                    newSprite = ItemImageRepo.Instance.BlueCubeC;
                    break;
                case MatchType.Red:
                    newSprite = ItemImageRepo.Instance.RedCubeC;
                    break;
                case MatchType.Purple:
                    newSprite = ItemImageRepo.Instance.PurpleCubeC;
                    break;
                case MatchType.Pink:
                    newSprite = ItemImageRepo.Instance.PinkCubeC;
                    break;
                default:
                    return;
            }
        }
        else
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = ItemImageRepo.Instance.GreenCubeDefault;
                    break;
                case MatchType.Yellow:
                    newSprite = ItemImageRepo.Instance.YellowCubeDefault;
                    break;
                case MatchType.Blue:
                    newSprite = ItemImageRepo.Instance.BlueCubeDefault;
                    break;
                case MatchType.Red:
                    newSprite = ItemImageRepo.Instance.RedCubeDefault;
                    break;
                case MatchType.Purple:
                    newSprite = ItemImageRepo.Instance.PurpleCubeDefault;
                    break;
                case MatchType.Pink:
                    newSprite = ItemImageRepo.Instance.PinkCubeDefault;
                    break;
                default:
                    return;
            }
        }
    }

    public override void Execute()
    {
        //ParticleManager.Instance.PlayParticle(this);
        //AudioManager.Instance.PlayEffect(SoundID);
        base.Execute();
    }
}
