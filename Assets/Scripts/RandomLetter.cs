using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomLetter : MonoBehaviour
{
    public Text letter;

    // Start is called before the first frame update
    void Start()
    {
        letter = gameObject.GetComponent<Text>();
        
        char c = (char)('A' +  Random.Range(0, 26)); // генерируем случайную букву по знацению ASCII

        letter.text =(""+ c);
        Debug.Log(c);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
