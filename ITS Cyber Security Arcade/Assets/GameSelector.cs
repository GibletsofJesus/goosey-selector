using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class GameSelector : MonoBehaviour {

    public AudioSource[] musics;
    public Renderer[] backgrounds;
    public Image[] gameImages;
    public Image fadeMeOut;
    public Animator[] ImageAnimators;
    public string[] gameTitles;
	void Start ()
    {
        ImageAnimators[0].Play("in_idle");
    }

    float lastMoveTimer;
    int gameIndex;

    // Update is called once per frame
    void Update()
    {
        if (fadeMeOut.color.a > 0)
        {
            Color col = fadeMeOut.color;
            col.a -= Time.deltaTime * 2;
            fadeMeOut.color = col;
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && lastMoveTimer <= 0)
        {
            StartCoroutine(fadeThings(gameIndex, false));
            if (Input.GetAxis("Horizontal") > 0)
            {
                gameIndex++;
                if (gameIndex > 2)
                    gameIndex = 0;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                gameIndex--;
                if (gameIndex < 0)
                    gameIndex = 2;
            }

            StartCoroutine(fadeThings(gameIndex, true));
            lastMoveTimer = 0.25f;
        }
        if (lastMoveTimer > 0)
            lastMoveTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            //Process.Start(gameTitles[gameIndex]);
            Application.Quit();
        }
    }

    IEnumerator fadeThings(int index,bool inOut)//True in, false out
    {
        ImageAnimators[index].Play(inOut?"pop_in" : "pop_out");
        if (inOut)
            musics[index].Play(); 
        float lerpy = 0;
        while (lerpy < 1)
        {            
            lerpy += Time.deltaTime*3;

            Color col_ = gameImages[index].color;
            col_.a = Mathf.Lerp(inOut ? 0:1, inOut ? 1 : 0, lerpy * 2);
            gameImages[index].color = col_;

            musics[index].volume = Mathf.Lerp(inOut? 0 : 1, inOut ? 1 : 0, lerpy);

            Color col = backgrounds[index].material.GetColor("_TintColor");
            col.a = Mathf.Lerp(inOut ? 0 : .5f, inOut ? 0.5f : 0, lerpy);
            backgrounds[index].material.SetColor("_TintColor",col);

            yield return new WaitForEndOfFrame();
        }
        if (!inOut)
        {
            musics[index].Pause();
        }

    }
}
