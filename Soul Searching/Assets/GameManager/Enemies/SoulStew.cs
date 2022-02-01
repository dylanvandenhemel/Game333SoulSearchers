using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("AnimatorClip of the hiding animation, so we can get the length.")]
    public AnimationClip hidingAnimation;
    private Transform player;
    private Animator animator;
    private Vector3 alteredPos;
    private RaycastHit raycastHit;
    private bool hidden = true, hiding = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        eyeRange.localScale += new Vector3(attackDistance * 2, .5f, attackDistance * 2);
        alteredPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        //Raycast to see if player is in emerge range and in line of sight
        //Player will probably need to become his own layer
        if (Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, emergeDistance) && raycastHit.collider.gameObject.CompareTag("Player"))
        {
            hidden = false;

            //If close enough to attack, then attack
            if (Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, attackDistance) && raycastHit.collider.gameObject.CompareTag("Player"))
                StewAttack();
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
        animator.SetBool("Prepped", Physics.Raycast(alteredPos, player.position - alteredPos, out raycastHit, prepDistance) && raycastHit.collider.gameObject.CompareTag("Player"));
    }

    private void StewAttack()
    {
        animator.SetTrigger("Attack");
        player.GetComponent<Player>().bresetPlayer = true;
        player.GetComponent<ResetDelegate>().bcallReset = true;
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

