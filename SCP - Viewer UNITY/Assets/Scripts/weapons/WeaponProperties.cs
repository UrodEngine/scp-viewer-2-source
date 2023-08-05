using UnityEngine;

[System.Serializable]
public class WeaponProperties : MonoBehaviour
{
    /// <summary> ������� �������� ����������� </summary>
    public short        currentReload;
    /// <summary> �������� ��������. <see cref="currentReload"/> ����� ��������� �������� ����� ���� </summary>
    public short        shootDelay;
    /// <summary> ���� ������ ���� </summary>
    public ushort       damage;
    /// <summary> ���������� ���� ��� �������� </summary>
    public ushort       bulletsPerFire;
    /// <summary> ������. ������������ � �� �������������� �������� ����� ����� �������� True </summary>
    public bool         shooted;
    /// <summary> ������ ���� � ����������� <see cref="BulletAI"/> </summary>
    public GameObject   bulletInstance;
    /// <summary> ����� ���������� ����</summary>
    public Vector3      shootTargetPosition = new Vector3(0.666f, 0.666f, 0.666f);

    public void Shoot(in GameObject bullet, in GameObject startPositionObject){
        for (ushort i = 0; i < bulletsPerFire; i++){

            GameObject spawnedBullet = GameObject.Instantiate(bullet, startPositionObject.transform.position + startPositionObject.transform.forward * 1.2f, Quaternion.identity);

            spawnedBullet.transform.rotation = startPositionObject.transform.rotation;
            spawnedBullet.transform.Rotate(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4));
            spawnedBullet.GetComponent<BulletAI>().damage = damage;
            spawnedBullet.GetComponent<BulletAI>().onMoved();
            GameObject.Destroy(spawnedBullet, 2f);
        }      
    }
    public void Shoot(in GameObject bullet, in GameObject startPositionObject, in Vector3 ShootTo)
    {
        for (ushort i = 0; i < bulletsPerFire; i++)
        {
            GameObject spawnedBullet = GameObject.Instantiate(bullet, startPositionObject.transform.position + startPositionObject.transform.forward * 1.2f, Quaternion.identity);

            spawnedBullet.transform.LookAt(ShootTo);
            spawnedBullet.transform.Rotate(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4));
            spawnedBullet.GetComponent<BulletAI>().damage = damage;
            spawnedBullet.GetComponent<BulletAI>().onMoved();
            GameObject.Destroy(spawnedBullet, 2f);
        }
    }
}