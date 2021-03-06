﻿namespace Nettle.Data.Functions
{
    using Nettle.Compiler;
    using Nettle.Functions;
    using System;
    using System.Net;

    /// <summary>
    /// Represents function for getting a single HTTP resource
    /// </summary>
    public class HttpGetFunction : FunctionBase
    {
        /// <summary>
        /// Constructs the function by defining the parameters
        /// </summary>
        public HttpGetFunction()
        {
            DefineRequiredParameter
            (
                "URL",
                "The URL to get.",
                typeof(string)
            );
        }

        /// <summary>
        /// Gets a description of the function
        /// </summary>
        public override string Description
        {
            get
            {
                return "Gets a single HTTP resource as a string.";
            }
        }

        /// <summary>
        /// Gets the response from a HTTP GET
        /// </summary>
        /// <param name="context">The template context</param>
        /// <param name="parameterValues">The parameter values</param>
        /// <returns>The truncated text</returns>
        protected override object GenerateOutput
            (
                TemplateContext context,
                params object[] parameterValues
            )
        {
            Validate.IsNotNull(context);

            var url = GetParameterValue<string>
            (
                "URL",
                parameterValues
            );

            var headerValues = ExtractKeyValuePairs<string, object>
            (
                parameterValues,
                1
            );

            using (var client = new WebClient())
            {
                foreach (var header in headerValues)
                {
                    var value = String.Empty;

                    if (header.Value != null)
                    {
                        value = header.Value.ToString();
                    }

                    client.Headers.Add
                    (
                        header.Key,
                        value
                    );
                }
                
                return client.DownloadString(url);
            }
        }
    }
}
