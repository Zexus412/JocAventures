using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

    public class StoryNode
    {

        public string history;
        public string[] answers;
        public StoryNode[] nextNode;
        public bool isFinal = false;
            public delegate void NodeVisited();
            public NodeVisited nodeVisited;
    }

    public Text historyText;
    public Transform answersParent;
    public GameObject buttonAnswer;
    private StoryNode current;
 
	// Use this for initialization
	void Start () {
        current = StoryFiller.FillStory();
        historyText.text = "";
        FillUI();
		
	}
	
	// Update is called once per frame
	void AnswerSelected (int index) {
        print(index);
        historyText.text += "\n" + current.answers[index];
        if (!current.isFinal)
        {
            current = current.nextNode[index];
            FillUI();

        }
        else
        {
            //Final de partida
            Application.LoadLevel("GameOver");
        }
	}

    void FillUI()
    {
        historyText.text += "\n" + "\n" + current.history;
        foreach (Transform child in answersParent.transform)
        {
            Destroy(child.gameObject);
        }

        bool isLeft = true;
        float height = 50;
        int index = 0;
        foreach (string answer in current.answers)
        {
            GameObject buttonAnswerCopy = Instantiate(buttonAnswer);
            buttonAnswerCopy.transform.parent = answersParent;
            float x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.1f;

            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(isLeft ? x : -x, height, 0);
            if (!isLeft) height = buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 2.5f;

            isLeft = !isLeft;
            FillListener(buttonAnswerCopy.GetComponent<Button>(), index);
            buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;

            index++;

        }
    }



        void FillListener(Button button, int index)
        {
            button.onClick.AddListener(() =>
            {
                AnswerSelected(index);
            });
        }


         void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            FinishGameMenu();

    }

    void FinishGameMenu()
    {
        //pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Pantalla inicial");
    }

}
