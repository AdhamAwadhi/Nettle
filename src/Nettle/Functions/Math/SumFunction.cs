﻿namespace Nettle.Functions.Math
{
    using Nettle.Compiler;
    using System;
    using System.Linq;

    /// <summary>
    /// Represent a sum numbers function implementation
    /// </summary>
    public sealed class SumFunction : FunctionBase
    {
        /// <summary>
        /// Constructs the function by defining the parameters
        /// </summary>
        public SumFunction() 
            : base()
        { }

        /// <summary>
        /// Gets a description of the function
        /// </summary>
        public override string Description
        {
            get
            {
                return "Computes the sum of a sequence of numeric values.";
            }
        }

        /// <summary>
        /// Sums the parameter values supplied as a double
        /// </summary>
        /// <param name="context">The template context</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The summed number</returns>
        protected override object GenerateOutput
            (
                TemplateContext context,
                params object[] parameterValues
            )
        {
            Validate.IsNotNull(context);

            var numbers = ConvertToNumbers(parameterValues);
            var total = numbers.Sum();

            if (total.IsWholeNumber())
            {
                return Convert.ToInt64(total);
            }
            else
            {
                return total;
            }
        }
    }
}
