using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Player.Data.PlayerAttributeData;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;


[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField]  public PlayerSO Data { get; private set; }

    [field: SerializeField] public float AdjustSpeed { get; private set; }
    
    public int Health;
    public float Experience;
    public int Gold;

    #region Levels

    public int craftingLevel;
    public int smithingLevel;

    #endregion

    #region Experience

    public double craftingXp;
    public double smithingXp;
    
    #endregion
    

    public List<Quest> questList = new List<Quest>();

    //multiplayer stuff:
    public PhotonView view;
    //end of multiplayer stuff

    //if an enemy is killed, increase goal amount
    
    [field: Header("Collisions")]
    //[field: SerializeField] public CapsuleCollidersUtility CollidersUtility { get; private set; }
    [field: SerializeField] public PlayerCapsuleColliderUtility CollidersUtility { get; private set; }
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

    [field: SerializeField] public CraftingSystem CraftingSystem;
    
    [field: SerializeField] public PlayerLayerData playerData { get; private set; }
    
    [field: SerializeField] public PlayerAttributeData playerAttribute { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public PlayerInput Input { get; private set; } 
    
    public Transform MainCameraTransform { get; private set; }
    
    private PlayerMovementStateMachine MovementStateMachine;

    public void Awake()
    {
        getLevels();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AdjustSpeed = 5f;
        Rigidbody = GetComponent<Rigidbody>();
        CollidersUtility.Initialize(gameObject);
        CollidersUtility.CalculateCapsuleColliderDimensions();
        CraftingSystem = new CraftingSystem(this);
        CraftingSystem.addLevel();
        Input = GetComponent<PlayerInput>();
        
        MainCameraTransform = Camera.main.transform;
        CraftingSystem.isCrafting("Shoes", craftingLevel);
        MovementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void OnValidate()
    {
        CollidersUtility.Initialize(gameObject);
        CollidersUtility.CalculateCapsuleColliderDimensions();
    }

    public void Start()
    {
        view = GetComponent<PhotonView>();
        MovementStateMachine.ChangeState(MovementStateMachine.IdlingState);
    }

    private void OnTriggerEnter(Collider collider)
    {
        MovementStateMachine.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        MovementStateMachine.OnTriggerExit(collider);
    }

    public void Update()
    {
        if (view.IsMine)
        {
            if (Keyboard.current.leftAltKey.wasPressedThisFrame && Cursor.visible == true)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("Cursor being turned off!");
            }
            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                Debug.Log(" "+PhotonNetwork.PlayerList.Length);
            }
            else if (Keyboard.current.leftAltKey.wasPressedThisFrame && Cursor.visible == false)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Cursor being turned back on!");
            }
            MovementStateMachine.HandleInput();

            MovementStateMachine.Update();
        }
        
    }

    public void getLevels()
    {
        craftingLevel = playerAttribute.CraftingLevel;
        smithingLevel = playerAttribute.SmithingLevel;
    }

    public void FixedUpdate(){
        MovementStateMachine.PhysicsUpdate();
    }
}