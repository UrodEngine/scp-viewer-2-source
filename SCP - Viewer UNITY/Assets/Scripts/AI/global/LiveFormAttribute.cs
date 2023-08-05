#region UPDATES.AliveFormAttributes.AliveForm_ABSTRACT_class
//====================================================================================
// UPDATE 31.05.2022 - ����������� ���� Raycast. �������� ���� ��� ������.
//====================================================================================
#endregion

using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;


/// <summary> ����������� �����, ������������ ����� �������� </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public abstract class AliveForm : IAliveConfigs
{
    public AliveForm() {
        OnThinked  += IsThinked;
        OnDamaged   += delegate { };
        OnDying     += delegate { };
    }
    #region Alterable values
    public  static System.Random    randomizer = new System.Random();

    /// <summary> �������� �������, ������������ �������� �������� �������� </summary>
    public  short   ThinkingTickrate;   
    
    /// <summary> ������� ������ </summary>
    public  short   walking;

    /// <summary> �������������� ��������. ������ ���������� ������ ���� ������ �������� </summary>
    public  short   attentiviness;

    /// <summary> ������� �����, ����� �������� �������� �������� event isThink() </summary>
    public  short   thinkingTimer;

    /// <summary> �������� �������� </summary>
    public  short   blinkingTimer;

    /// <summary> ����� ��������� </summary>
    public  short   stanTime;     
    
    /// <summary> ����� ������ </summary>
    public  short   scaredTimer;

    /// <summary> ���������� ������������� �������� �� ��������� � ����� �� </summary>
    public  bool    agressive = false;   
    
    /// <summary> ����������, ������� �� �������� ������ �� ����� ������ </summary>
    public  bool    bloodlust;

    /// <summary> ������� ����� �������� </summary>
    public  Vector3 interestPoint;

    /// <summary> ����������, ������� �� �������� ������� </summary>
    public  bool    monsterFinded;
    public  GameObjectPrefabs       IncludedObjects;
    public  ObjectFormProperties    properties = new ObjectFormProperties();
    public  IncludedComponents      components = new IncludedComponents();

    [HideInInspector] public float      allObjectSearhHeigth   = 7;
    [HideInInspector] public float      allObjectSearchRadius  = 65;
    [HideInInspector] public Collider[] allSpheredObjects;
    [HideInInspector] public Collider[] allRaycastedObjects;

    ///~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~<s ��������>
    public  System.Action 
        OnThinked   = () => { }, 
        OnDamaged   = () => { }, 
        OnDying     = () => { }, 
        OnStanned   = () => { };

    ///~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~<s ������>
    [System.Serializable] public sealed class GameObjectPrefabs    
    {
        [System.NonSerialized] 
        public GameObject       parentGameObject;
        public GameObject       DeadRagdoll;

        public Drop[] drop;

        [System.Serializable]
        public sealed class Drop
        {
            /// <summary> ���������� ������� </summary>
            public GameObject item;
            /// <summary> ���� ����������� �������� </summary>
            public short dropChance;
        }

        public void TryDrop()
        {
            if (drop != null && drop.Length > 0)
            {
                for (ushort dropIndex = 0; dropIndex < drop.Length; dropIndex++)
                {
                    if (Random.Range(0, 100) < drop[dropIndex].dropChance)
                    {
                        GameObject dropOut = GameObject.Instantiate(
                            drop[dropIndex].item,
                            parentGameObject.transform.position + new Vector3(0, 2, 0),
                            Quaternion.identity);
                    }
                }
            }
        }
    }
    [System.Serializable] public sealed class IncludedComponents   
    {
        public Animator             animator        = null;
        public NavMeshAgent         navMeshAgent    = null;
        public SkinnedMeshRenderer  skinnedMeshRenderer;
    }
    #endregion

    public                void    CheckDie                    (in GameObject _gameObject) 
    {
        if (properties.health > 0 || properties.invulnerable)
        {
            return;
        }

        if (IncludedObjects.DeadRagdoll != null)    
        {
            GameObject dragdoll         = GameObject.Instantiate(IncludedObjects.DeadRagdoll, _gameObject.transform.position, Quaternion.identity);
            dragdoll.transform.rotation = _gameObject.transform.rotation;

            if (bloodlust is false && dragdoll.GetComponent<UntieChild>())
            {
                GameObject.Destroy(dragdoll.GetComponent<UntieChild>().UntieChildObject);
            }
        }

        OnDying();

        if (IncludedObjects != null)
        {
            IncludedObjects.TryDrop();
        }  
        GameObject.Destroy(_gameObject);
    }
    public                  void    KillForm                    (in bool withBlood) {
        if (properties.invulnerable is true) return;
        properties.health = 0; 
        bloodlust = withBlood; 
    } //����� �����
    public                  void    SetDamage                   (in int damage, in bool isBloodLust){
        OnDamaged();
        if (properties.invulnerable is true) return;
        int damageAfterArmor = (int)(properties.armor - damage);
        if(properties.armor <= 1)
        properties.health = (short)Mathf.Clamp(properties.health - damage, 0, 99999999);
        properties.armor  = (short)Mathf.Clamp(properties.armor  - damage, 1, 99999999);
        bloodlust   = isBloodLust;
    } //������� �� value 
    /// <summary>
    /// ������ ���������� ������. ����� ����������� ��� ����� ������ ��������.<br/> 
    /// ---------------------------------------------------------------------------------------<br/> 
    ///  - ���� (<see cref="allRaycastedObjects"/>) <see langword="��������"/> ������ � ���� ������ � ��������� <see langword="��� ��������� �������"/> � ���� ������ ��������. <br/> 
    ///  - ���� (<see cref="thinkingTimer"/>) <see langword="�����������"/> � ���� ������ � ��������� <see langword="�������� ����� ����������"/> ��������.  <br/> 
    ///  - ���� (<see cref="blinkingTimer"/>) <see langword="�����������"/> � ���� ������ � ��������� <see langword="��������"/> ��������. <br/> 
    ///  - ���� (<see cref="stanTime"/>) <see langword="�����������"/> � ���� ������ � ��������� ����� <see langword="���������"/> ��������. <br/> 
    ///  - ���� (<see cref="scaredTimer"/>) <see langword="�����������"/> � ���� ������ � ��������� ����� <see langword="������"/> ��������. <br/> 
    ///  ---------------------------------------------------------------------------------------<br/> 
    /// </summary>
    /// <remarks>
    /// ������������� ��������� � ������ <see cref="MainThreadHandler.AddActions(in System.Action)"/>, ��������� ����� ������������ <see cref="Physics.Raycast(Ray)"/> <br/>
    /// </remarks>
    public virtual          void    CirclingHeart               () 
    {      
        thinkingTimer       -= 3;
        
        stanTime = stanTime > (short)0 ? (short)(stanTime - 1) : stanTime;
        scaredTimer = scaredTimer > (short)0 ? (short)(scaredTimer - 1) : scaredTimer;

        if (blinkingTimer <=100 )   blinkingTimer = blinkingTimer > (short)0 ? (short)(blinkingTimer - 1) : (short)100; //��������. �� 100 �� 0 � �� �����
        if (thinkingTimer <= 0)     OnThinked();

        IsBurning();

        allSpheredObjects   = Physics.OverlapSphere(IncludedObjects.parentGameObject.transform.position, allObjectSearchRadius);
        allRaycastedObjects = new NearObiUtilitiesSimple().SimpleRaycastAll(IncludedObjects.parentGameObject.transform.position, allSpheredObjects, allObjectSearchRadius, allObjectSearhHeigth);
    } 
    public                  void    SetStan                     (in int stanValue)
    {
        stanTime = (short)stanValue;
        OnStanned();
    }
    public      virtual     void    IsThinked                   (){
        walking                     = (short)Random.Range(0, 50);        
        thinkingTimer               = (short)Random.Range(ThinkingTickrate / 5, ThinkingTickrate);        

        short TypeOfInterestPoints    = (short)Random.Range(0, 1);
        switch (TypeOfInterestPoints){
            case 0:
                interestPoint = Random.Range(0,10) >=5 ? InterestingObjects.StaticPoints[Random.Range(0, InterestingObjects.StaticPoints.Length)]
                    :
                interestPoint = new InterestingObjectsSearcher().GetOnlyStaticNearestPoints(IncludedObjects.parentGameObject.transform.position, 2);             //������, ���� �� ��������� ����� ��� �����
                break;
            case 1:
                interestPoint = Random.Range(0, 10) >= 5 ? InterestingObjects.NonStaticPoints[Random.Range(0, InterestingObjects.NonStaticPoints.Length)]
                    :
                interestPoint = new InterestingObjectsSearcher().GetOnlyNonStaticNearestPoints(IncludedObjects.parentGameObject.transform.position, 2);          //������, ���� �� ��������� ����� ��� �����
                break;
        }
    } 
    internal                void    IsBurning                   ()
    {
        if (properties.heatLevel > 0)
        {
            walking = 100;
            thinkingTimer -= 25;
        }
    }
}


