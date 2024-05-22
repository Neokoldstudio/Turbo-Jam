using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class VfxManager : SerializedMonoBehaviour
{
    public Dictionary<VfxType, Vfx[]> usedVfx;

    public List<GameObject> currentVfx;

    public int TriggerVfx(VfxType vfxEnum)
    {
        Debug.Log("Trying to vfx type : " + vfxEnum.ToString());
        if (usedVfx.ContainsKey(vfxEnum))
        {
            Vfx vfxToTrigger = usedVfx[vfxEnum][Random.Range(0, usedVfx[vfxEnum].Length)];
            GameObject vfx = Instantiate(vfxToTrigger.gO,(vfxToTrigger.parent!=null)?vfxToTrigger.parent:null);
            vfx.transform.SetParent(null);
            currentVfx.Add(vfx);
            return currentVfx.IndexOf(vfx);
        }
        return -1;
    }
}

[System.Serializable]
public class Vfx
{
    public GameObject gO;
    public bool hasParent;
    [ShowIf("hasParent")]
    public Transform parent;
}

public enum VfxType
{
    Swing,
    Slash,
    Impact,
    CloudTrail
}
