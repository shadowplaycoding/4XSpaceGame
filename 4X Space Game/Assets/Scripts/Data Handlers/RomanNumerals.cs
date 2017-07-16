using UnityEngine;
using System;
using System.Collections;

public class RomanNumerals {

    public static string RomanNumeralGenerator(int number)
    {
        string roman = "";

        while (number > 0)
        {
            if (number >= 1000)
            {
                roman = roman + "M";
                number = number - 1000;
            }
            else if (number >= 900)
            {
                roman = roman + "CM";
                number = number - 900;
            }
            else if (number >= 500)
            {
                roman = roman + "D";
                number = number - 500;
            }
            else if (number >= 400)
            {
                roman = roman + "CD";
                number = number - 400;
            }
            else if (number >= 100)
            {
                roman = roman + "C";
                number = number - 100;
            }
            else if (number >= 90)
            {
                roman = roman + "XC";
                number = number - 90;
            }
            else if (number >= 50)
            {
                roman = roman + "L";
                number = number - 50;
            }
            else if (number >= 40)
            {
                roman = roman + "XL";
                number = number - 40;
            }
            else if (number > 10)
            {
                roman = roman + "X";
                number = number - 10;
            }
            else if (number <= 10)
            {
                roman = roman + NumeralsOneToTen(number);
                number = 0;
            }
        }

        return roman;
    }

    public static string NumeralsOneToTen(int number)
    {
        string roman;

        switch (number)
        {
            case 1:
                roman = "I";
                break;
            case 2:
                roman = "II";
                break;
            case 3:
                roman = "III";
                break;
            case 4:
                roman = "IV";
                break;
            case 5:
                roman = "V";
                break;
            case 6:
                roman = "VI";
                break;
            case 7:
                roman = "VII";
                break;
            case 8:
                roman = "VIII";
                break;
            case 9:
                roman = "IX";
                break;
            case 10:
                roman = "X";
                break;
            default:
                roman = number.ToString();
                break;
        }

        return roman;
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}

