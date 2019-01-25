using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPartsSpawn : MonoBehaviour
{
    public Transform car;
    public List<Transform> wheels = new List<Transform>();
    public Transform hat;
    public Transform beard;
    public Renderer character;
    public int num;

    public Camera myCam;
    public Camera camUI;

    public void SpawnItems(GameObject _car, GameObject _wheel, GameObject _hat, GameObject _beard, Material charMaterial, int _num)
    {
        Instantiate(_car, car);
        for (int i = 0; i < wheels.Count; i++)
        {
            Instantiate(_wheel, wheels[i]);
        }
        num = _num;
        CameraFollow nC = Instantiate(myCam.gameObject, gameObject.transform).GetComponent<CameraFollow>();
        nC.car = gameObject.transform;
        nC.playerNum = num;
        GetComponentInChildren<KartPhysics>().playerNum = num;
        GetComponentInChildren<Racer>().playerNum = num;
        nC.GetComponent<Camera>().rect = camUI.rect;
        
        nC.transform.parent = null;
        Instantiate(_hat, hat);
        Instantiate(_beard, beard);
        character.material = charMaterial;
        IGP_Manager.instance.allIGP.Add(GetComponentInChildren<IGP>());
    }
}
