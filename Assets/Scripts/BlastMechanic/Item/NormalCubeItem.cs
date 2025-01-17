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
        var imageRepo = ItemImageRepo.Instance;
        
        if (matchCount > 3)
        {
            UpdateColorfulSprite(imageRepo,matchCount);
        }else
        {
            UpdateSprite(GetSpritesForType());
        }
    }
    
    private void UpdateColorfulSprite(ItemImageRepo itemRepo,int matchedCount) //junk code update this.
    {
        Sprite newSprite;
        if (matchedCount > 4 && matchedCount < 8) //A
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = itemRepo.GreenCubeA;
                    break;
                case MatchType.Yellow:
                    newSprite = itemRepo.YellowCubeA;
                    break;
                case MatchType.Blue:
                    newSprite = itemRepo.BlueCubeA;
                    break;
                case MatchType.Red:
                    newSprite = itemRepo.RedCubeA;
                    break;
                case MatchType.Purple:
                    newSprite = itemRepo.PurpleCubeA;
                    break;
                case MatchType.Pink:
                    newSprite = itemRepo.PinkCubeA;
                    break;
                default:
                    return;
            }
            UpdateSprite(newSprite);
        }else if (matchedCount > 7 && matchedCount < 10) //B
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = itemRepo.GreenCubeB;
                    break;
                case MatchType.Yellow:
                    newSprite = itemRepo.YellowCubeB;
                    break;
                case MatchType.Blue:
                    newSprite = itemRepo.BlueCubeB;
                    break;
                case MatchType.Red:
                    newSprite = itemRepo.RedCubeB;
                    break;
                case MatchType.Purple:
                    newSprite = itemRepo.PurpleCubeB;
                    break;
                case MatchType.Pink:
                    newSprite = itemRepo.PinkCubeB;
                    break;
                default:
                    return;
            }
            UpdateSprite(newSprite);
        }else if (matchedCount > 9) //C
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = itemRepo.GreenCubeC;
                    break;
                case MatchType.Yellow:
                    newSprite = itemRepo.YellowCubeC;
                    break;
                case MatchType.Blue:
                    newSprite = itemRepo.BlueCubeC;
                    break;
                case MatchType.Red:
                    newSprite = itemRepo.RedCubeC;
                    break;
                case MatchType.Purple:
                    newSprite = itemRepo.PurpleCubeC;
                    break;
                case MatchType.Pink:
                    newSprite = itemRepo.PinkCubeC;
                    break;
                default:
                    return;
            }
            UpdateSprite(newSprite);
        }
        else
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = itemRepo.GreenCubeDefault;
                    break;
                case MatchType.Yellow:
                    newSprite = itemRepo.YellowCubeDefault;
                    break;
                case MatchType.Blue:
                    newSprite = itemRepo.BlueCubeDefault;
                    break;
                case MatchType.Red:
                    newSprite = itemRepo.RedCubeDefault;
                    break;
                case MatchType.Purple:
                    newSprite = itemRepo.PurpleCubeDefault;
                    break;
                case MatchType.Pink:
                    newSprite = itemRepo.PinkCubeDefault;
                    break;
                default:
                    return;
            }
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
