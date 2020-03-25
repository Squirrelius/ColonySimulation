using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Vector2 _levelSize;
    public Transform _groundPlane;
    public GameObject _bushPrefab;
    public GameObject _treePrefab;
    public GameObject _chickenPrefab;
    public LayerMask _blockingLayerMask;

    public int _initialChickenAmt = 10;
    public int _treesPerMinute = 5;
    public int _bushesPerMinute = 7;

    public int _maxTrees = 20;
    public int _maxBushes = 80;

    private List<Tree> _treeList;
    private List<Bush> _bushList;

    private float _timer = 60;

    void Awake()
    {
        _groundPlane.localScale = new Vector3(_levelSize.x/10, 1, _levelSize.y/10);
        _treeList = new List<Tree>();
        _bushList = new List<Bush>();
    }

    void Start()
    {
        for(int i=0; i<_initialChickenAmt; i++)
            SpawnAtRandomLocation(_chickenPrefab);

        for (int i = 0; i < _maxTrees; i++)
        {
            if (_treeList.Count < _maxTrees)
            {
                Tree newTree = SpawnAtRandomLocation(_treePrefab).GetComponent<Tree>();
                newTree.onDie += OnTreeDied;
                _treeList.Add(newTree);
            }
        }

        for (int i = 0; i < _maxBushes / 2; i++)
        {
            if (_bushList.Count < _maxBushes)
            {
                Bush newBush = SpawnAtRandomLocation(_bushPrefab).GetComponent<Bush>();
                newBush.onDie += OnBushDied;
                _bushList.Add(newBush);
            }
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 30)
        {
            _timer = 0;
            for (int i = 0; i < _treesPerMinute/2; i++)
            {
                if (_treeList.Count < _maxTrees)
                {
                    Tree newTree = SpawnAtRandomLocation(_treePrefab).GetComponent<Tree>();
                    newTree.onDie += OnTreeDied;
                    _treeList.Add(newTree);
                }
            }

            for (int i = 0; i < _bushesPerMinute/2; i++)
            {
                if (_bushList.Count < _maxBushes)
                {
                    Bush newBush = SpawnAtRandomLocation(_bushPrefab).GetComponent<Bush>();
                    newBush.onDie += OnBushDied;
                    _bushList.Add(newBush);
                }
            }
        }
    }

    private GameObject SpawnAtRandomLocation(GameObject prefab)
    {
        Collider coll = prefab.GetComponent<Collider>();
        float radius = coll.bounds.extents.x > coll.bounds.extents.z ? coll.bounds.extents.x : coll.bounds.extents.z;

        for (int i = 0; i < 20; i++)
        {
            Vector3 rndPos = new Vector3(Random.Range(-_levelSize.x, _levelSize.x) / 2f, 0, Random.Range(-_levelSize.y, _levelSize.y) / 2f);
            Collider[] colliders = Physics.OverlapSphere(rndPos + coll.bounds.center, radius, _blockingLayerMask);
            if (colliders.Length == 0)
            {
                return Instantiate(prefab, rndPos, Quaternion.identity);
            }
        }
        Debug.LogWarning("Failed to spawn " + prefab.name);
        return null;
    }

    private void OnBushDied(Bush sender)
    {
        sender.onDie -= OnBushDied;
        _bushList.Remove(sender);
    }

    private void OnTreeDied(Tree sender)
    {
        sender.onDie -= OnTreeDied;
        _treeList.Remove(sender);
    }
}