[StructLayout(LayoutKind.Sequential, Pack = 1), System.Serializable]
public sealed class ObjectFormProperties
{
    public short health      = 100;
    public short armor       = 2;
    public short heatLevel   = 0;
    public bool invulnerable;
} //����� ������� ����� �� ����������


[StructLayout(LayoutKind.Sequential, Pack = 1), System.Serializable]
public sealed class ManProperties : AliveForm, IAliveConfigs
{
    public ManProperties()
    {
        agressive           = false;
        properties.armor    = 2;
        properties.health   = 100;
        attentiviness       = (short)MathUE.GetRandom(75, 95);
        blinkingTimer       = (short)MathUE.GetRandom(5, 100);
        ThinkingTickrate    = (short)MathUE.GetRandom(256, 512);
    }
}


[StructLayout(LayoutKind.Sequential, Pack = 1), System.Serializable]
public sealed class MurderProperties : AliveForm, IAliveConfigs
{
    public MurderProperties()
    {
        agressive           = true;
        properties.armor    = short.MaxValue;
        properties.health   = 100;
        attentiviness       = 120;
        blinkingTimer       = 999;
        ThinkingTickrate    = 1024;
    }
}

/// <summary> ���������� ����� ���� �� ������ ������������ ������ <see cref="AliveForm"/>, ����������� �� ���������� <see cref="IAliveForm"/> </summary>
public readonly struct AliveFormFieldGetter{
    /// <summary> ���������� ����� ���� �� ������ ������������ ������ <see cref="AliveForm"/>, ����������� �� ���������� <see cref="IAliveForm"/> </summary>
    public AliveFormFieldGetter(in IAliveForm target, out AliveForm result)
    {
        result = target.GetField().GetType() == typeof(ManProperties) ?
           result = target.GetField() as ManProperties
            :
           result = target.GetField() as MurderProperties;
    }
    /// <summary> ���������� ����� ���� �� ������ ������������ ������ <see cref="AliveForm"/>, ����������� �� ���������� <see cref="IAliveForm"/> </summary>
    public AliveFormFieldGetter(in IAliveConfigs target, out AliveForm result)
    {
        result = target.GetType() == typeof(ManProperties) ?
            result = target as ManProperties
             :
            result = target as MurderProperties;
    }
}
