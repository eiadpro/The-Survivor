using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetScript : MonoBehaviour
{
    [SerializeField] Transform robo;
    [SerializeField] Transform target;
    [SerializeField] Transform target1;
    [SerializeField] Transform target2;
    [SerializeField] Transform block1;
    [SerializeField] Transform block2;
    [SerializeField] Transform blockway1;
    [SerializeField] Transform blockway2;
    [SerializeField] Transform blockofTrap;
    [SerializeField] TextMeshProUGUI tc;
    [SerializeField] TextMeshProUGUI ts;
    [SerializeField] TextMeshProUGUI ti;
    [SerializeField] Transform opendoor;
    [SerializeField] AudioSource a5;
    [SerializeField] AudioSource a8;
    float randomTime;
    float sec = 0;
    bool routineStarted = false;
    bool oncea8 = true;
    //Used to check if the target has been hit
    public bool isHit = false;

    [Header("Customizable Options")]
    //Minimum time before the target goes back up
    public float minTime;
    //Maximum time before the target goes back up
    public float maxTime;
    public static int count = 0;

    [Header("Audio")]
    public AudioClip upSound;
    public AudioClip downSound;

    [Header("Animations")]
    public AnimationClip targetUp;
    public AnimationClip targetDown;

    public AudioSource audioSource;
    bool once=false;

    public void die()
    {
        count = 0;
        SceneManager.LoadScene(3);
    }
    public void initial()
    {
        count = 0;
    }
    public int GetCount()
    {
       return count;
    }
    private void Update()
    {
        if (count == 14)
        {
            if (!once&&target1!=null)
            {
                target1.GetComponent<Target>().enabled = true;
                target2.GetComponent<Target>().enabled = true;
                robo.position = new Vector3(-12.48f, -18.935f, 73.35f);
                if(a8 != null)
                a5.Play();
            }
            once = true;
            if (opendoor != null)
            {
                if(opendoor.rotation.y<=0.73f)
                opendoor.Rotate(Vector3.up, 0.75f);
                print(opendoor.rotation.y);
            }
            
        }
        if (tc != null)
        {
            if(count>10)
            tc.text = (count - 1).ToString();
        else
                tc.text = "0"+(count - 1).ToString();
        }
        //Generate random time based on min and max time values
        if (count >= 2 && count < 14)
        {
            if (oncea8&&a8!=null)
            {
                a8.Play();
                oncea8 = false;
            }
            if (ts != null)
            {
                ti.GetComponent<TextMeshProUGUI>().enabled = true;
                ts.GetComponent<TextMeshProUGUI>().enabled = true;
                tc.GetComponent<TextMeshProUGUI>().enabled = true;
                if(sec<60)
                sec += Time.deltaTime;

                if (sec <= 50)
                    ts.text = (60 - System.Math.Round(sec, 2)).ToString();
                else if (sec < 60)
                {

                    ts.text = "0" + (System.Math.Round(60 - sec, 2)).ToString();

                }
                else
                {

                    ts.text = "00.00";
                    die();
                }
                print((60 - System.Math.Round(sec, 2)).ToString());
            }
            print(sec);
        }
        else
        {
            if (ts != null)
            {
                ti.GetComponent<TextMeshProUGUI>().enabled = false;
                ts.GetComponent<TextMeshProUGUI>().enabled = false;
                tc.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
        randomTime = Random.Range(minTime, maxTime);
        //If the target is hit
        if (isHit)
        {
            if (block1 != null)
            {
                block1.position = Vector3.MoveTowards(block1.position, blockway1.transform.position, 7 * Time.deltaTime);
                block2.position = Vector3.MoveTowards(block2.position, blockway2.transform.position, 7 * Time.deltaTime);
                blockofTrap.GetComponent<MeshRenderer>().enabled = true;
                blockofTrap.GetComponent<Rigidbody>().useGravity = true;
            }
            if (transform.GetComponent<way>() != null)
            {
                transform.GetComponent<way>().enabled = false;


            }

            print(count);


        }
        if (isHit == true)
        {
            if (routineStarted == false)
            {
                //Animate the target "down"
                gameObject.GetComponent<Animation>().clip = targetDown;
                gameObject.GetComponent<Animation>().Play();

                //Set the downSound as current sound, and play it
                audioSource.GetComponent<AudioSource>().clip = downSound;
                audioSource.Play();

                //Start the timer
                //StartCoroutine(DelayTimer());
                routineStarted = true;
                if (target != null)
                    target.GetComponent<Target>().enabled = false;
                count++;

            }

        }
    }

    //Time before the target pops back up
    private IEnumerator DelayTimer()
    {
        //Wait for random amount of time
        yield return new WaitForSeconds(randomTime);
        //Animate the target "up" 
        gameObject.GetComponent<Animation>().clip = targetUp;
        gameObject.GetComponent<Animation>().Play();

        //Set the upSound as current sound, and play it
        audioSource.GetComponent<AudioSource>().clip = upSound;
        audioSource.Play();

        //Target is no longer hit
        isHit = false;
        routineStarted = false;
    }
}