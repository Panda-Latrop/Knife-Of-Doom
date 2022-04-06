using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSkinComponent :  ActorComponent
{
    [SerializeField]
    protected SpriteRenderer[] renderers;

    public void SetSkin(StuffSkinScriptableObject skin)
    {
        var sprites = skin.Sprites;
        for (int i = 0; i < renderers.Length && i < sprites.Length; i++)
        {
            renderers[i].sprite = sprites[i];
        }
    }
}
