using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Runtime.InteropServices;

/// Created by UROD Engine
/// Образ человека и его поведения.
///------------------------------------------------------------------------------------------------------------------------------------
/// Version 2.0 - Переделано с нуля.
///------------------------------------------------------------------------------------------------------------------------------------
/// Имеет возможность использовать 3+ типа оружия (Пистолет, Дробовик, Винтовка и т.д.)
/// Умеет бояться и драться в ближнем бою                                    
/// Научился более адекватно избегать монстров                               
/// Храбрость выбирается случайно и в зависимости от вида специальности      
/// Научился подбирать оружие с земли <TakeWeapon>. Поиск оружия ведет метод 
/// Если при подборе пушки у человека нету патронов - ему выдаются патроны   
///------------------------------------------------------------------------------------------------------------------------------------
/// 16.05.2022 update - параметры гендера удалены.   они перемещены к scp - 113.
/// 16.05.2022 update - зомби префабы удалены.       они перемещены к scp - 049.
/// 16.05.2022 update - рефакторинг интерфейсов для взаимодействий AliveForm классов
/// 
/// 14.03.2023 update - мышление людей происходит не одновременно, а чередуемо через <see cref="MainThreadHandler"/>
/// 
/// 25.05.2023 update - поля гендерных мешей возвращены обратно.
/// 25.05.2023 update - поле зомби префабан возвращено обратно.
/// 25.05.2023 update - добавлены поля дополнительных анимаций
/// 25.05.2023 update - переработка полей


[StructLayout(LayoutKind.Sequential, Pack = 1)]
public sealed class Man : MonoBehaviour, IAliveForm, IPassportData, ISCPSkillRequest
{
    #region Alterable values
    /*=========================================================================================================================================================*/
    public enum             ManTypeEnum { Dclass = 0, Scientist = 1, Security = 2, MTF = 3, Chaos = 4, AbsolutelyAgressive = 5 }

    public ManProperties    DClassConfigs = new ManProperties();
    public ushort           dialogTimer;
    public short            patrons;
    public short            damage;    
    
    /// <summary> храбрость, если значение меньше, чем у <see cref="braveryCheck"/> - человек убежит. При высокой храбрости человек будет драться </summary>
    [Range(25,byte.MaxValue)]   public  byte bravery;
                                private byte braveryCheck = 45;

    /// <summary> будет ли человек стрелять, даже если монстр уже рядом.  </summary>
    public bool             shootIfIsScared;    

    public  bool            enemyIsNear;
    private GameObject      _enemiesTypesMan;
    private GameObject      _monster;

    public bool             canOpenDoors;

    [Header("Mens personality & loyality types:")]
    public ManTypeEnum      ManType;
    public ManTypeEnum[]    EnemiesType;

    [Header("Zombie corpse:")]
    public GameObject       zombieRagdollPrefab;

    [Header("Genders:")]
    public GenderMeshes     genderMeshes;

    [Header("Animations:")]
    public AdditionalAnimations additionalAnimations;

    [Header("AudioClips:")]
    public ClipsList        clipList;

    ///<summary> Здесь закрепляется шапка на голове человека. Это кость в скелете модели человека </summary>
    [Header("Slots:")]
    public GameObject       HatSlot;
    ///<summary> Здесь закрепляется оружие на руке человека. Это кость в скелете модели человека </summary>
    public GameObject       HandWeaponSlot;
    ///<summary> Здесь закрепляется оружие на спине человека. Это кость в скелете модели человека </summary>
    public GameObject       Spina;
    ///<summary>Префаб оружия.</summary>
    public GameObject       Weapon;

    public string           aliveName           { get; set; }
    public string           aliveSurname        { get; set; }
    public short            aliveAges           { get; set; }
    public float            heigh               { get => 7; }
    private float           _distanceToScared   { get; set; }   //Дистанция, на которой идет проверка храбрости

    [HideInInspector] public Rigidbody        rigidBody;
    [HideInInspector] public NavMeshAgent     navMeshAgent;

    public SCPSkillNode[]   skills { get; set; } = new SCPSkillNode[2] 
    {
        new SCPSkillNode("Run", 250),
        new SCPSkillNode("Say", 500)
    };


    /*=========================================================================================================================================================*/
    #endregion

