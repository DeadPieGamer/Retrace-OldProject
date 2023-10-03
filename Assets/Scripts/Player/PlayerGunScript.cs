using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour
{
    // The camera
    private Camera cam;
    // The different layers the raycast shall ignore when searching for walls
    [SerializeField] private LayerMask ignoreMe;
    // The two bullet prefabs
    public GameObject bulletPrefabWeak;
    public GameObject bulletPrefabStronk;
    // The box to show where the bullet will spawn
    public GameObject rayPreviewPrefab;
    // The shooting sfx
    public GameObject shootSFXPrefab;
    // The full charging time to make a powerful bullet
    [SerializeField] private float chargeUp = 0.8f;
    // Two pri
    private float currentCharge;
    private bool allowShoot;

    // Here to allow for pausing, and to not allow shooting while paused
    [SerializeField] private GameManager gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        currentCharge = chargeUp;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks whether the player hits the pause/fullscreen buttons and makes the gameManager either pause the game or toggle fullscreen
        if (Input.GetButtonDown("Pause"))
        {
            gameMaster.Paused();
        }

        if (Input.GetButtonDown("FullscreenToggle"))
        {
            gameMaster.ToggleFullScreen();
        }

        // Shoots a raycast (Direct line with no thickness) from the camera
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        // Checks whether the raycast hits a wall and the game isn't paused
        if (Physics.Raycast(ray, out hit, 1000f, ~ignoreMe) && Time.timeScale != 0)
        {
            // Creates the box and the location the bullet will spawn
            if (Input.GetButtonDown("Fire1"))
            {
                // Spawns the preview box on the wall you're aiming at
                Instantiate(rayPreviewPrefab, hit.point, Quaternion.identity);
                // Tells the script that you're allowed to shoot
                allowShoot = true;
            }

            // Decreases the current charge until it reaches 0, if you're holding down the shoot button and are allowed to shoot
            if (Input.GetButton("Fire1") && currentCharge > 0f && allowShoot)
            {
                currentCharge -= Time.deltaTime;
                if (currentCharge <= 0f)
                {
                    // Debug tells me the shot is fully charged
                    Debug.Log("Fully Charged");
                }
            }

            // Checks if the player lifts their finger from the trigger and if the player is allowed to shoot
            if (Input.GetButtonUp("Fire1") && allowShoot)
            {
                // Checks if the current charge is under or equal to zero. If it is, then the bullet shot will be the buffed up one
                if (currentCharge <= 0f)
                {
                    // Runs the shooting function and tells it the bullet should be buffed
                    ShootyShoot(true);
                    // Resets the charge-time
                    currentCharge = chargeUp;
                } // If the current charge isn't under or equal to zero, this will run
                else
                {
                    // Runs the shooting function and tells it the bullet should be normal
                    ShootyShoot(false);
                    // Resets the charge-time
                    currentCharge = chargeUp;
                }
            } // If the player lifts their finger without being allowed to shoot, this will run
            else if (Input.GetButtonUp("Fire1"))
            {
                // Resets the charge-time
                currentCharge = chargeUp;
                // Says the player isn't allowed to shoot
                allowShoot = false;
            }
        } // If the raycast doesn't hit a wall, this will run
        else
        {
            // Says the player isn't allowed to shoot
            allowShoot = false;
        }
    }

    // The function that summons a bullet. Has a bool to check whether the bullet is the normal or strong one
    void ShootyShoot(bool isStronk)
    {
        // Shoots a ray from the camera
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        // Checks that the raycast hit something
        if (Physics.Raycast(ray, out hit, 1000f, ~ignoreMe))
        {
            // Checks if the bullet summoned should be strong or normal
            if (isStronk)
            {
                // Instantiates strong bullet at shooting position
                Instantiate(bulletPrefabStronk, hit.point, Quaternion.identity);
            }
            else
            {
                // Instantiates normal bullet at shooting position
                Instantiate(bulletPrefabWeak, hit.point, Quaternion.identity);
            }
            // Instantiates shooting SFX at player position
            Instantiate(shootSFXPrefab, transform.position, Quaternion.identity);
        }
    }
}
