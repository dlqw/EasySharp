using System.Text;

namespace EasySharp.Core.Lexers;

internal class CodeSplitter(string source)
{
    #region private

    private enum CodeCharType
    {
        None,
        Normal,
        Special,
    }
    
    private CodeCharType GetCodeToken(char c)
    {
        if (char.IsLetterOrDigit(c) || c == '_')
        {
            return CodeCharType.Normal;
        }

        if (Token.OperatorsChars.Contains(c))
        {
            return CodeCharType.Special;
        }

        return CodeCharType.Normal;
    }

    private bool Match(int index, char first, char second)
    {
        if (index < 0 || index >= source.Length - 1) return false;
        return source[index] == first && source[index + 1] == second;
    }

    #endregion

    #region public

    public IEnumerable<Code> Split()
    {
        int line = 1;
        int columnStart = 1;
        int columnEnd = 0;
        StringBuilder value = new StringBuilder();
        CodeCharType lastCodeToken = CodeCharType.None;
        bool inComment = false;
        bool ignoreNext = false;

        void NextLine()
        {
            line++;
            columnStart = 1;
            columnEnd = 0;
        }

        for (var index = 0; index < source.Length; index++)
        {
            var c = source[index];
            if (c == '\n')
            {
                NextLine();
                ignoreNext = false;
                continue;
            }
            if (ignoreNext)
            {
                ignoreNext = false;
                columnStart++;
                continue;
            }
            if (inComment)
            {
                inComment = !Match(index, '*', '/');
                if (inComment)
                {
                    columnStart++;
                    continue;
                }

                ignoreNext = true;
                continue;
            }
            if (Match(index, '\\', '\\'))
            {
                NextLine();
            }
            else if (Match(index, '\\', '*'))
            {
                inComment = true;
            }
            else
            {
                if (char.IsWhiteSpace(c))
                {
                    string valueStr = value.ToString();
                    if (!string.IsNullOrEmpty(valueStr))
                    {
                        yield return new Code(line, columnStart, columnEnd, valueStr, CodeToken.Normal);
                        value.Clear();
                        columnStart = columnEnd + 1;
                    }

                    // 跳过本字符
                    columnStart++;
                }
                else if (Token.Separators.Contains(c))
                {
                    string valueStr = value.ToString();
                    if (!string.IsNullOrEmpty(valueStr))
                    {
                        yield return new Code(line, columnStart, columnEnd, valueStr, CodeToken.Normal);
                        value.Clear();
                        columnStart = columnEnd + 1;
                    }

                    yield return new Code(line, columnStart, columnStart, c.ToString(), CodeToken.Separator);
                    
                    // 跳过本字符
                    columnStart++;
                }
                else
                {
                    if(lastCodeToken != CodeCharType.None)
                    {
                        var currentCodeToken = GetCodeToken(c);
                        if(currentCodeToken != lastCodeToken)
                        {
                            string valueStr = value.ToString();
                            if (!string.IsNullOrEmpty(valueStr))
                            {
                                yield return new Code(line, columnStart, columnEnd, valueStr, lastCodeToken == CodeCharType.Normal ? CodeToken.Normal : CodeToken.Operator);
                                value.Clear();
                                columnStart = columnEnd + 1;
                            }
                        }
                    }
                    value.Append(c);
                    lastCodeToken = GetCodeToken(c);
                }

                columnEnd++;
            }
        }

        string valueStr2 = value.ToString();
        if (!string.IsNullOrEmpty(valueStr2))
        {
            yield return new Code(line, columnStart, columnEnd, valueStr2, CodeToken.Normal);
        }
    }

    #endregion
}