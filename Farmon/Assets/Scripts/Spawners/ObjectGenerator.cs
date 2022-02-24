using GrassMan.Combat;
using GrassMan.Pools;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] GameObject rootPrefab;
    List<Grass> grassCounter = new List<Grass>();
    Grass grass;

    [Header("Spawn Start Points")]
    public Transform GrassStartPosition;
    public Transform RootStartPosition;

    [Header("Spawn Size")]
    public int NumberOfRows;
    public int ObjectsPerRow;
    public float Spacing;
   
    float _activeGrass;
    float _totalGrass;
  
    void Start()
    {       
        for (int row = 0; row < NumberOfRows; row++)
        {
            for (int col = 0; col < ObjectsPerRow; col++)
            {
                Vector3 startingPos = new Vector3(GrassStartPosition.position.x + col * Spacing, GrassStartPosition.position.y, GrassStartPosition.position.z - row * Spacing);
                grass = GrassPool.Instance.Get();
                grass.transform.position = startingPos;
                grass.transform.rotation = Quaternion.identity;
                grass.gameObject.SetActive(true);

                Vector3 rootStartingPos = new Vector3(RootStartPosition.position.x + col * Spacing, RootStartPosition.position.y, RootStartPosition.position.z - row * Spacing);
                GameObject root = Instantiate(rootPrefab, rootStartingPos, Quaternion.identity);
                root.transform.parent = transform.parent;
                if(grass.gameObject.activeInHierarchy)
                {
                    grassCounter.Add(grass);
                }              
                _totalGrass = grassCounter.Count;
                _activeGrass = _totalGrass;

            }
        }
    }

    private void OnEnable()
    {
        Grass.OnTakeHit += TakeDamage;
    }
    private void OnDisable()
    {
        Grass.OnTakeHit -= TakeDamage;
    }

    private void TakeDamage()
    {
        _activeGrass = _totalGrass;
        foreach (Grass grass in grassCounter)
        {
            if(!grass.gameObject.activeInHierarchy)
            {
                _activeGrass--;               
            }
        }
        if(_totalGrass / 2 >= _activeGrass)
        {
            ActiveGrasses();
            
         
        }       
    }

    private void ActiveGrasses()
    {

        for (int i = 0; i < _totalGrass - _activeGrass; i++)
        {
            Grass grass = GrassPool.Instance.Get();
            grass.gameObject.SetActive(true);
            grass.enabled = true;
         
        }      
    }
  
}


    
