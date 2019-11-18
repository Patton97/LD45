using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] List<Transform> transforms;
    void Start()
    {
        transforms = ShuffleList(transforms);
        SpawnObjects();
    }

    List<T> ShuffleList<T>(List<T> list)
    {
        List<T> temp = new List<T>();

        while (list.Count > 0)
        {
            int random = Random.Range(0, list.Count);
            temp.Add(list[random]);
            list.RemoveAt(random);
        }

        return temp;
    }

    void SpawnObjects()
    {
        //Assumes objects & transforms are of same length
        for(int i = 0; i < objects.Length; i++)
        {
            Instantiate(objects[i], transforms[i]);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach(Transform t in transforms)
        {
            Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, transform.rotation, Vector3.one) * Matrix4x4.Rotate(t.rotation);
            Gizmos.DrawWireCube(t.position, t.lossyScale * .1f);
        }
    }
}
