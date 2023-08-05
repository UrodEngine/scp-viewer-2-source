using UnityEngine;
using UnityEngine.UI;

public class CKpanel_1 : MonoBehaviour
{
    public CKtransform  keyCardTransform;
    public Scrollbar    scrollbar_x, scrollbar_y, scrollbar_z;
    
    void Start()
    {
        scrollbar_x.onValueChanged.AddListener(delegate { keyCardTransform.xrot = scrollbar_x.value * 360; ; });
        scrollbar_y.onValueChanged.AddListener(delegate { keyCardTransform.yrot = scrollbar_y.value * 360; ; });
        scrollbar_z.onValueChanged.AddListener(delegate { keyCardTransform.zrot = scrollbar_z.value * 360; ; });
    }

}
