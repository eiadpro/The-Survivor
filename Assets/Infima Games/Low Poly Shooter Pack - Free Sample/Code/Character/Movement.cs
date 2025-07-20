// Copyright 2021, Infima Games. All Rights Reserved.

using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfimaGames.LowPolyShooterPack
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class Movement : MovementBehaviour
    {
        #region FIELDS SERIALIZED



        public GameObject bloodyscreen;
        [SerializeField] GameObject zombies;
        [SerializeField] Weapon w;
        [SerializeField] Transform keytaken;
        [SerializeField] Transform target1;
        [SerializeField] Transform target2;
        [SerializeField] Transform robo;
        [SerializeField] GameObject[] waygate;
        [SerializeField] GameObject[] waygatekey;
        [SerializeField] float speedgate = 5f;
        [SerializeField] TextMeshProUGUI th;
        [SerializeField] Transform gate1;
        [SerializeField] Transform gatekey1;
        [SerializeField] AudioSource a4;
        [SerializeField] AudioSource a6;
        [SerializeField] AudioSource a7;
        [SerializeField] AudioSource af1;
        [SerializeField] AudioSource af2;
        [SerializeField] AudioSource af3;
        [SerializeField] AudioSource af4;
        [SerializeField] AudioSource af5;
        [SerializeField] AudioSource af6;
        [SerializeField] AudioSource af7;
        [SerializeField] AudioSource af8;

        int t = 0;
        public int HP = 100;
        public bool isDead;
        int currentgate = 0;
        int currentgatekey = 0;
        bool once = true;
        bool oncea6 = true;
        bool once6 = true;
        bool oncea7 = true;
        bool gateopen = false;
        bool gatekeyopen = false;
        bool mission = false;
        bool missions = false;
        bool gatekey = false;
        bool stop = false;
        bool onceaf1 = false;
        [SerializeField] Transform block1, block2;
        [SerializeField] Transform blockway1, blockway2, blockway11, blockway22;
        [SerializeField] Transform target;
        [Header("Audio Clips")]

        [Tooltip("The audio clip that is played while walking.")]
        [SerializeField]
        private AudioClip audioClipWalking;
        public AudioSource _jumpSound;

        [Tooltip("The audio clip that is played while running.")]
        [SerializeField]
        private AudioClip audioClipRunning;

        [Header("Speeds")]

        [SerializeField]
        private float speedWalking = 5.0f;

        [Tooltip("How fast the player moves while running."), SerializeField]
        private float speedRunning = 9.0f;
        public float jumpForce = 10;
        public float count = 0;
        [SerializeField] Transform trap;
        [SerializeField] Transform waytrap;
        public bool gate=false;
        public bool keycol = false;
        public bool havekey = false;
        #endregion

        #region PROPERTIES

        //Velocity.
        private Vector3 Velocity
        {
            //Getter.
            get => rigidBody.velocity;
            //Setter.
            set => rigidBody.velocity = value;
        }

        #endregion

        #region FIELDS

        /// <summary>
        /// Attached Rigidbody.
        /// </summary>
        private Rigidbody rigidBody;
        /// <summary>
        /// Attached CapsuleCollider.
        /// </summary>
        private CapsuleCollider capsule;
        [SerializeField] LayerMask wal;
        /// <summary>
        /// Attached AudioSource.
        /// </summary>
        private AudioSource audioSource;

        /// <summary>
        /// True if the character is currently grounded.
        /// </summary>
        private bool grounded;
        private bool isfalling;
        private bool bullet;
        public bool gun1;
        public bool havegun1;
        private bool button=false;
        private bool traping = false;
        /// <summary>
        /// Player Character.
        /// </summary>
        private CharacterBehaviour playerCharacter;
        /// <summary>
        /// The player character's equipped weapon.
        /// </summary>
        private WeaponBehaviour equippedWeapon;

        /// <summary>
        /// Array of RaycastHits used for ground checking.
        /// </summary>
        private readonly RaycastHit[] groundHits = new RaycastHit[8];

        #endregion

        #region UNITY FUNCTIONS

        /// <summary>
        /// Awake.
        /// </summary>
        protected override void Awake()
        {
            //Get Player Character.
            playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();
        }

        /// Initializes the FpsController on start.
        protected override void Start()
        {
            //Rigidbody Setup.
            
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            //Cache the CapsuleCollider.
            capsule = GetComponent<CapsuleCollider>();

            //Audio Source Setup.
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClipWalking;
            audioSource.loop = true;
        }

        /// Checks if the character is on the ground.
        private void OnCollisionStay()
        {
            //Bounds.
            Bounds bounds = capsule.bounds;
            //Extents.
            Vector3 extents = bounds.extents;
            //Radius.
            float radius = extents.x - 0.01f;

            //Cast. This checks whether there is indeed ground, or not.
            Physics.SphereCastNonAlloc(bounds.center, radius, Vector3.down,
                groundHits, extents.y - radius * 0.5f, ~0, QueryTriggerInteraction.Ignore);

            //We can ignore the rest if we don't have any proper hits.
            if (!groundHits.Any(hit => hit.collider != null && hit.collider != capsule))
                return;

            //Store RaycastHits.
            for (var i = 0; i < groundHits.Length; i++)
                groundHits[i] = new RaycastHit();

            //Set grounded. Now we know for sure that we're grounded.
            grounded = true;
        }

        protected override void FixedUpdate()
        {
            //Move.
            MoveCharacter();

            //Unground.
            grounded = false;
        }

        /// Moves the camera to the character, processes jumping and plays sounds every frame.
       public void time()
        {
            mission = false;
            missions = false;
        }
        public void move()
        {
            traping = false;
            stop= false;
        }

        protected override void Update()
        {
            
            if (HP < 30)
            {
                if (!onceaf1)
                {
                    af1.Play();
                    onceaf1 = true;
                }
                recover();
            }
            else
            {
                onceaf1 = false;
                af1.Pause();
            }
            th.text = "Health : " + HP;
            if (transform.position.y <= -48f)
            {
                target.GetComponent<TargetScript>().die();
            }
            if (transform.position.z >=86.5f&&oncea6)
            {
                a6.Play();
                oncea6 = false;
            }
            if (keycol&&!target1.GetComponent<Target>().enabled&&!target2.GetComponent<Target>().enabled && Input.GetKeyDown(KeyCode.E))
            {
                havekey = true;
                af4.Play();
                keytaken.GetComponent<MeshRenderer>().enabled = false;
            }
            if (havekey && gatekey && Input.GetKeyDown(KeyCode.E))
            {
                af2.Play();
                gatekeyopen = true;
                currentgatekey++;
                if (currentgatekey >= waygatekey.Length)
                {
                    currentgatekey = 0;
                }
            }
            if (!havekey && gatekey && Input.GetKeyDown(KeyCode.E))
            {
                af3.Play();
            }


                if (gatekeyopen)
            {


                gatekey1.position = Vector3.MoveTowards(gatekey1.position, waygatekey[currentgatekey].transform.position, speedgate * Time.deltaTime);
                if (oncea7)
                {
                    a7.Play();
                    oncea7 = false;
                }
            }
            print("m" + mission);
            if(missions && robo.position.x >= 9f){
                mission = true;
            }
            if (mission&&robo.position.x >= 9f&&target.GetComponent<TargetScript>().GetCount()==0)
            {
                print("m" + "sadasdasdasd");
                block1.position= Vector3.MoveTowards(block1.position, blockway1.transform.position, 20 * Time.deltaTime);
                block2.position= Vector3.MoveTowards(block2.position, blockway2.transform.position, 20 * Time.deltaTime);
                Invoke(nameof(time), 1.5f);
            }
            if (gateopen)
            {


                gate1.position = Vector3.MoveTowards(gate1.position, waygate[currentgate].transform.position, speedgate * Time.deltaTime);
                if (once)
                {
                    a4.Play();
                    once = false;
                }
            }
            if (gate && Input.GetKeyDown(KeyCode.E))
            {
                af2.Play();
                gateopen=true;
                currentgate++;
                if (currentgate >= waygate.Length)
                {
                    currentgate = 0;
                }
            }
            if (traping)
            {
                trap.position = Vector3.MoveTowards(trap.position, waytrap.position, 15*Time.deltaTime);
                stop = true;
                Invoke(nameof(move), 2f);
            }
            if (Input.GetKey(KeyCode.Space) && grounded)
            {

                rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);

                if (_jumpSound)
                    _jumpSound.Play();

            }
            if (bullet && Input.GetKeyDown(KeyCode.E))
            {
                if (w.Getbullets() != 180)
                    af7.Play();
                else
                    af8.Play();
                w.incbullets();
                
                target2.GetComponent<Target>().enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.E) && gun1)
            {
                if (once6)
                {
                    af6.Play();
                    once6 = false;
                }
                
                havegun1 = true;
                zombies.SetActive(true);
                target1.GetComponent<Target>().enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.E) && button)
            {
                traping = true;
                af5.Play();
            }
                
            //Get the equipped weapon!
            equippedWeapon = playerCharacter.GetInventory().GetEquipped();

            //Play Sounds!
            PlayFootstepSounds();
        }

        #endregion

        #region METHODS

        private void MoveCharacter()
        {
            #region Calculate Movement Velocity

            //Get Movement Input!
            Vector2 frameInput = playerCharacter.GetInputMovement();
            //Calculate local-space direction by using the player's input.
            var movement = new Vector3(frameInput.x, 0.0f, frameInput.y);

            //Running speed calculation.
            if (playerCharacter.IsRunning())
                movement *= speedRunning;
            else
            {
                //Multiply by the normal walking speed.
                movement *= speedWalking;
            }

            //World space velocity calculation. This allows us to add it to the rigidbody's velocity properly.
            movement = transform.TransformDirection(movement);

            #endregion
            print("fall" + isfalling + grounded);
            //Update Velocity.
            if (!isfalling || grounded)
                if(!stop)
                Velocity = new Vector3(movement.x, rigidBody.velocity.y, movement.z);
        }

        /// <summary>
        /// Plays Footstep Sounds. This code is slightly old, so may not be great, but it functions alright-y!
        /// </summary>
        private void PlayFootstepSounds()
        {
            //Check if we're moving on the ground. We don't need footsteps in the air.
            if (grounded && rigidBody.velocity.sqrMagnitude > 0.1f)
            {
                //Select the correct audio clip to play.
                audioSource.clip = playerCharacter.IsRunning() ? audioClipRunning : audioClipWalking;
                //Play it!
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            //Pause it if we're doing something like flying, or not moving!
            else if (audioSource.isPlaying)
                audioSource.Pause();
        }
        public void TakeDamage(int damageAmount)
        {
            HP -= damageAmount;
            
            if (HP <= 0)
            {
                print("PlayerDead");
                target.GetComponent<TargetScript>().die();
                isDead = true;
                SoundManager.Instance.playerchannel.PlayOneShot(SoundManager.Instance.playerdeath);

            }
            else
            {
                print("PlayerHit");
                SoundManager.Instance.playerchannel.PlayOneShot(SoundManager.Instance.playerhurt);
               
                    StartCoroutine(bloody_screen());
              
               
            }


        }
        private IEnumerator bloody_screen()
        {
            if (bloodyscreen.activeInHierarchy == false)
            {
                bloodyscreen.SetActive(true);
            }
            var image = bloodyscreen.GetComponentInChildren<UnityEngine.UI.Image>();

            // Set the initial alpha value to 1 (fully visible).
            Color startColor = image.color;
            startColor.a = 1f;
            image.color = startColor;

            float duration = 3f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                // Calculate the new alpha value using Lerp.
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

                // Update the color with the new alpha value.
                Color newColor = image.color;
                newColor.a = alpha;
                image.color = newColor;

                // Increment the elapsed time.
                elapsedTime += Time.deltaTime;

                yield return null; ; // Wait for the next frame.
            }
            if (bloodyscreen.activeInHierarchy == true)
            {
                bloodyscreen.SetActive(false);
            }
        }
        public void win()
        {
            target.GetComponent<TargetScript>().initial();
            SceneManager.LoadScene(2);
        }
        public void recover()
        {
            t++;
            if(HP<30&&t>1000)
            HP += 1;
            if (HP >= 30)
            {
                t = 0;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("cube"))
            {
                isfalling = true;
            }
            if (other.transform.CompareTag("cub"))
            {
                isfalling = true;
                gate = true;
            }
            if (other.transform.CompareTag("bullet"))
            {
                isfalling = true;
                bullet = true;
            }
            if (other.transform.CompareTag("button"))
            {
                isfalling = true;
                button= true;
            }
            if (other.transform.CompareTag("gun1"))
            {
                isfalling = true;
                gun1 = true;


            }
            if (other.transform.CompareTag("gun2"))
            {
                isfalling = true;


            }
            if (other.transform.CompareTag("mission"))
            {
                missions = true;
            }
            if (other.transform.CompareTag("key"))
            {
                keycol = true;
            }
            if (other.transform.CompareTag("withkey"))
            {
                gatekey = true;
            }
            if (other.CompareTag("ZombieHand"))
            {
                if (isDead == false)
                {
                    TakeDamage(10);
                }
            }
            if (other.transform.CompareTag("Finish"))
            {
                win();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag("cube"))
            {
                isfalling = false;
            }
            if (other.transform.CompareTag("bullet"))
            {
                bullet = false;
                isfalling = false;
            }
            if (other.transform.CompareTag("gun1"))
            {
                isfalling = false;
                gun1 = false;


            }
            if (other.transform.CompareTag("gun2"))
            {
                isfalling = false;


            }
            if (other.transform.CompareTag("button"))
            {
                isfalling = false;
                button = false;
            }
            if (other.transform.CompareTag("cub"))
            {
                isfalling = false;
                gate = false;
            }

            if (other.transform.CompareTag("key"))
            {
                keycol = false;
            }
            if (other.transform.CompareTag("withkey"))
            {
                gatekey = false;
            }
            if (other.transform.CompareTag("mission"))
            {
                missions = false;
            }
            #endregion
        }
    }
}