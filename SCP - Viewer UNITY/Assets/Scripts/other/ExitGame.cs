using UnityEngine;

public class ExitGame : MonoBehaviour
{
public void ExitGameMethod() {
        Invoke("temp", 1.2f);
    }

    void temp(){
        Debug.Log("Вышел из игры");
        
        Application.Quit();
    }
}
