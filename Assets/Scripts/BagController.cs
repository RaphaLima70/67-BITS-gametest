using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BagController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> positions = new List<Transform>();
    [SerializeField]
    private List<GameObject> deadEnemies = new List<GameObject>();
    [SerializeField]
    private GameObject bagPosition, newPosition;
    [SerializeField]
    private GameObject positionPrefab;
    [SerializeField]
    private int listPivot;

    public void CreateDeadEnemy(GameObject deadEnemyPrefab)
    {
        if (positions.Count <= 0)
        {
            newPosition = Instantiate(positionPrefab, new Vector3(transform.position.x,
            transform.position.y, transform.position.z), Quaternion.identity);
            positions.Add(newPosition.transform);
            newPosition.GetComponent<DeadEnemyController>().SetDeadTargetPosition(bagPosition.transform);
        }
        else
        {
            newPosition = Instantiate(positionPrefab, new Vector3(positions[listPivot - 1].transform.position.x,
             positions[listPivot - 1].transform.position.y + 0.4f, positions[listPivot - 1].transform.position.z), Quaternion.identity);
            positions.Add(newPosition.transform);
            newPosition.GetComponent<DeadEnemyController>().SetDeadTargetPosition(positions[listPivot - 1]);
        }
        GameObject clone = Instantiate(deadEnemyPrefab);
        deadEnemies.Add(clone);
        clone.transform.SetParent(positions[listPivot]);
        clone.transform.position = positions[listPivot].position;
        GameManager.Instance.AddDeadEnemy();
        listPivot++;
    }
    public void ClearBag()
    {
        if (positions.Count > 0)
        {
            foreach (Transform pos in positions)
            {
                Destroy(pos.gameObject);
            }
            foreach (GameObject enemy in deadEnemies)
            {
                Destroy(enemy);
            }
            positions.Clear();
            deadEnemies.Clear();
            listPivot = 0;
            GameManager.Instance.ResetEnemiesCounter();
        }

    }
    public bool CanReceiveEnemy()
    {
        return positions.Count < GameManager.Instance.GetCurrentCapacity();
    }
    public int EnemyCounter()
    {
        return positions.Count;
    }
}
