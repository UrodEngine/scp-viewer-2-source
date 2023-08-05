using UnityEngine;

public class PlayerPrefsIndicator2 : MonoBehaviour
{
    [SerializeField] private RectTransform  rect;
    [SerializeField] private string         PlayerPrefsName;
    [SerializeField] private float          speed = 0.1f;
    [SerializeField] private Node[]         nodes;
    void FixedUpdate()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            switch (nodes[i].type)
            {
                case Node.NodeType.INTEGER:
                    if (nodes[i].intKey == PlayerPrefs.GetInt(PlayerPrefsName))
                    {
                        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, nodes[i].position, speed);
                    }
                    break;
                case Node.NodeType.FLOAT:
                    if (nodes[i].floatKey == PlayerPrefs.GetFloat(PlayerPrefsName))
                    {
                        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, nodes[i].position, speed);
                    }
                    break;
                case Node.NodeType.STRING:
                    if (nodes[i].stringKey == PlayerPrefs.GetString(PlayerPrefsName))
                    {
                        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition,nodes[i].position, speed);
                    }
                    break;
                default:
                    return;
            }
        }

        //rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, posByPlayerPrefsValue[PlayerPrefs.GetInt(PlayerPrefsName)], speed);
    }
    [System.Serializable]
    public class Node
    {
        public enum NodeType { INTEGER, FLOAT, STRING}

        public NodeType type;

        public int      intKey;
        public string   stringKey;
        public float    floatKey;
        public Vector2  position;
    }
}
