using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextUpdate : MonoBehaviour
{
	public Text displayText;
	private static String[] filePaths = {"private.txt", "public.txt", "privateprivate.txt", "privatepublic.txt", "publicpublic.txt", "publicprivate.txt", "privateprivateprivate.txt", "privateprivatepublic.txt", "privatepublicpublic.txt", "publicpublicpublic.txt", "publicpublicprivate.txt", "publicprivateprivate.txt"};
    // Start is called before the first frame update
    void Start()
    {
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.anyKey) {
			SceneManager.LoadScene(0);
		}
    }

	void SetCountText() {
		String path = "Assets/Text/";
		path += filePaths[PlayerStats.level];
		StreamReader reader = new StreamReader(path);
		displayText.text = reader.ReadToEnd();
		reader.Close();
	}
}
