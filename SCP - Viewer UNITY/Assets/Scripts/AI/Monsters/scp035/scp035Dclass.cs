using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("SCP/035 - mask Dclass")]
public class scp035Dclass : MonoBehaviour
{
	private Man mind => GetComponent(nameof(Man)) as Man;
	private bool searchVictims;
	public List<GameObject> recruited = new List<GameObject>();
	[SerializeField] private System.UInt16 victimVerbLevel;
	[SerializeField] private Man.ManTypeEnum[] enemiesList = new Man.ManTypeEnum[] 
	{
		Man.ManTypeEnum.Dclass,
		Man.ManTypeEnum.Scientist,
		Man.ManTypeEnum.Security,
		Man.ManTypeEnum.MTF,
		Man.ManTypeEnum.Chaos,
	};
	public GameObject men;

	private void Awake()
	{
		mind.DClassConfigs.properties.health	= 512;
		mind.DClassConfigs.properties.armor		= 256;
		mind.bravery = 15;
		mind.shootIfIsScared = false;
		mind.DClassConfigs.OnThinked += () =>
		{
			if (searchVictims is false) searchVictims = Random.Range(0, 100) > 50 ? true : false; 
		};
		mind.DClassConfigs.OnDying += () => 
		{
			searchVictims = false;
		};
	}
	private void FixedUpdate()
	{
		if (searchVictims is true) SearchMans();		
	}

	private void SearchMans()
	{
		MainThreadHandler.AddActions(() => 
		{
			Collider[] sphered = Physics.OverlapSphere(transform.position, 50);
			Collider[] raycasted = new NearObiUtilitiesSimple().SimpleRaycastAll(transform.position, sphered, 50, 8);
			NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 8, raycasted, nameof(Man), out men);
		});

        try
        {
			if (men != null)
			{
				bool checkThis = false;
				for (int i = 0; i < recruited.ToArray().Length; i++)
				{
					try
					{
						if (men == recruited[i]) checkThis = true;
					}
					catch
					{
						recruited.RemoveAt(i);
					}
					continue;
				}
				if (checkThis == false)
				{
					mind.DClassConfigs.walking = 100;
					mind.DClassConfigs.interestPoint = men.transform.position;
					Man victimConfigs = men.GetComponent<Man>();
					victimConfigs.DClassConfigs.walking = 100;
					victimConfigs.DClassConfigs.interestPoint = transform.position;
					mind.ManType = victimConfigs.ManType;
					if (Vector3.Distance(transform.position, men.transform.position) < 8)
					{
						victimVerbLevel++;
						mind.dialogTimer = 25;
						victimConfigs.dialogTimer = 25;
						victimConfigs.EnemiesType = new Man.ManTypeEnum[] { Man.ManTypeEnum.Security };
						victimConfigs.ClearEnemies();
						RecruitingCheck(victimConfigs);
					}
				}
			}
		}
        catch
        {

        }
	}
	private void RecruitingCheck(Man victim)
    {
		if (victimVerbLevel < 256)
        {
			return;
		}
		recruited.Add(victim.gameObject);
		victim.ManType = Man.ManTypeEnum.AbsolutelyAgressive;
		//victim.DClassConfigs.agressive = true;
		victim.bravery = byte.MaxValue;
		searchVictims = false;
		victimVerbLevel = 0;
		victim.EnemiesType = enemiesList;
		mind.ManType = Man.ManTypeEnum.AbsolutelyAgressive;
        for (sbyte i = 0; i < 2; i++)
        {
			victim.ClearEnemies();
			victim.DClassConfigs.SetStan(150);
			continue;
		}
    }
}
