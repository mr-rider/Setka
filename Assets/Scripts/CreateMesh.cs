using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateMesh : MonoBehaviour
{
    public int rowCount = 1; // количество строк
    public int columnCount = 1; // количество столбцов

    public Transform letterPrefab; // префаб буквы
    public Transform canvasParent;
    private Transform letter;
    private bool meshIsReady = false; // таблица создана

    public List<Transform> letters = new List<Transform>(); // массив созданых букв

    public bool canMove = false; // можно ли начать перемешивание
    public bool isMove = false; // закончилось ли перемешивание
    public int nextLetter = 0; // индекс для перемешивания
    public Transform firstLetter;
    public Transform secondLetter;
    public Transform target1;
    public Transform target2;
    public Transform emptyPrefab;
    public float speed; // скорость перемещения букв


    // Start is called before the first frame update
    void Start()
    {
       // MeshGenerator();
        
                            
    }

    public void MeshGenerator()
    {
        if(meshIsReady != true) //создаем таблицу, если ее не существует
        {
            Debug.Log("Screen width (x) " + Screen.width + "Screen hight (y) " + Screen.height);

            // Получаем координаты левого верхнего угла
            float xLeftCorner = Screen.width / 2;
            if (columnCount > 1)
            {
                xLeftCorner = xLeftCorner - columnCount / 2 * 20;
            }
            float yLeftCorner = Screen.height / 2;
            if (rowCount > 1)
            {
                yLeftCorner = yLeftCorner + (rowCount / 2) * 26;
            }


            if (columnCount % 2 == 0)  //если четное число столбцов + 10
            {
                Debug.Log("Четное столбцы");
                xLeftCorner += 10;
            }

            if (rowCount % 2 == 0)
            {
                Debug.Log("Четное строки");
                yLeftCorner -= 13;
            }

            float offsetX = xLeftCorner;
            float offsetY = yLeftCorner;

            // Заполняем сетку
            for (int j = 1; j <= rowCount; j++)
            {
                for (int i = 1; i <= columnCount; i++)
                {

                    letter = Instantiate(letterPrefab, canvasParent, true);
                    letter.transform.position = new Vector3(offsetX, offsetY, 0);
                    letters.Add(letter);  // добавляем новую ячейку в список
                    offsetX = offsetX + 20;
                    if (i == columnCount)
                    {
                        offsetX = xLeftCorner;
                    }
                }
                offsetY = offsetY - 26;
            }
            meshIsReady = true; // таблица создана
        }

            

        
    }

    // перемешивание букв
    public void LetterMove()
    {
        isMove = true; // перемещение началось
        Debug.Log(" Перемешать");
          // Меняем две буквы местами   
        firstLetter.transform.position = Vector3.MoveTowards(firstLetter.position, target2.position, speed * Time.deltaTime);
        secondLetter.transform.position = Vector3.MoveTowards(secondLetter.position, target1.position, speed * Time.deltaTime);
        float distance = Vector3.Distance(firstLetter.transform.position, target2.position); // дистанция между буквами
        
        if(distance == 0)
        {
            Debug.Log(" Следующая буква");
            isMove = false; // перемещение пары букв закончено
            if(nextLetter < (letters.Count-1)) // все буквы перемещены?
            {
                nextLetter += 1;
                Debug.Log(" Next letter =" + nextLetter);
            }
           
        }
    }

    // вычисляем скорость перемещения текущей пары букв
    public void SpeedCalculate()
    {
        speed =  Vector3.Distance(firstLetter.transform.position, secondLetter.position) / 2;
    }

   

    public void SetCanMove()
    {
        if (meshIsReady && letters.Count > 1) // таблица создана и букв больше, чем одна
        {
            canMove = true; // запускаем перемешивание
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if( (isMove == false) && (nextLetter < letters.Count-1))
            {
                firstLetter = letters[nextLetter];
                if (letters.Count == 2) // если букв 2 отключаем Random
                {
                    secondLetter = letters[1];
                }
                else
                {
                    secondLetter = letters[Random.Range(0, letters.Count - 1)]; // выбираем вторую букву из списка случайным образом
                }
               

                target1 = Instantiate(emptyPrefab, canvasParent, true);
                target1.transform.position = firstLetter.position;

                target2 = Instantiate(emptyPrefab, canvasParent, true);
                target2.transform.position = secondLetter.position;

                SpeedCalculate(); // вычисление скорости перемещения
                Debug.Log(" Speed = " + speed);

               
                
               
            }else if((isMove == false) && (nextLetter == (letters.Count - 1))){

                canMove = false; // Все буквы перемешаны
                Debug.Log("Все буквы перемещены");

            }
            LetterMove();
            

        }

       
    }
}
