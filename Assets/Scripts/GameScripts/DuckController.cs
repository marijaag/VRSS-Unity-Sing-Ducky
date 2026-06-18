using UnityEngine;
using System.Collections;
public class DuckController : MonoBehaviour
{
    public int duckID;
    public Animator animator;
    private SimonSaysManager gameManager;
    private bool isClickable = false;

    void Start()
    {
        gameManager = FindAnyObjectByType<SimonSaysManager>();
        animator = GetComponentInChildren<Animator>();
    }

    public IEnumerator PlaySequenceAnimation()
    {
        animator.SetTrigger("Zapevaj");
        yield return new WaitForSeconds(0.3f);
        noteParticles.Play();
        StartCoroutine(PlayAudioWithDelay());
       // yield return new WaitForSeconds(1.2f);
    }

    public void SetClickable(bool clickable)
    {
        isClickable = clickable;
    }

    public ParticleSystem noteParticles;
    public AudioSource audioSource;

    void OnMouseDown()
    {
        if (!isClickable) return;
        gameManager.PlayerClickedDuck(duckID);
        animator.SetTrigger("Zapevaj");
       
       noteParticles.Play();
        //StartCoroutine(PlayNoteWithDelay());
        StartCoroutine(PlayAudioWithDelay());

    }

    IEnumerator PlayNoteWithDelay()
{
    yield return new WaitForSeconds(0.1f); 
    noteParticles.Play();
}

 IEnumerator PlayAudioWithDelay()
{
    yield return new WaitForSeconds(0.2f); 
    audioSource.Play();
}
}