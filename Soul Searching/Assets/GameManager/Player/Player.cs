using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //PauseMenu
    public GameObject pauseMenu;

    //player
    private Vector3 resetLocation;
    public bool bresetPlayer = false;
    PlayerControls pActions;
    private CharacterController cController;
    private Vector3 desiredDirection;
    public float speed = 5f;
    public float faceRotationSpeed = 5f;

    //Skeleton Possession
    private Transform currentSkeletonPile;
    private int playerChildCount;
    public bool bpossessSkel = false;
    public float skelSpeed = 3f;
    public float skelfaceRotationSpeed = 4f;
    private bool bOnePile;

    public LayerMask doggyLayer;
    RaycastHit hit;
    public bool bwhistling;

    public int collectablesCount;
    public static int savedCollection;

    private void OnEnable()
    {
        pActions = new PlayerControls();
        pActions.Enable();

        pActions.PlayerActions.Whistle.started += Whistle;
    }

    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerActions.Whistle.started -= Whistle;
    }

    private void Awake()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = false;
    }

    private void Start()
    {
        resetLocation = transform.position;
        cController = GetComponent<CharacterController>();
        playerChildCount = transform.childCount;
    }

    void Update()
    {
        Movement();
        ResetPlayer();
    }
    
    private void Movement()
    {
        desiredDirection.x = pActions.PlayerActions.Movement.ReadValue<Vector2>().x;
        desiredDirection.z = pActions.PlayerActions.Movement.ReadValue<Vector2>().y;
        if(!bpossessSkel)
        {
            cController.Move(desiredDirection * Time.deltaTime * speed);

            if (desiredDirection.x != 0 || desiredDirection.z != 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(desiredDirection.x, 0, desiredDirection.z)), Time.deltaTime * faceRotationSpeed);
            }
        }
        else if(bpossessSkel)
        {
            //skeliton animation
            if(currentSkeletonPile != null && (pActions.PlayerActions.Movement.ReadValue<Vector2>().x != 0 || pActions.PlayerActions.Movement.ReadValue<Vector2>().y != 0))
            {
                currentSkeletonPile.GetChild(0).GetComponent<Animator>().SetBool("bSkelWalk", true);
            }
            else if(currentSkeletonPile != null && (pActions.PlayerActions.Movement.ReadValue<Vector2>().x == 0 || pActions.PlayerActions.Movement.ReadValue<Vector2>().y == 0))
            {
                currentSkeletonPile.GetChild(0).GetComponent<Animator>().SetBool("bSkelWalk", false);
            }

            cController.Move(desiredDirection * Time.deltaTime * skelSpeed);

            if (desiredDirection.x != 0 || desiredDirection.z != 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(desiredDirection.x, 0, desiredDirection.z)), Time.deltaTime * skelfaceRotationSpeed);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Get Skeleton Parts
        if(!bpossessSkel)
        {
            if (other.CompareTag("SkeletonPile"))
            {
                pauseMenu.GetComponent<UIElements>().PossessUIOn();

                currentSkeletonPile = other.transform;
                pActions.PlayerActions.Possess.performed += Possess;
            }
        }

        //kill Player
        if(other.CompareTag("DeathBox") && !bpossessSkel)
        {
            //player holds reset manager
            bresetPlayer = true;
            GetComponent<ResetDelegate>().bcallReset = true;
            pActions.PlayerActions.Possess.performed -= Possess;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        //hell hound knocks player out of bones
        if (other.CompareTag("HellHound") && bpossessSkel)
        {
            KillSkeleton();
        }

        if(other.CompareTag("WardenAttack"))
        {
            if(bpossessSkel)
            {
                KillSkeleton();
            }
            bresetPlayer = true;
            ResetPlayer();
            GetComponent<ResetDelegate>().bcallReset = true;
        }

        if (bpossessSkel)
        {
            if (other.CompareTag("SkeletonPile"))
            {
                pActions.PlayerActions.Possess.performed -= Possess;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!bpossessSkel)
        {
            if (other.CompareTag("SkeletonPile"))
            {
                pauseMenu.GetComponent<UIElements>().PossessUIOff();

                pActions.PlayerActions.Possess.performed -= Possess;
            }
        }
        if (bpossessSkel)
        {
            if (other.CompareTag("SkeletonPile"))
            {
                pActions.PlayerActions.Possess.performed += Possess;
            }
        }

    }

    private void Possess(InputAction.CallbackContext c)
    {
        
        if(transform.childCount == playerChildCount)
        {
            //Sets pile with character until player unpossesses
            transform.position = new Vector3(currentSkeletonPile.position.x, transform.position.y, currentSkeletonPile.position.z);
            transform.rotation = currentSkeletonPile.rotation;
            //currentSkeletonPile.rotation = transform.rotation;
            currentSkeletonPile.parent = transform;

            //currentSkeletonPile.position = new Vector3(currentSkeletonPile.position.x, currentSkeletonPile.position.y - 0.5f, currentSkeletonPile.position.z);
            currentSkeletonPile.GetComponent<Collider>().enabled = false;
            currentSkeletonPile.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

            //Player becomes Skeleton
            bpossessSkel = true;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            currentSkeletonPile.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                                                                
                                //Make sure it is the actual skeleton for gameobject child index
            currentSkeletonPile.GetChild(0).gameObject.SetActive(true);

            //Makes player not be able to move through gates
            gameObject.layer = LayerMask.NameToLayer("Physical");

            GetComponent<PlayerSound>().PossessBonesSound();

        }
        else
        {
            //currentSkeletonPile.GetChild(1).GetComponent<Collider>().enabled = true;
            currentSkeletonPile.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
                                //Make sure it is the actual skeleton for gameobject child index
            currentSkeletonPile.GetChild(0).gameObject.SetActive(false);

            //Player control back
            bpossessSkel = false;
            currentSkeletonPile.GetComponent<Collider>().enabled = true;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            currentSkeletonPile.parent = null;
            //resets position and rotation
            currentSkeletonPile.rotation = Quaternion.Euler(0, 180, 0);
            //currentSkeletonPile.position = new Vector3(currentSkeletonPile.position.x, currentSkeletonPile.position.y - 0.5f, currentSkeletonPile.position.z);
            pActions.PlayerActions.Possess.performed -= Possess;

            //allows player to pass trough walls again
            gameObject.layer = LayerMask.NameToLayer("Phase");

            GetComponent<PlayerSound>().DropBonesSound();
        }
        
    }

    public void KillSkeleton()
    {
        currentSkeletonPile.GetComponent<Collider>().enabled = true;
        currentSkeletonPile.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        currentSkeletonPile.GetChild(0).gameObject.SetActive(false);

        //Player control back
        bpossessSkel = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        //moves pile back a bit after a trap
        currentSkeletonPile.localPosition = new Vector3(currentSkeletonPile.localPosition.x, currentSkeletonPile.localPosition.y, currentSkeletonPile.localPosition.z - 1);

        currentSkeletonPile.parent = null;
        pActions.PlayerActions.Possess.performed -= Possess;

        //allows player to pass trough walls again
        gameObject.layer = LayerMask.NameToLayer("Phase");
    }

    private void Whistle(InputAction.CallbackContext c)
    {
        if(!bwhistling)
        {
            StartCoroutine(WhistleCoolDown(Random.Range(1, 6)));
        }
    }

    IEnumerator WhistleCoolDown(int soundVal)
    {
        bwhistling = true;
        GetComponent<PlayerSound>().PlayerWistle();
        yield return new WaitForSeconds(1f);
        bwhistling = false;
    }

    private void ResetPlayer()
    {
        if(bresetPlayer)
        {
            pActions.PlayerActions.Possess.performed -= Possess;

            //CController is strict
            cController.enabled = false;
            cController.transform.position = resetLocation;
            if(cController.transform.position == resetLocation)
            {
                cController.enabled = true;
                bresetPlayer = false;
            }
            
        }
    }

}
