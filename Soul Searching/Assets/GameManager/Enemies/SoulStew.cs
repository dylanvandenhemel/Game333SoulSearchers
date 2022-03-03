using System.Collections;
using UnityEngine;

public class SoulStew : MonoBehaviour
{
    [Tooltip("Float representing the distance the player needs to be for the stew to emerge.")]
    public float emergeDistance = 7;
    [Tooltip("Float representing the distance the player needs to be for the stew to prep.")]
    public float prepDistance = 3;
    [Tooltip("Float representing the distance the player needs to be for the stew to attack.")]
    public float attackDistance = 2;
    [Tooltip("Transform of the red eyerange area, which will become equal to the attack distance.")]
    public Transform eyeRange;
    [Tooltip("Transform of the hand, which will move to watch the player.")]
    public Transform hand;
    [Tooltip("AnimatorClip of the hiding animation, so we can get the length.")]
    public AnimationClip hidingAnimation;
    private Transform player;
    private Animator animator;
    private Vector3 alteredPos;
    private RaycastHit raycastHit;
    private bool hidden = true, hiding = false, attacking = false;
    private const float EYE_RANGE_MULTIPLIER = 2.1f, EYE_RANGE_HEIGHT = .5f, RAYCAST_ADJUSTMENT = 1, HAND_ADJUSTMENT = .185f;

    private int previousStateNum = 0;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        eyeRange.localScale += new Vector3(attackDistance * EYE_RANGE_MULTIPLIER, EYE_RANGE_HEIGHT, attackDistance * EYE_RANGE_MULTIPLIER);
        alteredPos = new Vector3(transform.position.x, transform.position.y + RAYCAST_ADJUSTMENT, transform.position.z);
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;

        //Looks strange without moving the hand, it clips and pivots oddly otherwise
        hand.LookAt(player);
        hand.localPosition = Vector3.zero + Vector3.back * HAND_ADJUSTMENT;



        
    //Set state based on distance
        int stateNum = 0;
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if(!attacking)
        {
            if(dist <= attackDistance)
            {
                stateNum = 3;
            }
            else if(dist <= prepDistance)
            {
                stateNum = 2;
            }
            else if(dist <= emergeDistance)
            {
                stateNum = 1;
            }
            else
            {
                stateNum = 0;
            }
            animator.SetInteger("StateNum", stateNum);
        }
        

        
        //Bool to prevent transition repeating to Emerge from Idle
        animator.SetBool("Idling", previousStateNum >= 1);

        previousStateNum = stateNum;


        
        
        //Raycast to see if player is in emerge range and in line of sight
        if (Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, emergeDistance, LayerMask.GetMask("Phase", "Wall")) && raycastHit.collider.gameObject.CompareTag("Player") && !player.GetComponent<Player>().bpossessSkel)
        {
            hidden = false;

            //If close enough to attack, then attack
            if (!attacking && Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, attackDistance, LayerMask.GetMask("Phase", "Wall")) && raycastHit.collider.gameObject.CompareTag("Player") && !player.GetComponent<Player>().bpossessSkel)
                StewAttack();
        }

        //Stay hidden if player is too far away or out of sight
        else if (!attacking)
        {
            if (!hidden)
            {
                StopCoroutine("HidingCoroutine");
                StartCoroutine("HidingCoroutine");
            }
            if (!hiding)
            {
                //animator.Play("Emerge", 0, 0);
            }
        }

        Debug.Log(raycastHit.collider);

        //Ensure animator follows transitions
        //animator.SetBool("Prepped", Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, prepDistance, LayerMask.GetMask("Phase", "Wall")) && raycastHit.collider.gameObject.CompareTag("Player") && !player.GetComponent<Player>().bpossessSkel);
    }

    private void StewAttack()
    {
        //animator.SetTrigger("Attack");
        animator.SetInteger("StateNum", 3);
        previousStateNum = 3;
        attacking = true;
        player.GetComponent<Player>().bresetPlayer = true;
        player.GetComponent<ResetDelegate>().bcallReset = true;
        StartCoroutine(StopAttack());
    }
    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(0.5f);
        
        attacking = false;
    }

    //Slightly strange, but necessary due to the way the "default" state is just a loop of the 0th frame of emerging
    private IEnumerator HidingCoroutine()
    {
        hidden = true;
        hiding = true;
        //animator.SetTrigger("Hidden");
        yield return new WaitForSeconds(hidingAnimation.length);
        hiding = false;
    }
}

