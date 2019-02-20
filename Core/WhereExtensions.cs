using System;
using System.Linq.Expressions;

public static class WhereExtensions
{
    public static SqlBuilder<T> Where<T>(this SqlBuilder<T> builder, Expression<Func<T, bool>> expression)
    {
        string Analysis(Expression e)
        {
            if (e is BinaryExpression binaryExpression)
            {
                string @operator = ExpressionTypeToOperator(binaryExpression.NodeType);
                return $"({Analysis(binaryExpression.Left)} {@operator} {Analysis(binaryExpression.Right)}) ";
            }
            else if (e is ConstantExpression constantExpression)
            {
                bool addApostrophe = e.Type.Name == "String" || e.Type.Name == "DateTime";
                return addApostrophe ? $"'{constantExpression.Value}'" : e.ToString();
            }
            else if (e is MemberExpression memberExpression)
            {
                return $"[{memberExpression.Member.Name}]";
            }
            else throw new NotImplementedException("未实现此表达式转换");
        }

        string Func()
        {
            string sql = Analysis(expression.Body);
            return sql;
        }

        builder.AddUnit((nameof(Where), Func));

        return builder;
    }

    private static string ExpressionTypeToOperator(ExpressionType expressionType)
    {
        switch (expressionType)
        {
            case ExpressionType.Add:
                return "AND";

            case ExpressionType.AddAssign:
                return "AND";

            case ExpressionType.AddAssignChecked:
                return "AND";

            case ExpressionType.AddChecked:
                return "AND";

            case ExpressionType.And:
                return "AND";

            case ExpressionType.AndAlso:
                return "AND";

            case ExpressionType.AndAssign:
                return "AND";

            case ExpressionType.ArrayIndex:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.ArrayLength:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Assign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Block:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Call:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Coalesce:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Conditional:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Constant:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Convert:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.ConvertChecked:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.DebugInfo:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Decrement:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Default:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Divide:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.DivideAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Dynamic:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Equal:
                return "=";

            case ExpressionType.ExclusiveOr:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.ExclusiveOrAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Extension:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Goto:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.GreaterThan:
                return ">";

            case ExpressionType.GreaterThanOrEqual:
                return ">=";

            case ExpressionType.Increment:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Index:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Invoke:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.IsFalse:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.IsTrue:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Label:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Lambda:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.LeftShift:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.LeftShiftAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.LessThan:
                return "<";

            case ExpressionType.LessThanOrEqual:
                return "<=";

            case ExpressionType.ListInit:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Loop:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.MemberAccess:
                throw new NotImplementedException("未实现此表达式转换");
            case ExpressionType.MemberInit:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Modulo:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.ModuloAssign:
                throw new NotImplementedException("未实现此表达式转换");
            case ExpressionType.Multiply:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.MultiplyAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.MultiplyAssignChecked:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.MultiplyChecked:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Negate:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.NegateChecked:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.New:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.NewArrayBounds:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.NewArrayInit:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Not:
                return "NOT";

            case ExpressionType.NotEqual:
                return "<>";

            case ExpressionType.OnesComplement:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Or:
                return "OR";

            case ExpressionType.OrAssign:
                return "OR";

            case ExpressionType.OrElse:
                return "OR";

            case ExpressionType.Parameter:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.PostDecrementAssign:
                throw new NotImplementedException("未实现此表达式转换");
            case ExpressionType.PostIncrementAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Power:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.PowerAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.PreDecrementAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.PreIncrementAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Quote:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.RightShift:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.RightShiftAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.RuntimeVariables:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Subtract:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.SubtractAssign:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.SubtractAssignChecked:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.SubtractChecked:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Switch:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Throw:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Try:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.TypeAs:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.TypeEqual:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.TypeIs:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.UnaryPlus:
                throw new NotImplementedException("未实现此表达式转换");

            case ExpressionType.Unbox:
                throw new NotImplementedException("未实现此表达式转换");

            default:
                throw new NotImplementedException("未实现此表达式转换");
        }
    }
}