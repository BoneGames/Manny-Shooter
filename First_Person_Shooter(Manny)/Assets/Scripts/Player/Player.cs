using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region Variables
    [Header("Mechanics")]
    public int health = 100;
    public float runSpeed = 7.5f;
    public float walkSpeed = 6f;
    public float gravity = 10f;
    public float crouchSpeed = 4f;
    public float jumpHeight = 20f;
    public float interactRange = 10f;
    public float groundRayDistance = 1.1f;

    [Header("References")]
    public Camera attachedCamera;
    public Transform hand;

    //Animation
    private Animator anim;

    // Movement
    private CharacterController controller;
    private Vector3 movement;

    // Weapons
    public Weapon currentWeapon;
    private List<Weapon> weapons = new List<Weapon>();
    private int currentWeaponIndex = 0;
    #endregion

    void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, -transform.up);
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * groundRayDistance);
    }

    #region Initialisation
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        SelectWeapon(0);
    }
    void CreateUI()
    {

    }
    void RegisterWeapons()
    {

    }
    #endregion

    #region Controls
    /// <summary>
    /// moves the character controller in direction of input
    /// </summary>
    /// <param name="inputH"></param> horizontal input
    /// <param name="inputV"></param> vertical input
    void Move(float inputH, float inputV)
    {
        // Create direction from input
        Vector3 input = new Vector3(inputH, 0, inputV);
        // Localise direction to player transform
        input = transform.TransformDirection(input);
        // Set Move Speed
        float moveSpeed = walkSpeed;
        // apply movement
        movement.x = input.x * moveSpeed;
        movement.z = input.z * moveSpeed;
    }
    #endregion

    #region Combat 
    /// <summary>
    /// Cycles through weapons list with given direction
    /// </summary>
    /// <param name="direction"></param> -1 to 1: up or down list
    void SwitchWeapon(int direction)
    {

    }
    /// <summary>
    /// Disables attached weapon gameobjects
    /// </summary>
    void DisableAllWeapons()
    {

    }
    /// <summary>
    /// Adds weapon to list and attaches to player's hand
    /// </summary>
    /// <param name="weaponToPickup">Weapon to place in Hand</param>
    void Pickup(Weapon weaponToPickup)
    {

    }
    /// <summary>
    /// Removes weapon to list and removes from players hand
    /// </summary>
    /// <param name="weaponToDrop"></param> Weapon to remove from hand
    void Drop(Weapon weaponToDrop)
    {

    }

    void SelectWeapon(int index)
    {

    }
    #endregion

    #region Actions
    /// <summary>
    /// Handles movemetn mechanic
    /// </summary>
    void Movement()
    {
        // get input from user
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        Move(inputH, inputV);
        // Is the controller grounded?
        Ray groundRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if(Physics.Raycast(groundRay, out hit, groundRayDistance))
        {
            // if jump is pressed
            if(Input.GetButtonDown("Jump"))
            {
                // move controller up
                movement.y = jumpHeight;
            }
        }
       
        // move controller up
        // apply gravity
        movement.y -= gravity * Time.deltaTime;
        movement.y = Mathf.Max(movement.y, -gravity);
        // move controller
        controller.Move(movement * Time.deltaTime);
    }
    /// <summary>
    /// Interaction with items in the world
    /// </summary>
    void Interact()
    {

    }
    /// <summary>
    /// use current weapon to fire
    /// </summary>
    void Shooting()
    {

    }
    /// <summary>
    /// cycle through available weapons
    /// </summary>
    void Switching()
    {

    }
    #endregion

    void Update () {
        Movement();
        Shooting();
        Switching();
        Interact();
	}
}
