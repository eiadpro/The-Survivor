using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] Transform avatar;


    [SerializeField] GameObject hand;
    public  int HP = 100;
    private Animator animator;
    private NavMeshAgent agent;
    bool die = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>(); 

    }
    void Update()
    {

        if (HP > 0)
            transform.LookAt(new Vector3(avatar.position.x, transform.position.y, avatar.position.z));

    }
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            int random = Random.Range(0, 2);
            if (random == 0) {
                animator.SetTrigger("DIE1");
                die = true;
            }
            else if (random == 1)
            {
                animator.SetTrigger("DIE2");
                die = true;
            }
            else
            {
                animator.SetTrigger("DIE1");
                die = true;
            }
            transform.GetComponent<CapsuleCollider>().enabled = false;
            HP = 0;
            animator.SetTrigger("DIE1");
            hand.SetActive(false);
            SoundManager.Instance.zombiechannel.PlayOneShot(SoundManager.Instance.zombieDeath);
        }
        else
        {
            animator.SetTrigger("DAMAGE");
           
            SoundManager.Instance.zombiechannel.PlayOneShot(SoundManager.Instance.zombieHurt);
        }
    }
    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("nar"))
        {
            TakeDamage(7);
            print("aqaqa");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("nar"))
        {
            TakeDamage(7);
            print("aqaqa");
        }
    }
}
