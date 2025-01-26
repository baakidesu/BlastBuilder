using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticlesController : Singleton<ParticlesController>
{
    public ParticleSystem redParticleSystem;
    public ParticleSystem greenParticleSystem;
    public ParticleSystem blueParticleSystem;
    public ParticleSystem pinkParticleSystem;
    public ParticleSystem purpleParticleSystem;
    public ParticleSystem yellowParticleSystem;
    public void PlayParticleSystem(Item item)
    {
        ParticleSystem particeSystem = null;

        switch (item.itemType)
        {
            case ItemType.RedCube:
                particeSystem = redParticleSystem;
                break;
            case ItemType.GreenCube:
                particeSystem = greenParticleSystem;
                break;
            case ItemType.YellowCube:
                particeSystem = yellowParticleSystem;
                break;
            case ItemType.PinkCube:
                particeSystem = pinkParticleSystem;
                break;
            case ItemType.PurpleCube:
                particeSystem = purpleParticleSystem;
                break;
            case ItemType.BlueCube:
                particeSystem = blueParticleSystem;
                break;
            default:
                return;
        }
        Vector3 pos = new Vector3(item.transform.position.x, item.transform.position.y, 100);
        var particle = Instantiate(particeSystem, pos, Quaternion.identity, item.Cell.gameGrid.particlesParent);
        particle.Play();
    }
}
