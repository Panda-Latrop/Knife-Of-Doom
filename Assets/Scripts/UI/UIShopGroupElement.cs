using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIShopGroupElement : MonoBehaviour, IPointerClickHandler
{
    public delegate void ElementSelectDelegate(StuffSkinScriptableObject skin);
    [SerializeField]
    protected RectTransform gridRectTransform;
    [SerializeField]
    protected GridLayoutGroup grid;
    [SerializeField]
    protected List<Image> icons;
    protected List<StuffSkinInfo> skins = new List<StuffSkinInfo>();
    protected ElementSelectDelegate OnSelect;

    protected Vector2 size, cellSize,cellCount;
    protected Vector3 offset;
    public bool HasSkins => skins.Count > 0;
    protected void Awake()
    {
        cellSize = grid.cellSize + grid.spacing;
        size = gridRectTransform.rect.size;
        cellCount.x = Mathf.RoundToInt(size.x / cellSize.x);
        cellCount.y = Mathf.RoundToInt(size.y / cellSize.y);
        var r = Screen.currentResolution;
        offset =  gridRectTransform.rect.position- new Vector2(r.width, r.height);
    }
    [ContextMenu("EDITOR Autoset")]
    protected void EDITOR_AutoSet()
    {
        icons.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            icons.Add(transform.GetChild(i).GetChild(0).GetComponent<Image>());
        }
    }
    public void SetSkins(List<StuffSkinInfo> skins)
    {
        this.skins = skins;
        int i;
        for (i = 0; i < skins.Count && i < icons.Count; i++)
        {
            icons[i].sprite = skins[i].so.Sprites[0];
            icons[i].transform.parent.gameObject.SetActive(true);
            if (skins[i].open || skins[i].so.Open)
                icons[i].color = Color.white;
            else
                icons[i].color = Color.black;
        }
        for (int j = i; j < icons.Count; j++)
        {
            icons[j].transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      int id = NodeFromWorld(gridRectTransform.InverseTransformPoint(eventData.position));
        if (id >= 0)
            CallOnSelect(skins[id].so);
    }
    public int NodeFromWorld(Vector3 position)
    {

        Vector3 halfNodeSize = new Vector3(-cellSize.x / 2.0f, cellSize.y / 2.0f,0.0f);
        Vector3 gridSize = new Vector3(cellCount.x * cellSize.x, -cellCount.y * cellSize.x, 0.0f);

        Vector3 posShift = position + gridSize / 2.0f + halfNodeSize;
        
        int x = Mathf.RoundToInt(posShift.x / cellSize.x);
        int y = Mathf.RoundToInt(posShift.y / cellSize.x);
        int id = (int)(x + y * cellCount.x);
        Vector3 f = gridSize / 2.0f;
        f.y *= -1.0f;
        //Debug.Log(posShift + " " + id);// ;- gridRectTransform.position);
        //Debug.Log(position + " " + posShift + " " + id);
        if (x >= 0 && x < cellCount.x && y >= 0 && y < cellCount.y && id < icons.Count)
        {
            return id;
        }
        else
        {
            return -1;
        }
    }
    public void CallOnSelect(StuffSkinScriptableObject skin)
    {
        OnSelect?.Invoke(skin);
    }
    public void BindOnSelect(ElementSelectDelegate action)
    {
        OnSelect += action;
    }
    public void UnbindOnSelect(ElementSelectDelegate action)
    {
        OnSelect -= action;
    }
    public void ClearOnSelect()
    {
        OnSelect = null;
    }
    private void OnDestroy()
    {
        OnSelect = null;
    }
}