    private     void        Awake               ()
    {
        rigidBody       = this.GetComponent<Rigidbody>();
        navMeshAgent    = this.GetComponent<NavMeshAgent>();


        DClassConfigs.components.skinnedMeshRenderer.sharedMesh = Random.Range(0, 100) > 50 ? genderMeshes.male : genderMeshes.female ?? genderMeshes.male;

        SkillsSet();

        DClassConfigs.IncludedObjects.parentGameObject =    this.gameObject;

        DClassConfigs.OnDamaged     += () => 
        { 
            DClassConfigs.components.animator.Play("Xhurt",1,0);
            DClassConfigs.components.animator.SetLayerWeight(2, 0);
            DClassConfigs.components.animator.SetLayerWeight(1, 1); 
        };
        DClassConfigs.OnThinked     += () => 
        {
            #region dialogs
            //=========================== For dialogs comment clouds ==========================================
            Collider[] alls = Physics.OverlapSphere(transform.position, 25);
            NearObiUtilitiesSimpleStatic.SimpleRaycastAll(transform.position, alls, 25, 8, out Collider[] clearedalls);
            NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 8, clearedalls, nameof(Man), out GameObject men);
            if (Random.Range(0, 100) > 60 && men != null && men != this.gameObject) dialogTimer = 100;
            //=========================== For dialogs comment clouds ==========================================
            #endregion
        }; 
        DClassConfigs.OnDying       += () => 
        {
            #region dead counter PlayerPrefs
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.STATISTICS_DEADMENS))
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.STATISTICS_DEADMENS, PlayerPrefs.GetInt(PlayerPrefsKeys.STATISTICS_DEADMENS) + 1);
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.STATISTICS_DEADMENS, 1);
            }

            if (CamMove_v2.instance != null)
            {
                CamMove_v2.instance.SetShake(Random.Range(0.3f, 0.8f));
            }
            #endregion
        };
        DClassConfigs.OnStanned     += () => 
        {
            if (DClassConfigs.components.animator is null || DClassConfigs.components.navMeshAgent is null) return;

            DClassConfigs.components.navMeshAgent.SetDestination(DClassConfigs.IncludedObjects.parentGameObject.transform.position);
            DClassConfigs.components.animator.SetLayerWeight(2, 0);
            DClassConfigs.components.animator.SetLayerWeight(1, 0);
        };

        aliveSurname    = ManNames.TakeSurname();
        aliveName       = ManNames.TakeName();
        aliveAges       = (short)Random.Range(19, 66);

        bravery         = (byte)(bravery + Random.Range(-25, 25));

        shootIfIsScared = Random.Range(0, 100) > 50 ? true : false;

        TakeGunFinally();           
    }
    public      void        Update              ()
    {
        if (DClassConfigs.components.animator.GetCurrentAnimatorStateInfo(1).normalizedTime>=0.95f)
        {
            DClassConfigs.components.animator.SetLayerWeight(1, 0);
        }


        if (DClassConfigs.stanTime > 0)
        {
            DClassConfigs.components.animator.SetLayerWeight(3, Mathf.Lerp(DClassConfigs.components.animator.GetLayerWeight(3), 1, 0.4f));
            DClassConfigs.components.navMeshAgent.SetDestination(DClassConfigs.IncludedObjects.parentGameObject.transform.position);
            DClassConfigs.walking = 0;
            return;
        }
        else
        {
            DClassConfigs.components.animator.SetLayerWeight(3, Mathf.Lerp(DClassConfigs.components.animator.GetLayerWeight(3), 0, 0.4f));
        }

        if (ManType == ManTypeEnum.AbsolutelyAgressive || DClassConfigs.agressive)
        {
            EnemyTypeLogic();
        }
    }
    public      void        FixedUpdate         ()
    {
        WalkingAnimationUtility.Animate(DClassConfigs.components.animator, navMeshAgent, rigidBody, 2, 0.015f, 5, 0.4f);
        dialogTimer = dialogTimer > (ushort)0 ? (ushort)(dialogTimer - 1) : dialogTimer;

        MainThreadHandler.AddActions(DClassConfigs.CirclingHeart);
        DClassConfigs.CheckDie(DClassConfigs.IncludedObjects.parentGameObject);


        HandleProps();

        enemyIsNear = false;

        navMeshAgent.speed = Mathf.MoveTowards(navMeshAgent.speed, 16, 0.75f);
        RunningAnimation();
        //=============================================
        if (DClassConfigs.stanTime > 0)
        {
            return;
        }
        IWannaWalking(); 
    } 
    private     void        HandleProps         ()
    {
        if (navMeshAgent.isOnNavMesh is false || navMeshAgent.enabled is false)   return;

        if      (_monster != null)
        {
            BraveryMethods(_monster);
        }
        else if (_enemiesTypesMan != null)
        {
            BraveryMethods(_enemiesTypesMan);
        }

        try
        {
            TryTakeHP(DClassConfigs.allRaycastedObjects);
            TryTakeGun(DClassConfigs.allRaycastedObjects);
            TryTakeAmmo(DClassConfigs.allRaycastedObjects);
            SearchMonster(DClassConfigs.allRaycastedObjects); //+BraveryMethods
        }
        catch 
        {
            
        }

        SearchNearestDoor();
    }
    private     void        TryTakeGun          (in Collider[] _colliders)
    {
        if (Weapon != null)
        {
            return;
        }
        NearObiUtilitiesSimpleStatic.NearestTargetTag(transform, 5, _colliders, "Weapon_interactive",out GameObject weapon);
        if (DClassConfigs.stanTime <= 0 && DClassConfigs.walking >= 35)
        {
            if (weapon != null && weapon.transform.parent == null)
            {
                DClassConfigs.interestPoint = weapon.transform.position;
                navMeshAgent.SetDestination(weapon.transform.position);

                if (Vector3.Distance(transform.position, weapon.transform.position) < 7f)
                {
                    Weapon = weapon.gameObject;
                    TakeGunFinally();
                    DClassConfigs.components.animator.Play(additionalAnimations.getItem.name, 0);
                    DClassConfigs.walking = 0;
                    GameObject.Destroy(weapon);
                }
            }
        }
    }
    private     void        TryTakeAmmo         (in Collider[] _colliders)
    {
        if (patrons > 4 || DClassConfigs.properties.health < 50)
        {
            return;
        }
        NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 5, _colliders, nameof(ImportantTools),out GameObject ammoBox);
        if (ammoBox is null)
        {
            return;
        }
        if (ammoBox != null && ammoBox.GetComponent<ImportantTools>().ToolType is ImportantTools.ImportantToolsList.AmmoBox)
        {
            DClassConfigs.interestPoint = ammoBox.transform.position;
            navMeshAgent.SetDestination(ammoBox.transform.position);

            if (Vector3.Distance(transform.position, ammoBox.transform.position) < 7f)
            {
                patrons = Weapon != null? (short)PropWeaponType.FindPatrons(Weapon.GetComponent<PropWeaponType>().weaponType) : (short)(patrons+4);
                DClassConfigs.components.animator.Play(additionalAnimations.getItem.name, 0);
                DClassConfigs.walking = 0;
                GameObject.Destroy(ammoBox);
            }
        }
    }
    private     void        TryTakeHP           (in Collider[] _colliders)
    {
        if (DClassConfigs.properties.health >= 50)
        {
            return;
        }
        NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, 5, _colliders, nameof(ImportantTools),out GameObject medkit);
        if (medkit != null && medkit.GetComponent<ImportantTools>().ToolType is ImportantTools.ImportantToolsList.Medkit)
        {
            DClassConfigs.interestPoint = medkit.transform.position;
            navMeshAgent.SetDestination(medkit.transform.position);

            if (Vector3.Distance(transform.position, medkit.transform.position) < 7f)
            {
                DClassConfigs.components.animator.Play(additionalAnimations.getItem.name, 0);
                DClassConfigs.walking = 0;
                GameObject.Destroy(medkit);
                DClassConfigs.properties.health = 100;
            }
        }
    }
    /// <summary>
    /// --------------------------------------------------------------------------------------------------------------<br/>
    /// Если уже есть оружие, то взять при спавне. Этот метод работает, даже если оружие берется позже с пола. <br/>
    /// --------------------------------------------------------------------------------------------------------------<br/>
    /// Под капотом выполняется <see cref="PropWeaponType.FindPatrons(in PropWeaponType.WhatWeaponType)"/> - Если патронов в кармане нету, найти патроны в найденной пушке <br/>
    /// --------------------------------------------------------------------------------------------------------------<br/>
    /// </summary>
    private     void        TakeGunFinally      ()
    {
        if (Weapon != null)
        {
            GameObject weaponOnHand     = Instantiate(Weapon, HandWeaponSlot.transform);
            GameObject weaponOnSpina    = Instantiate(Weapon, Spina.transform);
            Weapon                      = weaponOnHand;

            weaponOnHand.transform.localPosition    = new Vector3(0, 0, 0);
            weaponOnSpina.transform.localPosition   = new Vector3(0, 0, 0);

            weaponOnHand.transform.localRotation    = new Quaternion(0, 0, 0, 0);
            weaponOnSpina.transform.localRotation   = new Quaternion(0, 0, 0, 0);

            patrons = patrons <= 0 ? 
                (short)PropWeaponType.FindPatrons(Weapon.GetComponent<PropWeaponType>().weaponType) 
                : 
                (short)(patrons + PropWeaponType.FindPatrons(Weapon.GetComponent<PropWeaponType>().weaponType));
        }
    }
    private     void        IWannaWalking       ()
    {
        if (navMeshAgent.enabled is false || navMeshAgent.isOnNavMesh is false)
        {
            return;
        }
        if (DClassConfigs.walking < 35 || DClassConfigs.components.animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            navMeshAgent.SetDestination(transform.position);
            return;
        }
        navMeshAgent.SetDestination(DClassConfigs.interestPoint);
    }
    private     void        SearchMonster       (in Collider[] _colliders)
    {
        if (_colliders is null) return;

        if (DClassConfigs.agressive is false && enemyIsNear)
        {
            NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, heigh, _colliders, nameof(DangerChecker), out _monster);
            if (DClassConfigs.blinkingTimer > 30)
            {
                _monster.GetComponent<DangerChecker>().discoveredTimer = DClassConfigs.attentiviness;
            }
        }
        else if (DClassConfigs.agressive is true && enemyIsNear)
        {
            NearObiUtilitiesSimpleStatic.NearestTargetComponent(transform, heigh, _colliders, nameof(Man), nameof(DangerChecker), out _monster);
            if (DClassConfigs.blinkingTimer > 30)
            {
                _monster.GetComponent<DangerChecker>().discoveredTimer = DClassConfigs.attentiviness;
            }
        }
        if (!_monster)
        {
            for (short i = 0; i < EnemiesType.Length; i++)
            {
                NearObiUtilitiesSimpleStatic.NearestDclass(transform, heigh, _colliders, EnemiesType[i], out _enemiesTypesMan);
                if (_enemiesTypesMan != null) break;
            }
        }
            

    }   //+BraveryMethods [Проверяет на смелость + есть ли в руках пушка]
    private     void        SearchNearestDoor   ()
    {
        if (navMeshAgent.isOnNavMesh is false || navMeshAgent.enabled is false)
        {
            return;
        }
        Collider[] DoorOverpass = Physics.OverlapSphere(transform.position, 10);
        if (canOpenDoors)
        {
            NearObiUtilitiesSimpleStatic.NearestTargetTag(transform, 7, DoorOverpass, "DoorGates",out GameObject door);
            if (door != null)
            {
                door.GetComponent<Animator>().SetInteger("Timer", 45);
            }
        }
    }
    private     void        BraveryMethods      (in GameObject monsterTarget)
    {
        if (monsterTarget is null || (monsterTarget != null && monsterTarget == this.gameObject)) return;

        if (shootIfIsScared is true) _distanceToScared = 0;
        else _distanceToScared = 12.5f;

        if (Weapon != null && patrons > 0 && Vector3.Distance(transform.position, monsterTarget.transform.position) > _distanceToScared) // Если есть пушка в руках и есть патроны - пытаться стрелять в монстра
        {
            if (navMeshAgent.enabled && navMeshAgent.isOnNavMesh) navMeshAgent.SetDestination(transform.position);
            Vector3 looktomonster = new Vector3(monsterTarget.transform.position.x, transform.position.y, monsterTarget.transform.position.z);
            transform.LookAt(looktomonster);
            ShootRequiring(monsterTarget);
            return;
        }
        else if (patrons > 0 && monsterTarget != null && Vector3.Distance(transform.position, monsterTarget.transform.position) < _distanceToScared)
        {
            RunOrPunch(monsterTarget);
        }
        else if (patrons <= 0) RunOrPunch(monsterTarget); // Если патронов нету - убегать или драться.
    }   //+ShootRequiring [Проверяет, что за оружие в руках]
    public      void        ShootRequiring      (in GameObject monsterTarget)
    {
        if (monsterTarget is null || (monsterTarget != null && monsterTarget == this.gameObject))
        {
            return;
        }

        PropWeaponType.WhatWeaponType WeaponType = Weapon.GetComponent<PropWeaponType>().weaponType;
        if (Weapon.GetComponent<Weapon>().currentReload <= 0)
        {
            switch (WeaponType)
            {
                case PropWeaponType.WhatWeaponType.pistol:
                    StartCoroutine(FireByWeapon(this, additionalAnimations.pistolsType, Weapon, monsterTarget.transform.position + new Vector3(0, monsterTarget.GetComponent<IAliveForm>().heigh, 0)));
                    break;

                case PropWeaponType.WhatWeaponType.shotgun:
                    StartCoroutine(FireByWeapon(this, additionalAnimations.shootgunType, Weapon, monsterTarget.transform.position + new Vector3(0, monsterTarget.GetComponent<IAliveForm>().heigh, 0)));
                    break;

                case PropWeaponType.WhatWeaponType.automatic_rifle:
                    StartCoroutine(FireByWeapon(this, additionalAnimations.riflesType, Weapon, monsterTarget.transform.position + new Vector3(0, monsterTarget.GetComponent<IAliveForm>().heigh, 0)));
                    break;
                default:
                    break;
            }
        }
    }
    private     void        RunOrPunch          (in GameObject monsterTarget)
    {
        if (monsterTarget is null) return;
        if (navMeshAgent.isOnNavMesh is false || navMeshAgent.enabled is false) return;

        monsterTarget.TryGetComponent<IAliveForm>(out IAliveForm componenter);    //Ищем чела, у которого есть образ AliveForm
        AliveForm targetConfigs;
        new AliveFormFieldGetter(componenter, out targetConfigs);
      
        try
        {
            if (Vector3.Distance(transform.position, monsterTarget.transform.position) < 20f)
            {
                DClassConfigs.walking = 100;
                if (bravery > braveryCheck && (navMeshAgent.enabled) && targetConfigs.properties.invulnerable is false)
                {
                    DClassConfigs.interestPoint = monsterTarget?.transform.position ?? transform.position;
                    navMeshAgent.SetDestination(monsterTarget?.transform.position ?? transform.position);
                }
                else if (bravery < braveryCheck || targetConfigs.properties.invulnerable is true)
                {
                    InteractiveMethods.FearByObject(monsterTarget.transform.position, transform);
                }
            }

            if (bravery > braveryCheck && Vector3.Distance(transform.position, monsterTarget.transform.position) < 8f && !DClassConfigs.components.animator.GetCurrentAnimatorStateInfo(0).IsName(additionalAnimations.meleeClip.name))
            {
                transform.LookAt(new Vector3(monsterTarget.transform.position.x, transform.position.y, monsterTarget.transform.position.z)); //Look at monster vector3 position

                DClassConfigs.components.animator.Play(additionalAnimations.meleeClip.name, 0);
                SoundSpots.Generate(transform,clipList.Get($"hit{Random.Range(0, clipList.elements.Length)}"), out AudioSource aSource);
                aSource.spatialBlend = 1;

                if (monsterTarget.TryGetComponent<IAliveForm>(out IAliveForm component))
                {
                    component.GetField().SetDamage(damage, false);
                }
            }
        }
        catch
        {
            return;
        }
    }
    private     void        EnemyTypeLogic      ()
    {
        Collider[] DclassNearest = Physics.OverlapSphere(transform.position, 90);
        for (short i = 0; i < DclassNearest.Length; i++)
        {
            if (DclassNearest[i] != null && DclassNearest[i].GetComponent<Man>())
            {
                DclassNearest[i].GetComponent<Man>().enemyIsNear = true;
            }
        }
    }
    public      void        ClearEnemies        ()
    {
        _enemiesTypesMan    = null;
        _monster            = null;
    }
    public      void        RunningAnimation    () => DClassConfigs.components.animator.SetBool("ifRunning", navMeshAgent.speed > 20 ? true : false);

    
    public          IAliveConfigs   GetField        () => DClassConfigs; // Это поле - класс, реализующий интерфейс IAliveDamageConfigs
    public          void            SkillsSet       ()
    {
        skills[0].isUsedSkill += () => {
            navMeshAgent.speed = 70;
        };
        skills[1].isUsedSkill += () => {
            dialogTimer = 100;
        };
    }
    public static   IEnumerator     FireByWeapon    (Man man, AnimationClip animClip, GameObject weaponInstance, Vector3 shootTo)
    {
        Weapon      weapon      = weaponInstance.GetComponent<Weapon>();
        Collider    manCollider = man.gameObject.GetComponent<Collider>();

        weapon.currentReload = weapon.shootDelay;

        man.DClassConfigs.components.animator.Play(animClip.name, 0, 0);

        yield return new WaitForSeconds(animClip.length / 3.4f);

        manCollider.enabled = false;
        man.DClassConfigs.components.animator.SetLayerWeight(1, 0);

        weapon.TryShoot(shootTo);

        man.patrons--;

        yield return new WaitForSeconds(0.01f);
        manCollider.enabled = true;
    }


    [System.Serializable]
    public sealed class GenderMeshes
    {
        public Mesh male;
        public Mesh female;
    }

    [System.Serializable]
    public sealed class AdditionalAnimations
    {
        [Header("Melee:")]
        public AnimationClip meleeClip;

        [Header("Ranges:")]
        public AnimationClip shootgunType;
        public AnimationClip riflesType;
        public AnimationClip pistolsType;

        [Header("Other:")]
        public AnimationClip getItem;
    }
}

