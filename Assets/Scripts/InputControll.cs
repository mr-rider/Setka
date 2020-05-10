using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class InputControll : MonoBehaviour
{
    public InputField inputColomns;
    public InputField inputRows;
    private int colomns;
    private int rows;
    

    public Text warningText;


    private CreateMesh CreateMeshScript;


    // Start is called before the first frame update
    void Start()
    {
        inputColomns = inputColomns.GetComponent<InputField>();
        inputRows = inputRows.GetComponent<InputField>();


        GameObject MeshController = GameObject.Find("MeshController");
        CreateMeshScript = MeshController.GetComponent<CreateMesh>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckInputFields()
    {
        // проверка введеных данных колонки
        if (string.IsNullOrEmpty(inputColomns.text))
        {
            warningText.text = "Пустое поле 'Колонки'";
            warningText.gameObject.SetActive(true);
        }
        else if(Convert.ToInt32(inputColomns.text) == 0)
        {
            warningText.text = "'Колонки' = 0";
            warningText.gameObject.SetActive(true);
        }
        else
        {
            warningText.gameObject.SetActive(false);
            colomns = Convert.ToInt32(inputColomns.text);
            CreateMeshScript.columnCount = colomns;
          
        }


        // проверка введеных данных строки
        if (string.IsNullOrEmpty(inputRows.text))
        {
            warningText.text = "Пустое поле 'Строки'";
            warningText.gameObject.SetActive(true);
        }
        else if (Convert.ToInt32(inputRows.text) == 0)
        {
            warningText.text = "'Строки' = 0";
            warningText.gameObject.SetActive(true);
        }
        else
        {
            warningText.gameObject.SetActive(false);
            rows = Convert.ToInt32(inputRows.text);
            CreateMeshScript.rowCount = rows;
        }



        Debug.Log(" Колонки = " + colomns);
       
        
        Debug.Log(" Строки = " + rows); 

    }
}
