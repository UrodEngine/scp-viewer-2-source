// Скрипт для автоматического создания и размещения кнопок для выбора игровой сцены.
// Основан на массиве класса <ButtonISO>. Имеет три входных параметра.
// MapName      - Декоративное имя кнопки. Не имеет смысла кроме как визуальное обозначение кнопки.
// MapFileName  - Название самой игровой сцены.
// MapIcon      - Картинка уровня.

using UnityEngine;
using Button    = UnityEngine.UI.Button;
using Image     = UnityEngine.UI.Image;
using Text      = UnityEngine.UI.Text;
public sealed class Maps_manager : MonoBehaviour
{
    public MapButtonPrefab_instance[]   ButtonsISO;
    public Map_select                   SCRIPT_ref;
    public GameObject                   buttonPrefab;
    public float                        DistanceMultiplier;

    private void Start                  ()                  
    {
        for (int i = 0; i < ButtonsISO.Length; i++)
        {
            GameObject MapButton = Instantiate(buttonPrefab, transform); //Создается кнопка на основе GameObject ссылки
            MapButton.transform.position += new Vector3(DistanceMultiplier, 0, 0)*i;

            MapButton.transform.GetChild(2).GetComponent<Text>().text    = ButtonsISO[i].MapName;
            MapButton.transform.GetChild(0).GetComponent<Image>().sprite = ButtonsISO[i].MapIcon;

            string tempName = ButtonsISO[i].MapFileName;
            MapButton.GetComponent<Button>().onClick.AddListener(()=> { SelectMapListener(tempName); });

            continue;
        }
        GameObject.Destroy(buttonPrefab);
    }
    private void SelectMapListener      (in string name)    
    {
        SCRIPT_ref.SelectMap(name);
    }
}

[System.Serializable]
public class MapButtonPrefab_instance
{
    public string   MapName;
    public string   MapFileName;
    public Sprite   MapIcon;
}