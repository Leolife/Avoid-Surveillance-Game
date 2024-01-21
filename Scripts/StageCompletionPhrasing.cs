using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class StageCompletionPhrasing : MonoBehaviour
{
    Dictionary<int, string> phraseList = new Dictionary<int, string>();

    public TextMeshProUGUI completionPhrase;

    void Start()
    {
        loadFile();
    }

    private void loadFile()
    {
        //function to load phrases into dictionary
        List<string> phrases = new List<string>();

        StreamReader streamReader = new StreamReader(@"Completion_Phrases.txt");

        int fileIn = 0;

        while (streamReader.Peek() >= 0)
        {
            phrases.Add(streamReader.ReadLine());
            fileIn++;
        }

        streamReader.Close();

        for (int i = 0; i < phrases.Count; i++)
        {
            phraseList.Add(i, phrases[i]);
        }
    }

    public void pickPhrase()
    {
        //function to select phrase to display
        System.Random random = new System.Random();
        int rand = random.Next(0, phraseList.Count);
        completionPhrase.text = phraseList[rand];
    }
}
