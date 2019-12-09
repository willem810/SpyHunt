using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public static BulletManager Instance;
    //public GameObject BulletPrefab;
    //List<GameObject> bulletPool = new List<GameObject>();
    Dictionary<int, List<IBullet>> BulletPool = new Dictionary<int, List<IBullet>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IBullet Spawn(Vector3 position, Quaternion rotation, IBullet prefab)
    {
        int key = prefab.GetHashCode();
        bool hasKey = BulletPool.ContainsKey(key);
        List<IBullet> bullets = new List<IBullet>();

        if (hasKey)
            bullets = BulletPool[key]?.Where(b => b.gameObject.activeSelf == false).ToList();
        else
            BulletPool.Add(key, bullets);



        if (!hasKey || bullets.Count() == 0)
        {
            GameObject newBulletObject = Instantiate(prefab.gameObject, position, rotation);

            IBullet newBullet = newBulletObject.GetComponent<IBullet>();
            BulletPool[key].Add(newBullet);
            return newBullet;
        }

        IBullet bullet;

        bullet = bullets.First();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        return bullet;

    }
}
