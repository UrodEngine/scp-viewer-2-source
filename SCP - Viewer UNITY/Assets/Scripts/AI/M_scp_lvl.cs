// Этот скрипт вешается на монстра, который при появлении
// сразу же загружает уровень.
//
// Монстры с этим скриптом по факту являются уровнями.

using UnityEngine;
[AddComponentMenu("SCP Tools/SCP map loader")]
public class M_scp_lvl : MonoBehaviour
{
    public string M_SCP_level_name;
    void Start(){
        GetComponent<LoadLevel>().LevelLoading(M_SCP_level_name);
    }
}
