using UnityEngine;
using VContainer;

public class NormalCubeItem : Item
{
    private MatchType _matchType;
    private ItemImageRepo _imageRepo;

    [Inject]
    void Construct(ItemImageRepo imageRepo)
    {
        _imageRepo = imageRepo;
    }

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
                return _imageRepo.GreenCubeDefault;
            case MatchType.Yellow:
                return _imageRepo.YellowCubeDefault;
            case MatchType.Blue:
                return _imageRepo.BlueCubeDefault;
            case MatchType.Red:
                return _imageRepo.RedCubeDefault;
            case MatchType.Pink:
                return _imageRepo.PinkCubeDefault; 
            case MatchType.Purple:
                return _imageRepo.PurpleCubeDefault;
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
                    newSprite = _imageRepo.GreenCubeA;
                    break;
                case MatchType.Yellow:
                    newSprite = _imageRepo.YellowCubeA;
                    break;
                case MatchType.Blue:
                    newSprite = _imageRepo.BlueCubeA;
                    break;
                case MatchType.Red:
                    newSprite = _imageRepo.RedCubeA;
                    break;
                case MatchType.Purple:
                    newSprite = _imageRepo.PurpleCubeA;
                    break;
                case MatchType.Pink:
                    newSprite = _imageRepo.PinkCubeA;
                    break;
                default:
                    return;
            }
        }else if (matchedCount > 7 && matchedCount < 10) //B
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = _imageRepo.GreenCubeB;
                    break;
                case MatchType.Yellow:
                    newSprite = _imageRepo.YellowCubeB;
                    break;
                case MatchType.Blue:
                    newSprite = _imageRepo.BlueCubeB;
                    break;
                case MatchType.Red:
                    newSprite = _imageRepo.RedCubeB;
                    break;
                case MatchType.Purple:
                    newSprite = _imageRepo.PurpleCubeB;
                    break;
                case MatchType.Pink:
                    newSprite = _imageRepo.PinkCubeB;
                    break;
                default:
                    return;
            }
        }else if (matchedCount > 9) //C
        {
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = _imageRepo.GreenCubeC;
                    break;
                case MatchType.Yellow:
                    newSprite = _imageRepo.YellowCubeC;
                    break;
                case MatchType.Blue:
                    newSprite = _imageRepo.BlueCubeC;
                    break;
                case MatchType.Red:
                    newSprite = _imageRepo.RedCubeC;
                    break;
                case MatchType.Purple:
                    newSprite = _imageRepo.PurpleCubeC;
                    break;
                case MatchType.Pink:
                    newSprite = _imageRepo.PinkCubeC;
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
