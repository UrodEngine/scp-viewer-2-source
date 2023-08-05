using UnityEngine;
using UnityEngine.UI;

public class SkillsList : MonoBehaviour
{
    byte content_size => (byte) transform.childCount;

    private void Start()
    {
        for (byte i = 0; i < content_size; i++)
        {
            ButtonsTaskCreate(transform.GetChild(i).GetComponent<Button>(),i);
        }
    }
    private void LateUpdate()
    {
        for (byte i = 0; i < content_size; i++)
        {
            transform.GetChild(i).transform.gameObject.SetActive(false);
        }
        SetListCount();
    }
    private void SetListCount()
    {
        if (CamMove_v2.instance.SavedGameObject == null) return;
        byte content_current_size = 0;
        if (CamMove_v2.instance.SavedGameObject.TryGetComponent<ISCPSkillRequest>(out ISCPSkillRequest skillData))
        {
            content_current_size = (byte)skillData.skills.Length;
        }
        for (byte i = 0; i < content_current_size; i++)
        {
            transform.GetChild(i).transform.gameObject.SetActive(true);
            string available    = skillData.skills[i].isReload == true? "Reloading" : "Available";
            Text text           = transform.GetChild(i).GetComponentInChildren<Text>();
            text.text           = $"{skillData.skills[i].name} \n {available}";
            text.color          = skillData.skills[i].isReload == true ? new Color(1, 0, 0, 1) : new Color(1, 1, 1, 1);
        }
    }
    private void ButtonsTaskCreate(Button button,byte id)
    {
        button.onClick.AddListener(delegate {
            CamMove_v2.instance.SavedGameObject.TryGetComponent<ISCPSkillRequest>(out ISCPSkillRequest skillData);
            skillData.skills[id].UseSkill();
        });
    }
}
