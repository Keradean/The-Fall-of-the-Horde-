using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    [Header("Tower Stats")]
    public TowerStats towerStats;

	[Header("Upgrade")]
	[SerializeField] private TowerStats[] upgradeLevel;
	private int currentLevel = 0;
	public GameObject towerPrefab;

    [Header("Tower Range Indicator")]
    [SerializeField] public GameObject rangeIndicator;

    [SerializeField] private LayerMask findTheEnemy;
    [SerializeField] private Collider[]  collidersInRange;
	public List<Enemy>  enemiesInRange = new List<Enemy>();

	public bool isCastleTower;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     	towerStats = upgradeLevel[0];   
    }

    // Update is called once per frame
    void Update()
    {
        collidersInRange = Physics.OverlapSphere(transform.position, towerStats.range, findTheEnemy);
        
        enemiesInRange.Clear();
        foreach (Collider col in collidersInRange)
        {
            if(col.TryGetComponent<Enemy>(out Enemy enemy))enemiesInRange.Add(enemy);
        }
    }

	#region Upgrade
	// Upgrade your Tower
	public void Upgrade()
	{	
		if(currentLevel < upgradeLevel.Length -1) // verhindere das du über das letzte level hinaus gehst
		{
			currentLevel ++;
			towerStats = upgradeLevel[currentLevel]; // Tausche das ScriptableObject aus
		}
	}
	#endregion

	#region OnClick
	public void OnPointerClick(PointerEventData eventData)
	{
		UIController.instance.ShowUpgradeUI(this);
	}
	
	#endregion
}
