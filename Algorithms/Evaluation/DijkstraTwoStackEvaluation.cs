using System;
using AST = DataStructures;

namespace Algorithms.Evaluation
{
    /// <summary>
    /// Evaluates arithmetic expressions that are fully parenthesized, with
    /// numbers and characters separated by whitespace.
    /// </summary>
    public static class DijkstraTwoStackEvaluation
    {
        public static Double Evaluate(String expression)
        {
            AST.Stack<String> operations = new  AST.Stack<String>();
            AST.Stack<Double> values = new AST.Stack<Double>();

            String[] tokens = expression.Split(" ");

            foreach (String token in tokens)
            {
                switch (token)
                {
                    case "(":
                        break;

                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    case "sqrt":
                        operations.Push(token);
                        break;

                    case ")":
                        // Pop, evaluate, and push result if token is ")".
                        String operation = operations.Pop();
                        Double value = values.Pop();

                        switch (operation)
                        {
                            case "+":
                                value = values.Pop() + value;
                                break;

                            case "-":
                                value = values.Pop() - value;
                                break;

                            case "*":
                                value = values.Pop() * value;
                                break;

                            case "/":
                                value = values.Pop() / value;
                                break;

                            case "sqrt":
                                value = Math.Sqrt(value);
                                break;
                        }

                        values.Push(value);
                        break;

                    default:
                        // If token is not operator or parentheses: push double value.
                        values.Push(Double.Parse(token));
                        break;
                }
            }

            return values.Pop();
        }
    }
}
