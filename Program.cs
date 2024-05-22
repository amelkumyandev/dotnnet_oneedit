using System;

public class OneEditAway
{
    public static bool IsOneEditAway(string stringOne, string stringTwo)
    {
        if (stringOne == stringTwo)
            return true;

        int len1 = stringOne.Length;
        int len2 = stringTwo.Length;

        if (Math.Abs(len1 - len2) > 1)
            return false;

        ReadOnlySpan<char> span1 = stringOne.AsSpan();
        ReadOnlySpan<char> span2 = stringTwo.AsSpan();

        if (len1 == len2)
        {
            return OneReplaceAway(span1, span2);
        }
        else if (len1 + 1 == len2)
        {
            return OneInsertOrRemoveAway(span1, span2);
        }
        else if (len1 - 1 == len2)
        {
            return OneInsertOrRemoveAway(span2, span1);
        }

        return false;
    }

    private static bool OneReplaceAway(ReadOnlySpan<char> span1, ReadOnlySpan<char> span2)
    {
        bool foundDifference = false;

        for (int i = 0; i < span1.Length; i++)
        {
            if (span1[i] != span2[i])
            {
                if (foundDifference)
                    return false;

                foundDifference = true;
            }
        }

        return true;
    }

    private static bool OneInsertOrRemoveAway(ReadOnlySpan<char> shorter, ReadOnlySpan<char> longer)
    {
        int index1 = 0, index2 = 0;

        while (index1 < shorter.Length && index2 < longer.Length)
        {
            if (shorter[index1] != longer[index2])
            {
                if (index1 != index2)
                    return false;

                index2++;
            }
            else
            {
                index1++;
                index2++;
            }
        }

        return true;
    }
    
    public static void Main()
    {
        Console.WriteLine(IsOneEditAway("pale", "ple"));    // True
        Console.WriteLine(IsOneEditAway("pales", "pale"));  // True
        Console.WriteLine(IsOneEditAway("pale", "bale"));   // True
        Console.WriteLine(IsOneEditAway("pale", "bake"));   // False
    }
}
