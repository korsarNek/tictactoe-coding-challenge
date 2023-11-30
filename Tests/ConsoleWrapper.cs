using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Tictactoe;

namespace Tests;

class StringConsoleWrapper : IConsoleWrapper
{
    private List<string> lines = new List<string>();
    private int cursorTop = 0;
    private int cursorLeft = 0;

    public bool CursorVisible { set {} }

    public void Clear()
    {
        lines.Clear();
        cursorTop = 0;
        cursorLeft = 0;
    }

    public (int left, int top) GetCursorPosition()
    {
        return (cursorLeft, cursorTop);
    }

    public void SetCursorPosition(int left, int top)
    {
        if (top >= lines.Count)
            top = lines.Count;
        if (top < 0)
            top = 0;

        if (left >= lines[top].Length)
            left = lines[top].Length;
        if (left < 0)
            left = 0;

        cursorTop = top;
        cursorLeft = left;
    }

    public void Write(string text)
    {
        foreach (var line in text.Split('\n'))
        {
            if (cursorTop == lines.Count)
                lines.Add(line);
            else
            {
                var toRemove = Math.Min(lines[cursorTop].Length - cursorLeft, line.Length);
                lines[cursorTop] = lines[cursorTop].Remove(cursorLeft, toRemove).Insert(cursorLeft, line);
            }

            cursorLeft = lines[cursorTop].Length;
            cursorTop++;
        }
        cursorTop--;
    }

    public void Write(char text)
    {
        Write(text.ToString());
    }

    public void WriteLine(string text)
    {
        Write(text + "\n");
    }

    public override string ToString()
    {
        return string.Join('\n', lines);
    }
}