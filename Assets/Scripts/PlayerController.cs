using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public float speed;
    public int score;
    public Text scoreText;
    public int currentLevel;

    public AudioClip PointSound;
    public AudioClip BottomSound;
    public AudioClip BumpSound;
    public AudioClip GoalSound;
    public AudioClip StartSound;

    private float volLow = .5f;
    private float volHigh = 1.0f;

    private Rigidbody rb;
    private Vector3 startPosition;

    private float count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        score = 120;
        SetScoreText();

        AudioSource.PlayClipAtPoint(StartSound, transform.position, 1.0f);
    }

    void FixedUpdate()
    {
        count += Time.deltaTime;
        if (count >= 1)
        {
            count = 0;
            score -= 1;
            SetScoreText();
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PointsCollectable"))
        {
            float volume = Random.Range(volLow, volHigh);
            AudioSource.PlayClipAtPoint(PointSound, transform.position, volume);
            other.gameObject.SetActive(false);
            score += 10;
            SetScoreText();
        }     
        else if (other.gameObject.CompareTag("Bottom"))
        {
            score -= 30;
            gameObject.transform.position = startPosition;
            rb.velocity = Vector3.zero;
            AudioSource.PlayClipAtPoint(BottomSound, transform.position, 1.0f);
        }
        else if (other.gameObject.CompareTag("GoalCollectable"))
        {
            AudioSource.PlayClipAtPoint(GoalSound, transform.position, 1.0f);
            other.gameObject.SetActive(false);
            currentLevel += 1;
            SceneSwitcher(); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pendulum"))
        {
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            AudioSource.PlayClipAtPoint(BumpSound, transform.position, 1.0f);
            rb.AddForce(dir * 500);
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void SceneSwitcher()
    {
        StartCoroutine(WaitForNewScene());
    }

    IEnumerator WaitForNewScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(currentLevel);
    }
}
