using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text QuestionText;
    public InputField InputText;
    public Text StatusText;
    public Button SubmitButton;

    enum Operand
    {
        PlusMinus,
        Multiply
    }

    int X;
    int Y;
    Operand Op;
    int Answer;

    static int MinN = -10;
    static int MaxN = 10;

    // Start is called before the first frame update
    void Start()
    {
        SubmitButton.onClick.AddListener(SubmitAnswer);
        GenerateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateQuestion()
    {
        StatusText.text = "";
        InputText.text = "";

        var Ops = Enum.GetValues(typeof(Operand));
        Op = (Operand)Ops.GetValue(UnityEngine.Random.Range(0, Ops.Length));
        string opStr = "";

        switch(Op)
        {
            case Operand.PlusMinus:
                X = UnityEngine.Random.Range(MinN, MaxN);
                Y = UnityEngine.Random.Range(MinN, MaxN);
                Answer = X + Y;
                opStr = Y >= 0 ? "+" : "-";
                break;
            case Operand.Multiply:
                X = UnityEngine.Random.Range(0, MaxN);
                Y = UnityEngine.Random.Range(0, MaxN);
                Answer = X * Y;
                opStr = "x";
                break;
            default:
                break;
        }

        QuestionText.text = string.Format("{0} {1} {2} =", X, opStr, Math.Abs(Y));

        Debug.Log(string.Format("X:{0} Y:{1} Operand:{2} Answer:{3}", X, Y, Op, Answer));
    }

    public void SubmitAnswer()
    {
        if(String.IsNullOrEmpty(InputText.text))
        {
            StatusText.text = "Missing answer";
        }

        int result = 0;
        if( Int32.TryParse(InputText.text, out result) )
        {
            if( result == Answer)
            {
                StatusText.text = "Correct!";
                StartCoroutine(RestartCoroutine());
            } else
            {
                StatusText.text = "Incorrect, Try Again.";
            }
        }else
        {
            StatusText.text = "Missing answer";
        }
    }

    IEnumerator RestartCoroutine()
    {
        SubmitButton.enabled = false;
        yield return new WaitForSeconds(3f);
        GenerateQuestion();
        SubmitButton.enabled = true;
    }
}
