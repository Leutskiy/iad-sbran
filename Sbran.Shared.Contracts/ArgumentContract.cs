using JetBrains.Annotations;
using System;

namespace Sbran.Shared.Contracts
{
    /// <summary>
    /// Контракты аргументов
    /// </summary>
    public sealed class Contract
    {
        public static class Argument
        {
            [ContractAnnotation("argument:null, argumentName:notnull => halt")]
            [ContractAnnotation("argument:notnull, argumentName:notnull => halt")]
            public static void IsNotNull<TArgument>([CanBeNull] TArgument argument,  string argumentName)
                where TArgument : class
            {
                if (argument == null)
                {
                    var message = $"The {argumentName} argument should not be null";
                    throw new ArgumentNullException(argumentName, message);
                }
            }

            public static void IsNotEmptyString<TArgument>( TArgument stringArgument,  string argumentName)
                where TArgument : class
            {
                if (typeof(string) == typeof(TArgument) && stringArgument.Equals(string.Empty))
                {
                    var message = $"The {argumentName} argument should not be empty";
                    throw new ArgumentException(message);
                }
            }

            public static void IsNotEmptyGuid<TArgument>(TArgument guidArgument,  string argumentName)
                where TArgument : struct
            {
                if (typeof(Guid) == typeof(TArgument) && guidArgument.Equals(Guid.Empty))
                {
                    var message = $"The {argumentName} argument should not be empty";
                    throw new ArgumentException(message);
                }
            }

            public static void IsValidIf(bool validityCondition,  string conditionText)
            {
                if (!validityCondition)
                {
                    var message = $"The {conditionText} condition should be satisfied";
                    throw new ArgumentException(message);
                }
            }

            public static void IsNotNullOrEmptyOrWhiteSpace<TArgument>([CanBeNull] TArgument stringArgument,  string argumentName)
                where TArgument : class
            {
                if (typeof(string) == typeof(TArgument) && string.IsNullOrWhiteSpace(stringArgument.ToString()))
                {
                    var message = $"The {argumentName} argument should not be null or empty or whitespace";
                    throw new ArgumentException(message);
                }
            }
        }

        public static class Implementation
        {
            [ContractAnnotation("argument:null, argumentName:notnull => halt")]
            [ContractAnnotation("argument:notnull, argumentName:notnull => halt")]
            public static void IsNotNull<TArgument>([CanBeNull] TArgument argument,  string argumentName)
                where TArgument : class
            {
                Argument.IsNotNull(argument, argumentName);
            }
        }
    }
}