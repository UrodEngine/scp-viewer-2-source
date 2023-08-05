using UnityEngine;
using UnityEngine.UI;

public class CKpanel_2 : MonoBehaviour
{
    public Scrollbar    scrollbar_r, scrollbar_g, scrollbar_b;
    public Scrollbar    scrollbar_r0, scrollbar_g0, scrollbar_b0;
    public Material     material, material_2;
    public Additional[]   additional;
    void Start()
    {
        scrollbar_r.onValueChanged.AddListener  (delegate { material.color = new Color(scrollbar_r.value, scrollbar_g.value, scrollbar_b.value, 1); });
        scrollbar_g.onValueChanged.AddListener  (delegate { material.color = new Color(scrollbar_r.value, scrollbar_g.value, scrollbar_b.value, 1); });
        scrollbar_b.onValueChanged.AddListener  (delegate { material.color = new Color(scrollbar_r.value, scrollbar_g.value, scrollbar_b.value, 1); });

        scrollbar_r0.onValueChanged.AddListener (delegate { material_2.color = new Color(scrollbar_r0.value, scrollbar_g0.value, scrollbar_b0.value, 1); });
        scrollbar_g0.onValueChanged.AddListener (delegate { material_2.color = new Color(scrollbar_r0.value, scrollbar_g0.value, scrollbar_b0.value, 1); });
        scrollbar_b0.onValueChanged.AddListener (delegate { material_2.color = new Color(scrollbar_r0.value, scrollbar_g0.value, scrollbar_b0.value, 1); });

        foreach (var current in additional){
            current.Activate();
        }
    }



    [System.Serializable]
    public class Additional
    {
        public Scrollbar    scrollbar_r2, scrollbar_g2, scrollbar_b2;
        public Text[]       texts;
        public RawImage[]   images;

        public void Activate()
        {
            scrollbar_r2.onValueChanged.AddListener(delegate {
                foreach (var item in texts)
                {
                    item.color = new Color(scrollbar_r2.value, scrollbar_g2.value, scrollbar_b2.value, 1);
                }
                foreach (var item in images)
                {
                    item.color = new Color(scrollbar_r2.value, scrollbar_g2.value, scrollbar_b2.value, 1);
                }
            });
            scrollbar_g2.onValueChanged.AddListener(delegate {
                foreach (var item in texts)
                {
                    item.color = new Color(scrollbar_r2.value, scrollbar_g2.value, scrollbar_b2.value, 1);
                }
                foreach (var item in images)
                {
                    item.color = new Color(scrollbar_r2.value, scrollbar_g2.value, scrollbar_b2.value, 1);
                }

            });
            scrollbar_b2.onValueChanged.AddListener(delegate {
                foreach (var item in texts)
                {
                    item.color = new Color(scrollbar_r2.value, scrollbar_g2.value, scrollbar_b2.value, 1);
                }
                foreach (var item in images)
                {
                    item.color = new Color(scrollbar_r2.value, scrollbar_g2.value, scrollbar_b2.value, 1);
                }
            });
        }
    }
}
