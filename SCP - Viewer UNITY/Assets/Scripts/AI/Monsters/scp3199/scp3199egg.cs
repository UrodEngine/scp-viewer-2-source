using UnityEngine;

[AddComponentMenu("SCP/3199-B - two leg's egg"), DisallowMultipleComponent]
public sealed class scp3199egg : MonoBehaviour
{
    [SerializeField] private bool canSpawn;
    [SerializeField] private GameObject whoSpawn;
    private void Start()
    {
        canSpawn = Random.Range(0, 100) > 70 ? true : false;
        StartCoroutine(Spawn());
    }
    private System.Collections.IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(10, 20));
        if (canSpawn)
        {
            GameObject scp3199s = Instantiate(whoSpawn, transform.position, whoSpawn.transform.rotation);
            scp3199s.GetComponent<scp3199>().murderConfigs.components.navMeshAgent.enabled = false;
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
