using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxActor : Actor
{
    [System.Serializable]
    public class ParalaxLayer
    {
        public Transform left, right;
        public float width;
        public float speed;
        protected int current = 0;

        public void Shift()
        {
            Vector3 velocity = Vector3.left * speed*Time.deltaTime;
            right.localPosition = right.localPosition + velocity;
            left.localPosition = left.localPosition + velocity;
            if (current == 0 && left.localPosition.x <= 0.0f)
            {
                right.localPosition = left.localPosition + Vector3.right * width;
                current = 1;
            }
            else
            {
                if (current == 1 && right.localPosition.x <= 0.0f)
                {
                    left.localPosition = right.localPosition + Vector3.right * width;
                    current = 0;
                }
                   
            }
        }
        public void SetDefault()
        {
            current = 0;
            left.localPosition = new Vector3(0.0f, left.localPosition.y, 0.0f);
            right.localPosition = left.localPosition + Vector3.right * width;

        }
    }

    [SerializeField]
    protected ParalaxLayer[] layers;

    public void SetDefault()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].SetDefault();
        }
    }
    protected override void OnLateUpdate()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].Shift();
        }
    }

    private void LateUpdate()
    {   
        OnLateUpdate();
    }
}
