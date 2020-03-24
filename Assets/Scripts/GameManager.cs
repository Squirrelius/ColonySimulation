using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject _PlantingBoxPrefab;
    public Slider _speedSlider;
    public LayerMask _blockingBuildLayerMask;

    private Storage _storage;

    public void OnGameSpeedChanged()
    {
        Time.timeScale = _speedSlider.value;
    }

    private void Awake()
    {
        _storage = FindObjectOfType<Storage>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 999, 1<<LayerMask.NameToLayer("Ground")))
            {
                Vector3 clickPos = rayHit.point;
                Collider[] collidingObjects = Physics.OverlapSphere(clickPos, 1, _blockingBuildLayerMask);

                if (collidingObjects.Length > 0)
                    Util.PrintMessageToScreen("Position blocked");
                else if (_storage._Wood <= 0)
                    Util.PrintMessageToScreen("Missing Resources: 1 Wood");
                else
                {
                    _storage._Wood--;
                    Instantiate(_PlantingBoxPrefab, clickPos, Quaternion.identity);
                }
            }
        }
    }
}
