
using SwiftMessageReader.Exceptions;

namespace SwiftMessageReader.Helpers
{
    public static class StringToModelParser
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
                SwiftLogger.Error(Messages.WrongMessageStructure);
                throw new WrongMessageStructureException(Messages.WrongMessageStructure);
            }
        }
    }
}
