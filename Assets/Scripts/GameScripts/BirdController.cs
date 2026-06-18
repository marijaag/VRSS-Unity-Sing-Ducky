using UnityEngine;

public class BirdController : MonoBehaviour
{
    private float speed = 1.2f;
    public Animator animator;
    private bool kliknuta = false;
    private Vector3 target;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (kliknuta) return;

        transform.position = Vector3.MoveTowards(
            transform.position, 
            target, 
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.1f)
            Destroy(gameObject);
    }

    void OnMouseDown()
    {
        if (kliknuta) return;
        kliknuta = true;

        SimonSaysManager manager = FindAnyObjectByType<SimonSaysManager>();
        manager.BonusPtica();

        Destroy(gameObject); 
    }

    public void SetTarget(Vector3 noviTarget)
{
    target = noviTarget;
}
}
