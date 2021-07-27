using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform barrelTransform;
    [SerializeField] Transform bulletParent;
    [SerializeField] LayerMask shootLayerCollision;
    [SerializeField] float bulletHitMissDistance;

    Animator animator;
    Transform cameraTransform;
    InputAction shootAction;
    PlayerInput playerInput;
    int shootAnimationParameterId;

    void Awake()
    {
        getDependencies();
        getInputAction();
        getHashAnimationParameter();
    }

    void OnEnable()
    {
        shootAction.performed += _ => shootGun();
    }

    void OnDisable()
    {
        shootAction.performed -= _ => shootGun();
    }

    void getDependencies()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
    }

    void getInputAction()
    {
        shootAction = playerInput.actions["Shoot"];
    }

    void getHashAnimationParameter()
    {
        shootAnimationParameterId = Animator.StringToHash("Shoot");
    }


    void shootGun()
    {
        setAnimationParameter();

        Bullet bullet = createBullet();

        RaycastHit hit;
        bool hitOnLayer = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, shootLayerCollision);

        Vector3 hitPos = getAPosFarFromTheCamera();
        if (hitOnLayer) hitPos = hit.point;

        initBullet(bullet, hitPos, hitOnLayer);
    }

    void setAnimationParameter()
    {
        animator.SetTrigger(shootAnimationParameterId);
    }

    Bullet createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        Bullet bulletController = bullet.GetComponent<Bullet>();
        return bulletController;
    }

    Vector3 getAPosFarFromTheCamera()
    {
        return cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
    }

    void initBullet(Bullet bullet, Vector3 hitPos, bool hit)
    {
        bullet.targetPos = hitPos;
        bullet.hit = hit;
    }
}
