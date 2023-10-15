using System.Text.RegularExpressions;

using SwiftMessageReader.Exceptions;

namespace SwiftMessageReader.Helpers
{
    public static class MessageStructureVerifiers
    {
        public static void CurlyBracketsVerifier(string input)
        {
            var stack = new Stack<char>();
            var isEven = true;

            foreach (char ch in input)
            {
                if (ch == '{')
                {
                    stack.Push(ch);
                }
                if (ch == '}')
                {
                    if (stack.Count == 0)
                    {
                        isEven = false;
                    }

                    stack.Pop();
                }
            }
            if (stack.Count != 0)
            {
                isEven = false;
            }

            if (isEven == false)
            {
                SwiftLogger.Error(Messages.WrongBracketsSequence);
                throw new WrongBracketsSequence(Messages.WrongBracketsSequence);
            }
        }

        public static void TagVerifier(string text)
        {
            var pattern = "^\\d\\:\\n?\\:\\d{2}\\:[0-9A-Z-]*\\s?\\n?:\\d{2}\\:[0-9A-Z-]*\\s?\\n?:\\d{2}\\:[\\x20-\\x2F\\x3A-\\x40A-Z0-9\\n]+[-]?$";
            text = text.Replace("\r", "");
            var isMatched = Regex.IsMatch(text, pattern);

            if (!isMatched)
            {
                SwiftLogger.Error(Messages.WrongMessageStructure);
                throw new WrongMessageStructure(Messages.WrongMessageStructure);
            }
        }
    }
}