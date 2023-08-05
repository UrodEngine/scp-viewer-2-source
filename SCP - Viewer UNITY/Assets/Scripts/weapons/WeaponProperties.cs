using UnityEngine;

[System.Serializable]
public class WeaponProperties : MonoBehaviour
{
    /// <summary> Текущее значение перезарядки </summary>
    public short        currentReload;
    /// <summary> Интервал стрельбы. <see cref="currentReload"/> часто принимает значение этого поля </summary>
    public short        shootDelay;
    /// <summary> Урон каждой пули </summary>
    public ushort       damage;
    /// <summary> Количество пуль при выстреле </summary>
    public ushort       bulletsPerFire;
    /// <summary> Статус. Выстрелившее и не перезаряженное оружение будет иметь значение True </summary>
    public bool         shooted;
    /// <summary> Префаб пули с компонентом <see cref="BulletAI"/> </summary>
    public GameObject   bulletInstance;
    /// <summary> Точка назначения пуль</summary>
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