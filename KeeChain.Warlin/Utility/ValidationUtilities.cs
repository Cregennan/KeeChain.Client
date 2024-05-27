namespace KeeChain.Warlin.Utility
{
    using System.Reflection;
    using System.Text.RegularExpressions;
    using FluentValidation;

    public static partial class ValidationUtilities
    {
        public static bool IsValidBase32String(this string test) => !string.IsNullOrEmpty(test) && Base32Regex().IsMatch(test);
        
        internal static IValidator? FindValidatorForType(Type type) => _validators.GetValueOrDefault(type);

        private static readonly Dictionary<Type, IValidator> _validators;

        static ValidationUtilities()
        {
            var abstractValidator = typeof(AbstractValidator<>);
            
            _validators = Assembly.GetAssembly(typeof(ValidationUtilities))!.GetTypes()
                .Where(x => x.IsSubclassOfRawGeneric(abstractValidator))
                .ToDictionary( 
                    keySelector: x => x.BaseType!.GetGenericArguments().First(),
                    elementSelector: x => Activator.CreateInstance(x) as IValidator)!;
        }
        
        private static bool IsSubclassOfRawGeneric(this Type derivedType, Type baseType) {
            while (derivedType != null && derivedType != typeof(object)) {
                var currentType = derivedType.IsGenericType ? derivedType.GetGenericTypeDefinition() : derivedType;
                if (baseType == currentType) {
                    return true;
                }

                derivedType = derivedType.BaseType;
            }
            return false;
        }

        [GeneratedRegex(@"^(?:[A-Z2-7]{8})*(?:[A-Z2-7]{2}={6}|[A-Z2-7]{4}={4}|[A-Z2-7]{5}={3}|[A-Z2-7]{7}=)?$")]
        private static partial Regex Base32Regex();
    }
}