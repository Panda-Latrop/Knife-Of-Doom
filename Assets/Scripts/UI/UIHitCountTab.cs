using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHitCountTab : Actor
{
    [SerializeField]
    protected List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    protected int maxCount;
    protected Color activeColor = new Color(1.0f, 1.0f, 1.0f, 0.5f), deactiveColor = new Color(0.0f, 0.0f, 0.0f, 0.5f);

    public void SetMaxHit(int max)
    {
        maxCount = max;
        if(renderers.Count > maxCount)
        {
            for (int i = 0; i < maxCount; i++)
            {
                renderers[i].color = activeColor;
                renderers[i].enabled = true;
            }
            for (int i = maxCount; i < renderers.Count; i++)
            {
                renderers[i].enabled = false;
            }
        }
    }
    public void SetHit(int hit)
    {
        for (int i = maxCount - 1-hit; i < maxCount && i >= 0; i++)
        {
            renderers[i].color = deactiveColor;
        }
    }
}
