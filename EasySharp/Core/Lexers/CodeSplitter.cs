using System.Text;

namespace EasySharp.Core.Lexers;

public class CodeSplitter(string source)
{
    #region private

    private int _line = 0;
    private int _columnStart = 0;
    private int _columnEnd = 0;
    private CodeToken _codeToken = CodeToken.None;

    private bool Match(int index, char first, char second)
    {
        if (index < 0 || index >= source.Length - 1) return false;
        return source[index] == first && source[index + 1] == second;
    }

    void NextLine()
    {
        _line++;
        _columnStart = 0;
        _columnEnd = -1;
    }

    #endregion

    #region public

    public IEnumerable<Code> Split()
    {
        _line = 1;
        _columnStart = 0;
        _columnEnd = -1;
        _codeToken = CodeToken.None;
        StringBuilder value = new StringBuilder();
        for (int index = 0; index < source.Length; index++)
        {
            #region Get Char

            char c = source[index];

            #endregion

            #region New Line

            if (c == '\n')
            {
                NextLine();
                continue;
            }

            #endregion

            #region Line Step

            _columnEnd++;

            #endregion

            #region WhiteSpace

            if (char.IsWhiteSpace(c))
            {
                if (value.Length > 0)
                {
                    yield return new Code(_line, _columnStart, _columnEnd, value.ToString(), _codeToken);
                    value.Clear();
                    _codeToken = CodeToken.None;
                    _columnStart = _columnEnd + 1;
                }
                else
                {
                    _columnStart++;   
                }

                continue;
            }

            #endregion

            #region Split Different Tokens

            CodeToken newCodeToken = Code.GetToken(c);
            if (!Code.Equals(_codeToken, newCodeToken))
            {
                if (value.Length > 0)
                {
                    yield return new Code(_line, _columnStart, _columnEnd, value.ToString(), _codeToken);
                    value.Clear();
                    _columnStart = _columnEnd + 1;
                }
                else
                {
                    _columnStart++;
                }

                _codeToken = newCodeToken;
            }

            #endregion

            value.Append(c);
        }

        if (value.Length > 0)
        {
            yield return new Code(_line, _columnStart, _columnEnd, value.ToString(), _codeToken);
        }
    }

    // public IEnumerable<Code> Split()
    // {
    //     int line = 1;
    //     int columnStart = 1;
    //     int columnEnd = 0;
    //     StringBuilder value = new StringBuilder();
    //     CodeCharType lastCodeToken = CodeCharType.None;
    //     bool inComment = false;
    //     bool ignoreNext = false;
    //
    //     void NextLine()
    //     {
    //         line++;
    //         columnStart = 1;
    //         columnEnd = 0;
    //     }
    //
    //     for (var index = 0; index < source.Length; index++)
    //     {
    //         var c = source[index];
    //         if (c == '\n')
    //         {
    //             NextLine();
    //             ignoreNext = false;
    //             continue;
    //         }
    //
    //         if (ignoreNext)
    //         {
    //             ignoreNext = false;
    //             columnStart++;
    //             continue;
    //         }
    //
    //         if (inComment)
    //         {
    //             inComment = !Match(index, '*', '/');
    //             if (inComment)
    //             {
    //                 columnStart++;
    //                 continue;
    //             }
    //
    //             ignoreNext = true;
    //             continue;
    //         }
    //
    //         if (Match(index, '\\', '\\'))
    //         {
    //             NextLine();
    //         }
    //         else if (Match(index, '\\', '*'))
    //         {
    //             inComment = true;
    //         }
    //         else
    //         {
    //             if (char.IsWhiteSpace(c))
    //             {
    //                 string valueStr = value.ToString();
    //                 if (!string.IsNullOrEmpty(valueStr))
    //                 {
    //                     yield return new Code(line, columnStart, columnEnd, valueStr, CodeToken.Normal);
    //                     value.Clear();
    //                     columnStart = columnEnd + 1;
    //                 }
    //
    //                 // 跳过本字符
    //                 columnStart++;
    //             }
    //             else if (Token.Separators.Contains(c))
    //             {
    //                 string valueStr = value.ToString();
    //                 if (!string.IsNullOrEmpty(valueStr))
    //                 {
    //                     yield return new Code(line, columnStart, columnEnd, valueStr, CodeToken.Normal);
    //                     value.Clear();
    //                     columnStart = columnEnd + 1;
    //                 }
    //
    //                 yield return new Code(line, columnStart, columnStart, c.ToString(), CodeToken.Separator);
    //
    //                 // 跳过本字符
    //                 columnStart++;
    //             }
    //             else
    //             {
    //                 if (lastCodeToken != CodeCharType.None)
    //                 {
    //                     var currentCodeToken = GetCodeToken(c);
    //                     if (currentCodeToken != lastCodeToken)
    //                     {
    //                         string valueStr = value.ToString();
    //                         if (!string.IsNullOrEmpty(valueStr))
    //                         {
    //                             yield return new Code(line, columnStart, columnEnd, valueStr,
    //                                 lastCodeToken == CodeCharType.Normal ? CodeToken.Normal : CodeToken.Operator);
    //                             value.Clear();
    //                             columnStart = columnEnd + 1;
    //                         }
    //                     }
    //                 }
    //
    //                 value.Append(c);
    //                 lastCodeToken = GetCodeToken(c);
    //             }
    //
    //             columnEnd++;
    //         }
    //     }
    //
    //     string valueStr2 = value.ToString();
    //     if (!string.IsNullOrEmpty(valueStr2))
    //     {
    //         yield return new Code(line, columnStart, columnEnd, valueStr2, CodeToken.Normal);
    //     }
    // }

    #endregion
}