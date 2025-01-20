using UnityEngine;
using VContainer;

public class NormalCubeItem : Item
{
    private MatchType _matchType;
    private ItemImageRepo _itemImageRepo = ItemImageRepo.Instance;
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
                return _itemImageRepo.GreenCubeDefault;
            case MatchType.Yellow:
                return _itemImageRepo.YellowCubeDefault;
            case MatchType.Blue:
                return _itemImageRepo.BlueCubeDefault;
            case MatchType.Red:
                return _itemImageRepo.RedCubeDefault;
            case MatchType.Pink:
                return _itemImageRepo.PinkCubeDefault; 
            case MatchType.Purple:
                return _itemImageRepo.PurpleCubeDefault;
        }
        return null;
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
            switch (_matchType)
            {
                case MatchType.Green:
                    newSprite = _itemImageRepo.GreenCubeA;
                    break;
                case MatchType.Yellow:
                    newSprite = _itemImageRepo.YellowCubeA;
                    break;
                case MatchType.Blue:
                    newSprite = _itemImageRepo.BlueCubeA;
                    break;
                case MatchType.Red:
                    newSprite = _itemImageRepo.RedCubeA;
                    break;
                case MatchType.Purple:
                    newSprite = _itemImageRepo.PurpleCubeA;
                    break;
                case MatchType.Pink:
                    newSprite = _itemImageRepo.PinkCubeA;
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
                    newSprite = _itemImageRepo.GreenCubeB;
                    break;
                case MatchType.Yellow:
                    newSprite = _itemImageRepo.YellowCubeB;
                    break;
                case MatchType.Blue:
                    newSprite = _itemImageRepo.BlueCubeB;
                    break;
                case MatchType.Red:
                    newSprite = _itemImageRepo.RedCubeB;
                    break;
                case MatchType.Purple:
                    newSprite = _itemImageRepo.PurpleCubeB;
                    break;
                case MatchType.Pink:
                    newSprite = _itemImageRepo.PinkCubeB;
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
                    newSprite = _itemImageRepo.GreenCubeC;
                    break;
                case MatchType.Yellow:
                    newSprite = _itemImageRepo.YellowCubeC;
                    break;
                case MatchType.Blue:
                    newSprite = _itemImageRepo.BlueCubeC;
                    break;
                case MatchType.Red:
                    newSprite = _itemImageRepo.RedCubeC;
                    break;
                case MatchType.Purple:
                    newSprite = _itemImageRepo.PurpleCubeC;
                    break;
                case MatchType.Pink:
                    newSprite = _itemImageRepo.PinkCubeC;
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
                    newSprite = _itemImageRepo.GreenCubeDefault;
                    break;
                case MatchType.Yellow:
                    newSprite = _itemImageRepo.YellowCubeDefault;
                    break;
                case MatchType.Blue:
                    newSprite = _itemImageRepo.BlueCubeDefault;
                    break;
                case MatchType.Red:
                    newSprite = _itemImageRepo.RedCubeDefault;
                    break;
                case MatchType.Purple:
                    newSprite = _itemImageRepo.PurpleCubeDefault;
                    break;
                case MatchType.Pink:
                    newSprite = _itemImageRepo.PinkCubeDefault;
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
