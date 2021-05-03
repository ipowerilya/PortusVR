using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    public Text bufferText;
    public FloatEvent OnInsert;

    float buffer = 0;
    float operand = 0;
    string operation = null;
    string inputString = "0";
    bool opChain = false;
    bool eqChain = false;

    int maxLength = 10;
    bool negative = false;

    static readonly HashSet<string> unaryOp = new HashSet<string> { "1/x", "^2", "sqrt" };


    void Start()
    {
        UpdateUI();
    }

    public void AddDigit(string str)
    {
        Debug.Assert(str.Length == 1);
        if (opChain || eqChain) 
        {
            eqChain = opChain = false;
            inputString = "0";
        }
        if (inputString.Length >= maxLength) return;

        char c = str[0];
        if (c == '.')
        {
            if (inputString.Contains(".")) return;
            if (inputString.Length == 0) inputString = "0";
            inputString += ".";
        }
        else if (!Char.IsDigit(c)) return;
        else
        {
            inputString = (inputString + c.ToString()).TrimStart('0');
        }
        inputString = inputString.Length != 0 ? inputString : "0";
        UpdateUI();
    }

    public void Command(string command)
    {
        switch(command)
        {
            case "CE":
                inputString = "0";
                break;
            case "C":
                buffer = 0;
                operand = 0;
                inputString = "0";
                operation = null;
                break;
            case "BS":
                if (inputString.Length > 0) inputString = inputString.Substring(0, inputString.Length - 1);
                break;
            case "+/-":
                negative = !negative;
                break;
            case "=":
                if (operation == null) break;
                if (!eqChain) operand = Parse(inputString);
                else buffer = Parse(inputString);
                eqChain = true;
                Eval();
                break;
            default:
                throw new Exception("Unknown command: " + command);
        }
        UpdateUI();
    }

    float Parse(string input)
    {
        return float.Parse(input, System.Globalization.NumberStyles.Float);
    }

    public void Operator(string op)
    {
        if (unaryOp.Contains(op))
        {
            buffer = Parse(inputString);
            operation = op;
            opChain = true;
            Eval();
            operation = null;
        }
        else
        {
            if (operation == null)
            {
                buffer = Parse(inputString);
                operation = op;
                opChain = true;
            }
            else
            {
                if (eqChain)
                {
                    eqChain = false;
                    opChain = true;
                    operation = op;
                    buffer = Parse(inputString);
                }
                else if (opChain)
                {
                    operation = op;
                }
                else
                {
                    operand = Parse(inputString);
                    opChain = true;
                    Eval();
                    operation = op;
                }
            }
        }
        UpdateUI();
    }

    public void Eval()
    {
        switch (operation)
        {
            case "%":
                buffer *= operand / 100f;
                break;
            case "1/x":
                buffer = 1f / buffer;
                break;
            case "^2":
                buffer *= buffer;
                break;
            case "sqrt":
                buffer = Mathf.Sqrt(buffer);
                break;
            case "/":
                buffer /= operand;
                break;
            case "x":
                buffer *= operand;
                break;
            case "-":
                buffer -= operand;
                break;
            case "+":
                buffer += operand;
                break;
            default:
                throw new Exception("Unknown operator: " + operation);
        }
        inputString = buffer.ToString("G" + maxLength.ToString());
    }

    void UpdateUI()
    {
        bufferText.text = (negative ? "-" : "") + (inputString.StartsWith(".") ? "0" : "") + (inputString.Length == 0 ? "0" : inputString);
    }

    public void Insert()
    {
        OnInsert.Invoke(Parse(inputString));
    }
}
