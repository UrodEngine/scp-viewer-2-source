using UnityEngine;
using UnityEngine.UI;

public sealed class LastObjectsList : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private readonly SimpleDelayer delayer = new SimpleDelayer(100);

    private  void Update(){

        delayer.Move();
        if (delayer.OnElapsed())
        {
            if (SPAWN_lists.instance.lastSelectedList.ToArray().Length <= 0 || SPAWN_lists.instance.lastSelectedList.ToArray().Length > 6)
            {
                return;
            }
            for (int i = 0; i < SPAWN_lists.instance.lastSelectedList.ToArray().Length; i++)
            {
                buttons[i].transform.GetChild(0).GetComponent<Text>().text = SPAWN_lists.instance.lastSelectedList[i] != null ? SPAWN_lists.instance.lastSelectedList[i].name : "NULL";
            }
        }
    }
}