public readonly struct WalkingAnimationUtility   
{
    private static void AnimateWalk  (in Animator animator, in int layerSource, in Rigidbody rigidBody, in float valueToStop, in float speedMultiplier, in float lerpSpeed)
    {
        float firstAdaptaionOfSpeed = Mathf.Clamp(rigidBody.velocity.magnitude, 0, 10);
        float normalizedValue       = (firstAdaptaionOfSpeed - 0) / (10 - 0);

        if (normalizedValue > valueToStop)
            animator.SetLayerWeight(layerSource, normalizedValue * speedMultiplier);
        else
            animator.SetLayerWeight(layerSource, Mathf.Lerp(animator.GetLayerWeight(layerSource), 0, lerpSpeed));
    }
    private static void AnimateWalk  (in Animator animator, in int layerSource, in NavMeshAgent navMesh, in float valueToStop, in float speedMultiplier, in float lerpSpeed)
    {
        float firstAdaptaionOfSpeed = Mathf.Clamp(navMesh.velocity.magnitude, 0, 10);
        float normalizedValue       = (firstAdaptaionOfSpeed - 0) / (10 - 0);

        if (normalizedValue > valueToStop)
            animator.SetLayerWeight(layerSource, normalizedValue * speedMultiplier);
        else
            animator.SetLayerWeight(layerSource, Mathf.Lerp(animator.GetLayerWeight(layerSource), 0, lerpSpeed));
    }
    /// <summary>
    /// ----------------------------------------------------------------------------------------------------<br/>
    /// Анимирует существу передвижение, опираясь на <see cref="NavMeshAgent"/> и <see cref="Rigidbody"/> состояния. <br/>
    /// По сути в <see cref="Animator"/>, в зависимости от скорости существа, у слоя изменяется значение <see langword="weight"/><br/>
    /// ----------------------------------------------------------------------------------------------------<br/>
    /// В качестве входных параметров использует: <br/>
    /// <see langword="layerSource"/>       - ID слоя компонента <see cref="Animator"/>, в котором находится зацикленная анимация ходьбы<br/>
    /// <see langword="valueToStop"/>       - значение минимальной скорости существа, достижение которой приведет к отлючению слоя анимации ходьбы<br/>
    /// <see langword="speedMultiplier"/>   - множитель параметра <see langword="valueToStop"/> <br/>
    /// <see langword="lerpSpeed"/>         - скорость появления и исчезновения анимации ходьбы<br/>
    /// ----------------------------------------------------------------------------------------------------<br/>
    /// Под капотом используются static void методы: <br/>
    /// 1) Если у существа активен компонент <see cref="NavMeshAgent"/>:<br/>
    /// __<see cref="AnimateWalk(in Animator, in int, in NavMeshAgent, in float, in float, in float)"/><br/>
    /// 2) Если у существа не активен компонент <see cref="NavMeshAgent"/>:<br/>
    /// __<see cref="AnimateWalk(in Animator, in int, in Rigidbody, in float, in float, in float)"/><br/>
    /// </summary>
    public static void Animate      (in Animator animator, in NavMeshAgent navMesh, in Rigidbody rigidBody, in int layerSource, in float valueToStop, in float speedMultiplier, in float lerpSpeed)
    {
        try
        {
            if (navMesh.enabled is true && navMesh.isOnNavMesh is true)
            {
                WalkingAnimationUtility.AnimateWalk(animator, layerSource, navMesh, valueToStop, speedMultiplier, lerpSpeed);
            }
            else if (navMesh.enabled is false || navMesh.isOnNavMesh is false)
            {
                WalkingAnimationUtility.AnimateWalk(animator, layerSource, rigidBody, valueToStop, speedMultiplier, lerpSpeed);
            }
        }
        catch
        {
            animator.SetLayerWeight(layerSource, Mathf.Lerp(animator.GetLayerWeight(layerSource), 0, lerpSpeed));
        }
    }
}