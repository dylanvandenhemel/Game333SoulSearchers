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
    [Tooltip("Float representing the time between the attack animation and the player being reset.")]
    public float attackTime = .4f;
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

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        eyeRange.localScale += new Vector3(attackDistance * 2, .5f, attackDistance * 2);
        alteredPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;

        //Looks strange without moving the hand, it clips and pivots oddly otherwise
        hand.LookAt(player);
        hand.localPosition = new Vector3(0, 0, 0) + Vector3.back * .185f;

        //Raycast to see if player is in emerge range and in line of sight
        //Player will probably need to become his own layer
        if (Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, emergeDistance) && raycastHit.collider.gameObject.CompareTag("Player") && !player.GetComponent<Player>().bpossessSkel)
        {
            hidden = false;

            //If close enough to attack, then attack
            if (!attacking && Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, attackDistance) && raycastHit.collider.gameObject.CompareTag("Player") && !player.GetComponent<Player>().bpossessSkel)
                StartCoroutine("StewAttack");
        }

        //Stay hidden if player is too far away or out of sight
        else
        {
            if (!hidden)
            {
                StopAllCoroutines();
                StartCoroutine("HidingCoroutine");
            }
            if (!hiding)
                animator.Play("Emerge", 0, 0);
        }

        //Ensure animator follows transitions
        animator.SetBool("Prepped", Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, prepDistance) && raycastHit.collider.gameObject.CompareTag("Player") && !player.GetComponent<Player>().bpossessSkel);
    }

    private IEnumerator StewAttack()
    {
        animator.SetTrigger("Attack");
        attacking = true;
        yield return new WaitForSeconds(attackTime);
        player.GetComponent<Player>().bresetPlayer = true;
        player.GetComponent<ResetDelegate>().bcallReset = true;
        attacking = false;
    }

    //Slightly strange, but necessary due to the way the "default" state is just a loop of the 0th frame of emerging
    private IEnumerator HidingCoroutine()
    {
        hidden = true;
        hiding = true;
        animator.Play("Hide", 0);
        yield return new WaitForSeconds(hidingAnimation.length);
        hiding = false;
    }
}

