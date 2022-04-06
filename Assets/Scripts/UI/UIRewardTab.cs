using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRewardTab : Actor
{
    [SerializeField]
    protected SpriteRenderer icon;
    [SerializeField]
    protected Vector3 startPos, endPos;
    [SerializeField]
    protected float lerpOffset,waitTime;
    public bool direction;
    protected float nextWait;
    protected Vector3 posOffset;
    public void SetIcon(StuffSkinScriptableObject skin)
    {
        icon.sprite = skin.Sprites[0];
    }
    protected override void OnStart()
    {
        posOffset = transform.localPosition;
    }
    public void PlayAnimation()
    {
        direction = true;
        enabled = true;
    }
    protected override void OnLateUpdate()
    {
        if (direction)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, endPos + posOffset, lerpOffset * Time.deltaTime);
            if ((transform.localPosition - (endPos + posOffset)).sqrMagnitude <= 0.001f)
            {
                transform.localPosition = endPos + posOffset;
                nextWait = Time.time + waitTime;
                direction = false;
            }
        }
        else
        {
            if(Time.time >= nextWait)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, startPos + posOffset, lerpOffset * Time.deltaTime);
                if((transform.localPosition - (startPos + posOffset)).sqrMagnitude <= 0.001f)
                {
                    transform.localPosition = startPos + posOffset;
                    enabled = false;
                }
            }
        }
    }
    private void Start()
    {
        OnStart();
    }
    private void LateUpdate()
    {
        OnLateUpdate();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition + endPos, 0.25f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.localPosition + startPos,0.25f);
    }
}
